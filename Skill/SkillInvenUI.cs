using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvenUI : PopUpInnerUI
{
    #region Enum
    private enum BarTypeEnum { Simple, Full, End };
    #endregion

    #region Property Fields
    public bool isSimpleMode { get; set; }
    #endregion

    #region Private Fields
    //private BarTypeEnum barTypeEnum;
    private SkillActType skillActType;
    private GameObject[] typeBar;
    private SkillTree[] skillTrees;
    private SkillDetailUI selectSkillPopUp;
    #endregion

    #region Awake Evenets
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }

    private void AwakeSetUp()
    {
        isSimpleMode = false;
        skillActType = SkillActType.Active;

        typeBar = new GameObject[(int)BarTypeEnum.End];
        skillTrees = new SkillTree[(int)SkillActType.End];
    }

    private void LoadChilds()
    {
        for(int i = 0; i < (int)BarTypeEnum.End; i++)
        {
            typeBar[i] = transform.GetChild(i + 1).gameObject;
        }

        for (int i = 0; i < (int)SkillActType.End; i++)
        {
            skillTrees[i] = transform.GetChild(i + 3).GetComponent<SkillTree>();
            skillTrees[i].gameObject.SetActive(true);
        }

        selectSkillPopUp = transform.GetChild(6).GetComponent<SkillDetailUI>();
    }
    #endregion

    #region Reset Events
    private void DeactivePopUp()
    {
        for (int i = 0; i < (int)BarTypeEnum.End; i++)
        {
            typeBar[i].SetActive(false);
        }

        for (int i = 0; i < (int)SkillActType.End; i++)
        {
            skillTrees[i].gameObject.SetActive(false);
        }

        selectSkillPopUp.gameObject.SetActive(false);
    }

    private void ActivePopUp(bool isSimple)
    {
        BarTypeEnum openType = isSimple ? BarTypeEnum.Simple : BarTypeEnum.Full;
        typeBar[(int)openType].SetActive(true);
        skillTrees[(int)SkillActType.Active].gameObject.SetActive(true);
    }
    #endregion

    #region Open Events
    public override void OpenSetUp(bool isSimple = false)
    {
        isSimpleMode = isSimple;
        DeactivePopUp();
        ActivePopUp(isSimpleMode);
    }

    public void SetSkillOnDetail(SkillIcon skillIcon)
    {
        selectSkillPopUp.SetSkillOnDetail(skillIcon);
    }

    public override void OpenDetailPopUp()
    {
        selectSkillPopUp.AddPopUpUI();
    }

    public void ChangeSkillType(SkillActType skillType)
    {
        if (skillActType == skillType) return;
        skillTrees[(int)skillActType].gameObject.SetActive(false);
        skillActType = skillType;
        skillTrees[(int)skillActType].gameObject.SetActive(true);
    }
    #endregion

    #region Disable Events
    public override void RemoveEvents()
    {
        SkillManager.Instance.skillMenuUI.ResetCurMenu();
        base.RemoveEvents();
    }
    #endregion

    #region Events
    public void MountSkill()
    {
        var skillIcon = selectSkillPopUp.curSkillIcon;
    }

    public SkillIcon GetSelectedSkill()
    {
        return selectSkillPopUp.curSkillIcon;
    }

    public void SetSkillDBOnIcon()
    {
        for(int i = 0; i < (int)SkillActType.End; i++)
        {
            skillTrees[i].SetSkillDBOnIcon(i);
            skillTrees[i].gameObject.SetActive(false);
        }
    }
    #endregion
}
