using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotBase : MonoBehaviour
{
    #region Protected Fields
    protected Image[] icons;
    #endregion

    #region Property Fields
    public bool isUnlocked { get; set; }
    public bool isMounted { get; set; }
    public Skill skill { get; set; }
    #endregion

    #region Awake Events
    protected virtual void AwakeSetUp()
    {
        isMounted = false;
        isUnlocked = false;
        skill = null;
    }
    #endregion

    #region Events
    public virtual void UnlockSkillSlot()
    {
        isUnlocked = true;
    }
    #endregion
}
