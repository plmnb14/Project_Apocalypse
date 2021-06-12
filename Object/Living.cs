using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : WorldObject
{
    public MountedWeapon weapon { get; set; }

    public enum animatorEnum 
        { IsArmed, OnAttack, OnDie, IsRun, IsBattle, OnReset };

    public string[] animatorParam = 
        { "IsArmed", "OnAttack", "OnDie", "IsRun", "IsBattle", "OnReset" };

    public enum LivingState { Idle, Run, Armed, Armed_Idle, Attack, Hit, Die, Skill, Status_End };
    public LivingState stateCurrent { get; set; }

    public float hitPoint { get; set; } = 1000.0f;

    public Animator animator { get; set; }

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

    #region Action Events
    public virtual void OnSkillEffect()
    {
        Debug.Log("here~~");
    }

    public virtual void OnSkill(string aniParam)
    {
        animator.SetTrigger(aniParam);
        weapon.SetAnimatorTrigger(aniParam);
    }

    #endregion

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

    protected virtual void AwakeSetUp()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void StateCheck()
    {

    }
}
