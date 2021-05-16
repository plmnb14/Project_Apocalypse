using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public float damage { get; set; }
    public LayerMask targetMask { get; set; }
    private BoxCollider2D myCollider;

    private void Awake()
    {
        damage = 0.0f;
        myCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != targetMask)
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
        }
    }
}
