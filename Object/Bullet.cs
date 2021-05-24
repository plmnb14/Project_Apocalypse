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
        dead = true;
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

    private void OnEnable()
    {
        dead = false;
    }

    private void Awake()
    {
        SetUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != targetMask || dead)
            return;

        Living ColTarget = collision.GetComponent<Living>();

        if (null != ColTarget && !ColTarget.dead)
        {
            LivingData data = PlayerStatusManager.instance.finalHeroStatus;
            damage = DamageCalculator.DamageCaculating(ref data, out bool isCritical);
            float finalDamage = Random.Range(damage * 0.7f, damage);

            Vector3 hitCorss = collision.ClosestPoint(transform.position);
            Vector3 hitNormal = transform.position - collision.transform.position;

            ColTarget.OnDamage(hitCorss, hitNormal, finalDamage, isCritical);

            VFX vfx = VFXManager.instance.GetVFX("VFX_Hit_01");
            vfx.transform.position = hitCorss;

            OnDie();
        }
    }
}
