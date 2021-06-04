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
    protected SpriteRenderer spriteRenderer;
    protected AttackCollider attackCollider;

    private Color myColor;
    protected float attackRange;
    protected bool canAttack;

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

        stateCurrent = LivingState.Die;
        deadTime = animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(deadTime);

        StageManager.instance.BackMonster(myName, this);
    }

    public override void OnDamage(Vector3 crossPoint, Vector3 hitNotmal, float damage, bool isCritical = false)
    {
        float finalDamage = damage;

        if (null != hpBar) hpBar.ChangeHealth(hitPoint - finalDamage);

        UI_DamageFont font = PoolManager.instance.GetObject("DamageFont") as UI_DamageFont;
        font.SetNumber(finalDamage, crossPoint);
        font.ChangeColor(isCritical ? UI_DamageFont.fontUsedType.Critical : UI_DamageFont.fontUsedType.Default);

        base.OnDamage(crossPoint, hitNotmal, finalDamage);
        StartCoroutine(ChangeColor());
    }

    private float colorTime;
    private WaitForEndOfFrame colorWait = new WaitForEndOfFrame(); 
    private IEnumerator ChangeColor()
    {
        colorTime = 0.05f;
        spriteRenderer.material.color = Color.red;
        while (colorTime > 0.0f)
        {
            colorTime -= Time.deltaTime;
            yield return colorWait;
        }
        spriteRenderer.material.color = myColor;

        yield break;
    }

    public override void ResetStatus(Transform parent = null)
    {
        base.ResetStatus();

        stateCurrent = LivingState.Idle;
        hitPoint = 2000.0f;
        attackRange = 0.75f;
        canAttack = false;
    }

    protected void OnRun()
    {
        if (dead)
            return;

        if(attackRange < Vector3.Distance(transform.position, StageManager.instance.hero.transform.position))
        {
            transform.position += Vector3.left * Time.deltaTime;
        }
        else
        {
            stateCurrent = LivingState.Attack;
        }
    }

    private float atkTimeCur = 0.0f;
    protected override IEnumerator OnAttack()
    {
        if (canAttack)
        {
            animator.SetTrigger(animatorParam[(int)animatorEnum.OnAttack]);
            canAttack = false;

            yield return new WaitForEndOfFrame();

            atkTimeCur = animator.GetCurrentAnimatorStateInfo(0).length;
        }

        else
        {
            if (atkTimeCur > 0.0f)
            {
                atkTimeCur -= Time.deltaTime;
            }

            else
            {
                atkTimeCur = 0.0f;
                stateCurrent = LivingState.Idle;
            }
        }
    }

    private float atkCooltimeCur = 0.0f;
    private float atkCooltimeMax = 0.1f;
    private void AttackCooltime()
    {
        if (stateCurrent == LivingState.Attack || canAttack) return;

        if (atkCooltimeCur < atkCooltimeMax)
        {
            atkCooltimeCur += Time.deltaTime;
        }

        else
        {
            atkCooltimeCur = 0.0f;
            canAttack = true;
        }
    }

    protected override void StateCheck()
    {
        switch (stateCurrent)
        {
            case LivingState.Idle:
            case LivingState.Run:
                {
                    OnRun();
                    break;
                }

            case LivingState.Attack:
                {
                    StartCoroutine(OnAttack());
                    break;
                }

            case LivingState.Die:
                {
                    break;
                }
        }
    }

    private void Update()
    {
        StateCheck();
        AttackCooltime();
    }

    private Color GetColor(int colorNumber) => colorNumber switch
    {
        0 => Color.red,
        1 => new Color(1.0f, 0.4f, 0.6f, 1.0f),
        2 => Color.yellow,
        3 => Color.blue,
        4 => Color.black,
        _ => Color.white
    };

    private void OnDisable()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnEnable()
    {
        GetComponent<Collider2D>().enabled = true;
        spriteRenderer.material.color = myColor;
    }

    protected override void AwakeSetUp()
    {
        base.AwakeSetUp();

        hitPoint = 2000.0f;
        stateCurrent = LivingState.Run;
        attackRange = Random.Range(0.5f, 0.9f);
        canAttack = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
        attackCollider = transform.GetChild(0).GetComponent<AttackCollider>();
        attackCollider.damage = 10.0f;
        attackCollider.targetMask = LayerMask.NameToLayer("Player");

        int rand = Random.Range(0, 5);
        myColor = GetColor(rand);
    }

    private void Awake()
    {
        AwakeSetUp();
    }
}
