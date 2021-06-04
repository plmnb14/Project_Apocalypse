using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDeath();
public class WorldObject : MonoBehaviour
{
    public event OnDeath onDeath;

    public bool dead { get; set; }
    public string myName { get; set; }
    protected virtual void OnDie()
    {
        if (onDeath != null) onDeath();

        dead = true;
    }

    public virtual void ResetStatus(Transform parent = null)
    {
        //dead = false;
        transform.position = Vector3.zero;
        transform.localPosition = Vector3.zero;

        if (null != parent) transform.parent = parent;
    }
}
