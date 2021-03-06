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

    public void ExchangeSkillSlot(int slotA, int slotB)
    {
        var slotASkillIcon = skillSlotUI[slotA].mountingSkillIcon;
        var slotBSkillIcon = skillSlotUI[slotB].mountingSkillIcon;
        skillSlotUI[slotA].ExchangeMountSkill(slotBSkillIcon);
        skillSlotUI[slotB].ExchangeMountSkill(slotASkillIcon);
    }

    public void DismountSkillSlot(int skillIndex)
    {
        skillSlotUI[skillIndex].DismountSkill();
    }

    public void SetSkillOnSlot(int slotIndex)
    {
        var currrentSkill = SkillManager.Instance.GetSelectedSkill();
        // 이미 장착되 있다면
        if (skillSlotUI[slotIndex].isMounted)
        {
            // 근데 나도 장착되있다면
            if (currrentSkill.isMounted)
            {
                ExchangeSkillSlot(skillSlotUI[slotIndex].mountingSkillIcon.mountSlotIndex, 
                    currrentSkill.mountSlotIndex);
            }
        }

        else
        {
            // 장착이 안되있고
            if (currrentSkill.isMounted)
            {
                //장착하려는 애만 장착되어있으면 장착취소
                SkillManager.Instance.DismountSkillImage(currrentSkill.mountSlotIndex);
                DismountSkillSlot(currrentSkill.mountSlotIndex);
            }

            // 후 다시 장착
            skillSlotUI[slotIndex].SetSkillOnManager();
        }
    }
    #endregion
}
