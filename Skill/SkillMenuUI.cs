using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMenuUI : ContentsMenu
{
    #region Enum
    public enum SkillMenuEnum { Lobby, SkillInven, CoreInven, End };
    #endregion

    #region Property Fields
    public int openSkillSlotIdx { get; set; }
    #endregion

    #region Private Fields
    private readonly int maxSkillMenuCnt = 3;
    public SkillMenuEnum curSkillMenu;
    private PopUpInnerUI[] skillMenus;
    private PopUpUI skillSelctUI;
    #endregion

    #region Awake Events
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }

    private void AwakeSetUp()
    {
        curSkillMenu = SkillMenuEnum.Lobby;
        skillMenus = new PopUpInnerUI[maxSkillMenuCnt];
    }

    private void LoadChilds()
    {
        for(int i = 0; i < maxSkillMenuCnt; i++)
        {
            skillMenus[i] = transform.GetChild(i).GetComponent<PopUpInnerUI>();
            skillMenus[i].gameObject.SetActive(true);
        }

        skillSelctUI = transform.GetChild(3).GetComponent<PopUpUI>();
    }
    #endregion

    #region Start Events
    private void Start()
    {
        ResetMenus();
        OpenDefaultPopUp();
    }

    private void ResetMenus()
    {
        for(int i = 0; i < maxSkillMenuCnt; i++)
        {
            skillMenus[i].OpenSetUp();
            skillMenus[i].gameObject.SetActive(false);
        }
    }

    private void OpenDefaultPopUp()
    {
        skillMenus[(int)curSkillMenu].AddPopUpUI();
    }

    public void SetSkillDBOnIcon()
    {
        var skillInven = skillMenus[(int)SkillMenuEnum.SkillInven] as SkillInvenUI;
        skillInven.SetSkillDBOnIcon();

    }
    #endregion

    #region Reset Events
    public void ResetPopUpActive(bool isActive)
    {
        for(int i = 0; i < maxSkillMenuCnt; i++)
        {
            skillMenus[i].gameObject.SetActive(isActive);
        }
    }
    public override void ResetOnEnable()
    {
        ResetPopUpActive(false);
        skillMenus[(int)SkillMenuEnum.Lobby].AddPopUpUI();
    }

    public void ResetCurMenu()
    {
        curSkillMenu = SkillMenuEnum.Lobby;
    }
    #endregion

    #region Open Events
    public void OpenSkillUI(SkillMenuEnum skillMenu, bool isSimple)
    {
        if (curSkillMenu == skillMenu) return;

        curSkillMenu = skillMenu;
        skillMenus[(int)skillMenu].AddPopUpUI(true);
        skillMenus[(int)skillMenu].OpenSetUp(isSimple);
    }

    public void OpenSkillDetailUI(SkillMenuEnum skillMenu)
    {
        skillMenus[(int)skillMenu].OpenDetailPopUp();
    }

    public void SetSkillIconOnDetail(SkillIcon skillIcon)
    {
        var skillInven = skillMenus[(int)SkillMenuEnum.SkillInven] as SkillInvenUI;
        skillInven.SetSkillOnDetail(skillIcon);
    }
    #endregion

    #region Events
    public void MountSkill()
    {
        var skillInven = skillMenus[(int)SkillMenuEnum.SkillInven] as SkillInvenUI;
        skillInven.MountSkill();
    }

    public void SetMountMode()
    {
        skillSelctUI.AddPopUpUI();
    }

    public SkillIcon GetSelectedSkill()
    {
        var skillInven = skillMenus[(int)SkillMenuEnum.SkillInven] as SkillInvenUI;
        return skillInven.GetSelectedSkill();
    }

    public void ChangeSkillStatus(ref SkillIcon skillIcon)
    {
        var skillLobby = skillMenus[(int)SkillMenuEnum.Lobby] as SkillLobbyUI;
        skillLobby.ChangeSkillStatus(ref skillIcon);
    }

    public bool GetSkillInvenMode()
    {
        var skillInven = skillMenus[(int)SkillMenuEnum.SkillInven] as SkillInvenUI;
        return skillInven.isSimpleMode;
    }

    public void DismountSkillImage(int slotIdx)
    {
        var skillLobby = skillMenus[(int)SkillMenuEnum.Lobby] as SkillLobbyUI;
        skillLobby.DismountSkillImage(slotIdx);
    }
    #endregion
}
