using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : WorldObject
{
    protected Vector3 direction;
    protected LayerMask targetMask;
    protected float damage;
    protected float speed;
    protected float lifetime;

    public virtual void SetStatus(Vector3 dir)
    {

    }

    public virtual void SetStatus(Vector3 dir, Vector3 shotPosition)
    {

    }

    protected override void OnDie()
    {
        ProjectileManager.instance.BackProjtile(myName, this);
    }

    protected virtual void OnFly()
    {
        transform.position += Time.deltaTime * direction * speed;
    }

    protected virtual void OnCycle()
    {
        if (lifetime > 0.0f) lifetime -= Time.deltaTime;
        else OnDie();
    }

    protected virtual void SetUp()
    {
        dead = false;
        speed = 1.5f;
        damage = 200.0f;
        targetMask = LayerMask.NameToLayer("Monster");
        direction = Vector3.right;
        lifetime = 2.0f;
    }
}
