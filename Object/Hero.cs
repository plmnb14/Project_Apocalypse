using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Living
{
    private MountedWeapon weapon;
    private bool canAttack;
    private Monster target;

    WaitForSeconds waitSecond = new WaitForSeconds(0.5f);
    public IEnumerator BeginStage()
    {
        stateCurrent = LivingState.Idle;
        animator.SetTrigger(animatorParam[(int)animatorEnum.OnReset]);
        animator.SetBool(animatorParam[(int)animatorEnum.IsArmed], false);
        animator.SetBool(animatorParam[(int)animatorEnum.IsBattle], false);
        animator.SetBool(animatorParam[(int)animatorEnum.IsRun], false);

        weapon.SetAnimatorTrigger(animatorParam[(int)animatorEnum.OnReset]);
        weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsArmed], false);
        weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsBattle], false);
        weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsRun], false);

        yield return waitSecond;

        stateCurrent = LivingState.Run;
    }

    public void UpdateAttackSpeed(float speed)
    {
        animator.SetFloat("AttackSpeed", speed);
        weapon.SetAnimatorfloat("AttackSpeed", speed);
    }

    protected override void Attack()
    {

    }

    public override void OnDamage(Vector3 crossPoint, Vector3 hitNotmal, float damage, bool isCritical = false)
    {
        float finalDamage = damage;

        UI_DamageFont font = PoolManager.instance.GetObject("DamageFont") as UI_DamageFont;
        font.SetNumber(finalDamage, crossPoint);
        font.ChangeColor(isCritical ? UI_DamageFont.fontUsedType.Critical : UI_DamageFont.fontUsedType.Default);

        base.OnDamage(crossPoint, hitNotmal, finalDamage);
    }

    private float atkTimeCur = 0.0f;
    protected override IEnumerator OnAttack()
    {
        if(canAttack)
        {
            animator.SetTrigger(animatorParam[(int)animatorEnum.OnAttack]);
            weapon.SetAnimatorTrigger(animatorParam[(int)animatorEnum.OnAttack]);
            weapon.targetTranstorm = target.transform.position;
            canAttack = false;

            yield return null;

            atkTimeCur = animator.GetCurrentAnimatorStateInfo(0).length;
        }

        else
        {
            if(atkTimeCur > 0.0f)
            {
                atkTimeCur -= Time.deltaTime;
            }

            else
            {
                atkTimeCur = 0.0f;
                stateCurrent = LivingState.Armed_Idle;
            }
        }
    }

    private float runTimeCur = 0.0f;
    private float runTimeMax = 1.0f;
    protected void OnRun()
    {
        if(!animator.GetBool(animatorParam[(int)animatorEnum.IsRun]))
        {
            animator.SetBool(animatorParam[(int)animatorEnum.IsRun], true);
            weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsRun], true);

            StageManager.instance.ScrollingBackround();
            DroneManager.instance.MoveDrones(true);
        }

        else
        {
            if (runTimeCur < runTimeMax) runTimeCur += Time.deltaTime;

            else
            {
                runTimeCur = 0.0f;
                stateCurrent = LivingState.Armed;

                animator.SetBool(animatorParam[(int)animatorEnum.IsRun], false);
                weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsRun], false);

                StageManager.instance.SummonMonster();
                DroneManager.instance.MoveDrones(false);
            }
        }
    }

    protected void OnArmedIdle()
    {
        Vector3 vec = new Vector3(2.0f, transform.position.y * 0.5f);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + vec, new Vector2(3.4f, 3.0f), 0.0f, 1 << LayerMask.NameToLayer("Monster"));

        Monster targetMonster = null;
        float _distance = Mathf.Infinity;
        for(int i = 0; i < colliders.Length; i++)
        {
            var tmpMonster = colliders[i].gameObject.GetComponent<Monster>();
            if (tmpMonster.dead) continue;

            float currentDistance = Vector3.Distance(transform.position, tmpMonster.transform.position);
            if(_distance > currentDistance)
            {
                targetMonster = tmpMonster;
                _distance = currentDistance;
            }
        }

        target = targetMonster;
        if (null != target)
        {
            stateCurrent = LivingState.Attack;
        }
    }

    private float armedTimeCur = 0.0f;
    private float armedTimeMax = 0.0f;
    protected IEnumerator OnArmed()
    {
        if(!animator.GetBool(animatorParam[(int)animatorEnum.IsArmed]))
        {
            animator.SetBool(animatorParam[(int)animatorEnum.IsArmed], true);
            weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsArmed], true);

            yield return new WaitForEndOfFrame();

            armedTimeMax = animator.GetCurrentAnimatorStateInfo(0).length;
        }

        else
        {
            if (armedTimeCur < armedTimeMax) armedTimeCur += Time.deltaTime;

            else
            {
                armedTimeCur = 0.0f;
                stateCurrent = LivingState.Armed_Idle;

                animator.SetBool(animatorParam[(int)animatorEnum.IsArmed], false);
                weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsArmed], false);

                animator.SetBool(animatorParam[(int)animatorEnum.IsBattle], true);
                weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsBattle], true);
            }
        }
    }

    public override void OnHit()
    {
        base.OnHit();
    }

    protected override void AwakeSetUp()
    {
        base.AwakeSetUp();

        stateCurrent = LivingState.Idle;
        weapon = transform.GetChild(0).GetComponent<MountedWeapon>();
        canAttack = true;
    }

    protected override void StateCheck()
    {
        switch(stateCurrent)
        {
            case LivingState.Idle:
                {
                    break;
                }

            case LivingState.Run:
                {
                    OnRun();
                    break;
                }

            case LivingState.Armed:
                {
                    StartCoroutine(OnArmed());
                    break;
                }

            case LivingState.Armed_Idle:
                {
                    OnArmedIdle();
                    break;
                }

            case LivingState.Attack:
                {
                    StartCoroutine(OnAttack());
                    break;
                }

            case LivingState.Hit:
                {
                    break;
                }

            case LivingState.Die:
                {
                    break;
                }
        }
    }

    private float atkCooltimeCur = 0.0f;
    private float atkCooltimeMax = 0.1f;
    private void AttackCooltime()
    {
        if (stateCurrent == LivingState.Attack || canAttack) return;

        if(atkCooltimeCur < atkCooltimeMax)
        {
            atkCooltimeCur += Time.deltaTime;
        }

        else
        {
            atkCooltimeCur = 0.0f;
            canAttack = true;
        }
    }

    private void Update()
    {
        StateCheck();
        AttackCooltime();
    }

    private void Awake()
    {
        hitPoint = 10000000.0f;
        AwakeSetUp();
    }
}
