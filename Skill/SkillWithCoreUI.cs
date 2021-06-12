using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWithCoreUI : MonoBehaviour
{
    #region Public Fields
    public int skillSlotIndex;
    #endregion

    #region Private Fields
    private SkillSlotUI skillSlotUI;
    private GameObject coreSlotsUI;
    private Text skillName;
    private Text typeName;
    #endregion

    #region Awake Events;
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }

    private void AwakeSetUp()
    {

    }

    private void LoadChilds()
    {
        skillSlotUI = transform.GetChild(1).GetComponent<SkillSlotUI>();
        coreSlotsUI = transform.GetChild(2).gameObject;
        skillName = transform.GetChild(3).GetComponent<Text>();
        typeName = transform.GetChild(4).GetComponent<Text>();

    }
    #endregion

    #region Public Events
    public void PopUpSkillInven()
    {
        var skillMenuUI = SkillManager.Instance.skillMenuUI;
        //skillMenuUI.SetSkillSlotIndex(skillSlotIndex);
        skillMenuUI.OpenSkillUI(SkillMenuUI.SkillMenuEnum.SkillInven, true);
    }
    #endregion
}
