using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLobbyUI : PopUpInnerUI
{
    #region Private Fields
    private readonly int maxSkillSlot = 6;
    private SkillWithCoreUI[] skillWithCoreUIs;
    #endregion

    #region Awake Events
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }

    private void AwakeSetUp()
    {
        skillWithCoreUIs = new SkillWithCoreUI[maxSkillSlot];
    }

    private void LoadChilds()
    {
        GameObject slotGreed = transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        for(int i = 0; i < maxSkillSlot; i++)
        {
            skillWithCoreUIs[i] = slotGreed.transform.GetChild(i).GetComponent<SkillWithCoreUI>();
        }
    }
    #endregion

    #region Start Events
    private void Start()
    {
        LockSlot();
    }

    public void LockSlot()
    {
        int UnlockCnt = SkillManager.Instance.curSkillCount;
        int maxCnt = SkillManager.Instance.maxSkillCount;
        for (int i = 0; i < UnlockCnt; i++)
        {
            skillWithCoreUIs[i].LockSlot(false);
        }

        for (int i = UnlockCnt; i < maxCnt; i++)
        {
            skillWithCoreUIs[i].LockSlot();
        }
    }
    #endregion

    #region Events
    public void ChangeSkillStatus(ref SkillIcon skillIcon)
    {
        int mountSlotIdx = skillIcon.mountSlotIndex;
        skillWithCoreUIs[mountSlotIdx].ChangeSkillIcon(ref skillIcon);
    }

    public void DismountSkillImage(int slotIdx)
    {
        skillWithCoreUIs[slotIdx].DismountSkillImage();
    }
    #endregion
}
