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
    #endregion

    #region Private Fields
    private SkillIcon mountingSkillIcon;
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
            // 이미 장착된것 해제
        }

        else
        {
            isMounted = true;
            icons[(int)SlotType.Skill].gameObject.SetActive(true);
            icons[(int)SlotType.Mount].gameObject.SetActive(false);
        }

        스킬에 따라 장착하기/ 장착중바꾸기
        skillIcon.isMounted = isMounted;
        mountingSkillIcon = skillIcon;
        skill = mountingSkillIcon.skill;
        ChangeImages();
    }

    public void SetMountMode(bool isActive)
    {
        isMountingMode = isActive;
        rayCaster.enabled = isActive;
    }
    #endregion

    #region Change Events
    public void ChangeImages()
    {
        Debug.Log(skill.originSkillDB.skillIcon);
        icons[(int)SlotType.Skill].sprite = skill.originSkillDB.skillIcon;
    }

    public override void UnlockSkillSlot()
    {
        base.UnlockSkillSlot();

        icons[(int)SlotType.Lock].gameObject.SetActive(false);
    }
    #endregion
}
