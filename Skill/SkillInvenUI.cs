using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvenUI : PopUpInnerUI
{
    #region Enum
    private enum BarTypeEnum { Simple, Full, End };
    public enum SkillTypeEnum { Active, Passive, Buff, End};
    #endregion

    #region Private Fields
    private BarTypeEnum barTypeEnum;
    private SkillTypeEnum skillTypeEnum;
    private GameObject[] typeBar;
    private GameObject[] skillView;
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
        barTypeEnum = BarTypeEnum.Simple;
        skillTypeEnum = SkillTypeEnum.Active;

        typeBar = new GameObject[(int)BarTypeEnum.End];
        skillView = new GameObject[(int)SkillTypeEnum.End];
    }

    private void LoadChilds()
    {
        for(int i = 0; i < (int)BarTypeEnum.End; i++)
        {
            typeBar[i] = transform.GetChild(i + 1).gameObject;
        }

        for (int i = 0; i < (int)SkillTypeEnum.End; i++)
        {
            skillView[i] = transform.GetChild(i + 3).gameObject;
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

        for (int i = 0; i < (int)SkillTypeEnum.End; i++)
        {
            skillView[i].SetActive(false);
        }

        selectSkillPopUp.gameObject.SetActive(false);
    }

    private void ActivePopUp(bool isSimple)
    {
        BarTypeEnum openType = isSimple ? BarTypeEnum.Simple : BarTypeEnum.Full;
        typeBar[(int)openType].SetActive(true);
        skillView[(int)SkillTypeEnum.Active].SetActive(true);
    }
    #endregion

    #region Open Events
    public override void OpenSetUp(bool isSimple = false)
    {
        DeactivePopUp();
        ActivePopUp(isSimple);
    }

    public void SetSkillOnDetail(SkillIcon skillIcon)
    {
        selectSkillPopUp.SetSkillOnDetail(skillIcon);
    }

    public override void OpenDetailPopUp()
    {
        selectSkillPopUp.AddPopUpUI();
    }

    public void ChangeSkillType(SkillTypeEnum skillType)
    {
        if (skillTypeEnum == skillType) return;
        skillView[(int)skillTypeEnum].SetActive(false);
        skillTypeEnum = skillType;
        skillView[(int)skillTypeEnum].SetActive(true);
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
    #endregion
}
