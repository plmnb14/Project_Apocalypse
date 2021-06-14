using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSlot : MonoBehaviour
{
    #region Private Fields
    private SkillSlotUI[] skillSlotUI;
    #endregion

    #region Awake Events
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }

    private void AwakeSetUp()
    {
        skillSlotUI = new SkillSlotUI[SkillManager.Instance.maxSkillCount];
    }

    private void LoadChilds()
    {
        int maxSkillCount = SkillManager.Instance.maxSkillCount;
        for(int i = 0; i < maxSkillCount; i++)
        {
            skillSlotUI[i] = transform.GetChild(i).GetComponent<SkillSlotUI>();
            skillSlotUI[i].slotIndex = i;
        }
    }
    #endregion

    #region Events
    public void UnlockSkillSlot()
    {
        int curSkillCount = SkillManager.Instance.curSkillCount;
        for(int i = 0; i < curSkillCount; i++)
        {
            skillSlotUI[i].UnlockSkillSlot();
        }
    }

    public void SetMountMode(bool isActive)
    {
        int curSkillCount = SkillManager.Instance.curSkillCount;
        for (int i = 0; i < curSkillCount; i++)
        {
            skillSlotUI[i].SetMountMode(isActive);
        }
    }
    #endregion
}
