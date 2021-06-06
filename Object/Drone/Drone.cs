using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Living
{
    #region Private Field
    private Monster targetObject;
    private Transform targetTransform;
    private LayerMask targetLayerMask;
    private Vector2 targetFindVector;
    private bool canAttack;
    private float attackCoolTimeMax;
    private float attackCoolTimeCurrent;
    #endregion

    #region Property
    public DroneStatusForLocal droneStatusForLocal { get; set; }
    #endregion

    #region Awake Event
    private void OnDisable()
    {
        ResetMemberFields();
    }
    private void OnEnable()
    {
        ResetMemberFields();
        StartCoroutine(OnAttackCoolTime());
    }

    protected void ResetMemberFields()
    {
        stateCurrent = LivingState.Idle;

        targetObject = null;
        targetTransform = null;
        targetLayerMask = 1 << LayerMask.NameToLayer("Monster");
        targetFindVector = new Vector2(5.0f, 2.5f);
        canAttack = false;
        attackCoolTimeMax = 2.0f;
        attackCoolTimeCurrent = attackCoolTimeMax;
    }

    protected override void AwakeSetUp()
    {
        animator = GetComponent<Animator>();

        ResetMemberFields();
    }
    private void Awake()
    {
        AwakeSetUp();
    }
    #endregion

    #region Attack Events
    protected void OnAttacks()
    {
        var projectile = ProjectileManager.instance.GetProjectile("Bullet");
        Vector3 direction = Vector3.Normalize(targetTransform.position - transform.position);
        projectile.SetStatus(direction, transform.position);

        StartCoroutine(OnAttackCoolTime());
    }

    private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    protected IEnumerator OnAttackCoolTime()
    {
        while(attackCoolTimeMax > attackCoolTimeCurrent)
        {
            attackCoolTimeCurrent += Time.deltaTime;
            yield return waitForEndOfFrame;
        }

        attackCoolTimeCurrent = 0.0f;
        canAttack = true;

        yield break;
    }

    protected void OnReadyAttack()
    {
        animator.SetTrigger(animatorParam[(int)animatorEnum.OnAttack]);
        canAttack = false;
        stateCurrent = LivingState.Idle;
    }
    #endregion

    #region Public Events
    public void SetMoveAnimation(bool isMove)
    {
        animator.SetBool(animatorParam[(int)animatorEnum.IsRun], isMove);
    }
    #endregion

    #region Update Events
    protected void FindTarget()
    {
        Collider2D[] colliders = 
            Physics2D.OverlapBoxAll(transform.position, targetFindVector, 0.0f, targetLayerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            var collisionTarget = colliders[i].gameObject.GetComponent<Monster>();
            if(!collisionTarget.dead && colliders[i].enabled)
            {
                targetObject = collisionTarget;
                targetTransform = targetObject.transform;
                stateCurrent = LivingState.Attack;
                break;
            }
        }
    }

    protected void CheckTargetObject()
    {
        if(targetObject.dead)
        {
            targetObject = null;
        }
        else
        {
            Debug.Log("대상이 살아 있음");
            stateCurrent = LivingState.Attack;
        }
    }

    protected void CheckState()
    {
        switch(stateCurrent)
        {
            case LivingState.Idle:
                {
                    if (null == targetObject && canAttack) FindTarget();
                    else if (canAttack) CheckTargetObject();
                    break;
                }
            case LivingState.Attack:
                {
                    OnReadyAttack();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void Update()
    {
        CheckState();
    }
    #endregion
}
