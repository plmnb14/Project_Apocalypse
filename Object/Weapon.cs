using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum State { Idle, Armed, Armed_Idle, Attack, Die, Status_End };
    public State curState { get; set; }

    protected Animator animator;

    public Vector3 targetTranstorm { get; set; }

    public virtual void SetAnimatorTrigger(string s)
    {
        animator.SetTrigger(s);
    }

    public virtual void SetAnimatorBool(string s, bool b)
    {
        animator.SetBool(s, b);
    }

    public virtual void Attack()
    {

    }

    protected virtual void SetUp()
    {
        animator = GetComponent<Animator>();
        curState = State.Idle;
    }
}
