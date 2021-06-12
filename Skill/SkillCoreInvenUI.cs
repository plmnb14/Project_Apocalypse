using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoreInvenUI : PopUpInnerUI
{
    #region Enum
    private enum InvenTypeEnum { Simple, Full, End };
    #endregion

    #region Private Fields
    private InvenTypeEnum barTypeEnum;
    private GameObject[] coreInven;
    private PopUpUI selectCorePopUp;
    #endregion

    #region Awake Evenets
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }

    private void AwakeSetUp()
    {
        barTypeEnum = InvenTypeEnum.Simple;

        coreInven = new GameObject[(int)InvenTypeEnum.End];
    }

    private void LoadChilds()
    {
        for(int i = 0; i < (int)InvenTypeEnum.End; i++)
        {
            coreInven[i] = transform.GetChild(i).gameObject;
        }

        selectCorePopUp = transform.GetChild(2).GetComponent<PopUpUI>();
    }
    #endregion

    #region Reset Events
    private void DeactivePopUp()
    {
        for (int i = 0; i < (int)InvenTypeEnum.End; i++)
        {
            coreInven[i].gameObject.SetActive(false);
        }

        selectCorePopUp.gameObject.SetActive(false);
    }

    private void ActivePopUp(bool isSimple)
    {
        InvenTypeEnum openType = isSimple ? InvenTypeEnum.Simple : InvenTypeEnum.Full;
        coreInven[(int)openType].SetActive(true);
    }
    #endregion

    #region Open Events
    public override void OpenSetUp(bool isSimple = false)
    {
        DeactivePopUp();
        ActivePopUp(isSimple);
    }

    public override void OpenDetailPopUp()
    {
        selectCorePopUp.AddPopUpUI();
    }
    #endregion

    #region Disable Events
    public override void RemoveEvents()
    {
        SkillManager.Instance.skillMenuUI.ResetCurMenu();
        base.RemoveEvents();
    }
    #endregion
}
