using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Object : WorldObject
{
    public virtual void OnActive(bool boolen)
    {
        gameObject.SetActive(boolen);
    }
}
