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
}
