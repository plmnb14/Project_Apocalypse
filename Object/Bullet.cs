using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public override void SetStatus(Vector3 dir)
    {
        direction = transform.right = (dir + new Vector3(0.0f, Random.Range(0.1f, 0.3f), 0.0f));
    }

    protected override void OnDie()
    {
        damage = 200.0f;
        lifetime = 2.0f;
        base.OnDie();
    }

    protected override void SetUp()
    {
        base.SetUp();
        speed = 3.0f;
    }

    private void Update()
    {
        base.OnFly();
        base.OnCycle();
    }

    private void Awake()
    {
        SetUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != targetMask)
            return;

        Living ColTarget = collision.GetComponent<Living>();

        if (null != ColTarget && !ColTarget.dead)
        {
            float finalDamage = damage + Random.Range(damage * -0.2f, damage * 0.2f);

            Vector3 hitCorss = collision.ClosestPoint(transform.position);
            Vector3 hitNormal = transform.position - collision.transform.position;

            ColTarget.OnDamage(hitCorss, hitNormal, finalDamage);

            VFX vfx = VFXManager.instance.GetVFX("VFX_Hit_01");
            vfx.transform.position = hitCorss;

            OnDie();
        }
    }
}
