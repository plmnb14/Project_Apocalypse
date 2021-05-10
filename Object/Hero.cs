using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Living
{
    private Weapon weapon;
    private bool canAttack;
    private Monster target;

    WaitForSeconds waitSecond = new WaitForSeconds(0.5f);
    public IEnumerator BeginStage()
    {
        curState = State.Idle;
        animator.SetTrigger(animatorParam[(int)animatorEnum.OnReset]);
        animator.SetBool(animatorParam[(int)animatorEnum.IsArmed], false);
        animator.SetBool(animatorParam[(int)animatorEnum.IsBattle], false);
        animator.SetBool(animatorParam[(int)animatorEnum.IsRun], false);

        weapon.SetAnimatorTrigger(animatorParam[(int)animatorEnum.OnReset]);
        weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsArmed], false);
        weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsBattle], false);
        weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsRun], false);

        yield return waitSecond;

        curState = State.Run;
    }

    public void UpdateAttackSpeed(float speed)
    {
        animator.SetFloat("AttackSpeed", speed);
        weapon.SetAnimatorfloat("AttackSpeed", speed);
    }

    protected override void Attack()
    {

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
                curState = State.Armed_Idle;
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
        }

        else
        {
            if (runTimeCur < runTimeMax) runTimeCur += Time.deltaTime;

            else
            {
                runTimeCur = 0.0f;
                curState = State.Armed;

                animator.SetBool(animatorParam[(int)animatorEnum.IsRun], false);
                weapon.SetAnimatorBool(animatorParam[(int)animatorEnum.IsRun], false);

                StageManager.instance.SummonMonster();
            }
        }
    }

    protected void OnArmedIdle()
    {
        Vector3 vec = new Vector3(2.0f, transform.position.y * 0.5f);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + vec, new Vector2(3.4f, 3.0f), 0.0f, 1 << LayerMask.NameToLayer("Monster"));

        for(int i = 0; i < colliders.Length; i++)
        {
            target = colliders[i].gameObject.GetComponent<Monster>();

            if(target.dead)
            {
                target = null;
            }

            else
            {
                curState = State.Attack;
                break;
            }
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
                curState = State.Armed_Idle;

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

    protected override void SetUp()
    {
        base.SetUp();

        curState = State.Idle;
        weapon = transform.GetChild(0).GetComponent<Weapon>();
        canAttack = true;
    }

    protected override void StateCheck()
    {
        switch(curState)
        {
            case State.Idle:
                {
                    break;
                }

            case State.Run:
                {
                    OnRun();
                    break;
                }

            case State.Armed:
                {
                    StartCoroutine(OnArmed());
                    break;
                }

            case State.Armed_Idle:
                {
                    OnArmedIdle();
                    break;
                }

            case State.Attack:
                {
                    StartCoroutine(OnAttack());
                    break;
                }

            case State.Hit:
                {
                    break;
                }

            case State.Die:
                {
                    break;
                }
        }
    }

    private float atkCooltimeCur = 0.0f;
    private float atkCooltimeMax = 0.1f;
    private void AttackCooltime()
    {
        if (curState == State.Attack || canAttack) return;

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
        SetUp();
    }
}
