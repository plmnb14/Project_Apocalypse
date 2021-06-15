using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlotUI : SkillSlotBase, IPointerClickHandler
{
    #region Enum
    private enum SlotType { Skill, Mount, Lock, End };
    #endregion

    #region Click Events
    public void OnPointerClick(PointerEventData eventData)
    {
        SkillManager skillManager = SkillManager.Instance;
        MountSkill(skillManager.GetSelectedSkill());
        skillManager.SetMountMode(false);
    }
    #endregion

    #region Property Fields
    public bool isMountingMode { get; set; }
    public int slotIndex { get; set; }

    public SkillIcon mountingSkillIcon { get; set; }
    #endregion

    #region Private Fields
    private GraphicRaycaster rayCaster;
    #endregion

    #region Awake Events
    private void Awake()
    {
        AwakeSetUp();
        LoadComponents();
        LoadChilds();
        OnDefault();
    }

    protected override void AwakeSetUp()
    {
        base.AwakeSetUp();
        icons = new Image[(int)SlotType.End];
        mountingSkillIcon = null;
        isMountingMode = false;
    }

    private void LoadComponents()
    {
        rayCaster = GetComponent<GraphicRaycaster>();
        rayCaster.enabled = false;
    }

    private void LoadChilds()
    {
        for (int i = 0; i < (int)SlotType.End; i++)
        {
            icons[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    private void OnDefault()
    {
        icons[(int)SlotType.Skill].gameObject.SetActive(false);
        icons[(int)SlotType.Mount].gameObject.SetActive(true);
        icons[(int)SlotType.Lock].gameObject.SetActive(true);
    }
    #endregion

    #region Mount Events
    public void MountSkill(SkillIcon skillIcon)
    {
        if(isMounted)
        {
            // 장착된 곳을 클릭하면 장착 종료 or 팝업창
            if(mountingSkillIcon == skillIcon)
            {
                Debug.Log("같은 곳을 클릭했습니다");
                return;
            }

            // 이미 다른 곳에 장착된 스킬이라면
            else if(skillIcon.isMounted)
            {
                Debug.Log("다른 곳에 장착되어 있습니다.");
                // 지금 이 위치에 장착된 스킬과 장착하려는 스킬을 교환
                SkillManager.Instance.ExchangeSkillSlot(slotIndex, skillIcon.mountSlotIndex);
                return;
            }
        }

        else
        {
            if (skillIcon.isMounted)
            {
                SkillManager.Instance.DismountSkillSlot(skillIcon.mountSlotIndex);
            }

            isMounted = true;
            skillIcon.isMounted = isMounted;
            icons[(int)SlotType.Skill].gameObject.SetActive(true);
            icons[(int)SlotType.Mount].gameObject.SetActive(false);
        }

        ExchangeMountSkill(skillIcon);
    }

    public void DismountSkill()
    {
        Debug.Log(mountingSkillIcon);
        Debug.Log(mountingSkillIcon.mountSlotIndex);

        isMounted = false;
        mountingSkillIcon.isMounted = false;
        mountingSkillIcon.mountSlotIndex = -1;
        mountingSkillIcon = null;
        skill = null;

        icons[(int)SlotType.Skill].gameObject.SetActive(false);
        icons[(int)SlotType.Mount].gameObject.SetActive(true);
    }

    public void SetMountMode(bool isActive)
    {
        isMountingMode = isActive;
        rayCaster.enabled = isActive;
    }

    public void ExchangeMountSkill(SkillIcon skillIcon)
    {
        skillIcon.mountSlotIndex = slotIndex;
        mountingSkillIcon = skillIcon;
        skill = mountingSkillIcon.skill;
        ChangeImages();
    }

    #endregion

    #region Change Events
    public void ChangeImages()
    {
        icons[(int)SlotType.Skill].sprite = skill.originSkillDB.skillIcon;
    }

    public override void UnlockSkillSlot()
    {
        base.UnlockSkillSlot();

        icons[(int)SlotType.Lock].gameObject.SetActive(false);
    }
    #endregion
}
