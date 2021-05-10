using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Living
{
    public UI_HPBar HpBar 
    {
        get { return hpBar; } 
        set { hpBar = value; if (null != hpBar) hpBar.SetUpHealth(hitPoint); } 
    }
    private UI_HPBar hpBar;
    private float deadTime;
    protected GameObject targetHero;

    protected override IEnumerator OnDead()
    {
        dead = true;
        animator.SetTrigger(animatorParam[(int)animatorEnum.OnDie]);
        hpBar = null;

        for (int i = 0; i < 10; i++)
        {
            ResourceManager.instance.GetResource("UI_DropCoin").transform.position = transform.position + (Vector3.up * 0.2f);
        }

        yield return new WaitForEndOfFrame();

        curState = State.Die;
        deadTime = animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(deadTime);

        StageManager.instance.BackMonster(myName, this);
    }

    public override void OnDamage(Vector3 crossPoint, Vector3 hitNotmal, float damage, bool isCritical = false)
    {
        float finalDamage = damage;

        if (null != hpBar) hpBar.ChangeHealth(hitPoint - finalDamage);

        UI_DamageFont font = Instantiate(Resources.Load<UI_DamageFont>("Prefab/DamageFont"));
        font.SetNumber(finalDamage, crossPoint);
        font.ChangeColor(isCritical ? UI_DamageFont.fontUsedType.Critical : UI_DamageFont.fontUsedType.Default);

        base.OnDamage(crossPoint, hitNotmal, finalDamage);
    }

    public override void ResetStatus(Transform parent = null)
    {
        base.ResetStatus();

        curState = State.Idle;
        hitPoint = 2000.0f;
    }


    protected void OnRun()
    {
        if (dead)
            return;

        transform.position += Vector3.left * Time.deltaTime * 1.0f;
    }

    protected override void SetUp()
    {
        base.SetUp();

        hitPoint = 2000.0f;
        curState = State.Idle;
    }

    protected override void StateCheck()
    {
        switch (curState)
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

            case State.Attack:
                {
                    OnAttack();
                    break;
                }

            case State.Die:
                {
                    break;
                }
        }
    }

    private void Update()
    {
        OnRun();
    }

    private void Awake()
    {
        SetUp();
    }
}
