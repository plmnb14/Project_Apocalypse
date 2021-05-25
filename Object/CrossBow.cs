using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MountedWeapon
{
    private GameObject aimPoint;

    public override void Attack()
    {
        Projectile proj = ProjectileManager.instance.GetProjectile("Bullet");
        proj.transform.position = aimPoint.transform.position;
        proj.SetStatus(targetTranstorm - aimPoint.transform.position);

        VFX vfx = VFXManager.instance.GetVFX("VFX_Shot_00");
        vfx.transform.position = aimPoint.transform.position;
    }

    protected override void SetUp()
    {
        base.SetUp();
        aimPoint = transform.GetChild(0).gameObject;
    }

    private void Awake()
    {
        SetUp();
    }
}
