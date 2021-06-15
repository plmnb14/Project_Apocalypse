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
    private SkillImage skillImage;
    private GameObject coreSlotsUI;
    private Text skillName;
    private Text typeName;
    private GameObject lockScreen;
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
        skillImage = transform.GetChild(1).GetComponent<SkillImage>();
        coreSlotsUI = transform.GetChild(2).gameObject;
        skillName = transform.GetChild(3).GetComponent<Text>();
        typeName = transform.GetChild(4).GetComponent<Text>();
        lockScreen = transform.GetChild(6).gameObject;

    }
    #endregion

    #region Events
    public void PopUpSkillInven()
    {
        var skillMenuUI = SkillManager.Instance.skillMenuUI;
        skillMenuUI.OpenSkillUI(SkillMenuUI.SkillMenuEnum.SkillInven, true);
        skillMenuUI.openSkillSlotIdx = skillSlotIndex;
    }

    public void ChangeSkillIcon(ref SkillIcon skillIcon)
    {
        skillImage.skill = skillIcon.skill;
        skillImage.ChangeImages();
    }

    public void DismountSkillImage()
    {
        skillImage.ChangeImages(false);
    }

    public void LockSlot(bool isLocked = true)
    {
        lockScreen.gameObject.SetActive(isLocked);
        skillName.gameObject.SetActive(!isLocked);
        typeName.gameObject.SetActive(!isLocked);
    }
    #endregion
}
