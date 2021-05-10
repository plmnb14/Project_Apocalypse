using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : WorldObject
{
    protected enum animatorEnum 
        { IsArmed, OnAttack, OnDie, IsRun, IsBattle, OnReset };

    protected string[] animatorParam = 
        { "IsArmed", "OnAttack", "OnDie", "IsRun", "IsBattle", "OnReset" };

    public enum State { Idle, Run, Armed, Armed_Idle, Attack, Hit, Die, Status_End };
    public State curState { get; set; }

    public float hitPoint { get; set; } = 1000.0f;

    protected Animator animator;

    protected override void OnDie()
    {
        base.OnDie();
    }

    public virtual void OnDamage(Vector3 crossPoint, Vector3 hitNotmal, float damage, bool isCritical = false)
    {
        hitPoint -= damage;

        if(hitPoint <= 0.0f)
        {
            StartCoroutine(OnDead());
        }
    }

    public override void ResetStatus(Transform parent = null)
    {
        base.ResetStatus();
    }

    protected virtual void Attack()
    {

    }

    protected virtual IEnumerator OnAttack()
    {
        yield return null;
    }

    protected virtual IEnumerator OnDead() { dead = true; yield return new WaitForEndOfFrame(); }
    public virtual void OnHit() { }

    protected virtual void SetUp()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void StateCheck()
    {

    }
}
