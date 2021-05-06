using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Once : VFX
{
    protected override void OnDie()
    {
        base.OnDie();
        VFXManager.instance.BackVFX(myName, this);
    }
}
