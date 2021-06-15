using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillIcon : SkillSlotBase, IPointerClickHandler
{
    #region Enum
    private enum IconType { Skill, Mount, Lock, End }
    #endregion

    #region Property Fields
    public int mountSlotIndex { get; set; }
    public int skillTreeIndex { get; set; }
    #endregion

    #region Private Fields
    private Text levelText;
    #endregion

    #region Click Events
    public void OnPointerClick(PointerEventData eventData)
    {
        var skillMenu = SkillMenuUI.SkillMenuEnum.SkillInven;
        var skillMenuUI = SkillManager.Instance.skillMenuUI;
        skillMenuUI.OpenSkillDetailUI(skillMenu);
        skillMenuUI.SetSkillIconOnDetail(this);
    }
    #endregion

    #region Awake Events
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
        OnDefault();
    }

    protected override void AwakeSetUp()
    {
        base.AwakeSetUp();
        icons = new Image[(int)IconType.End];
        skill = GetComponent<Skill>();
        mountSlotIndex = -1;
        skillTreeIndex = -1;
    }

    private void LoadChilds()
    {
        for(int i = 0; i < (int)IconType.End; i++)
        {
            icons[i] = transform.GetChild(i).GetComponent<Image>();
        }

        levelText = transform.GetChild(3).GetComponent<Text>();
    }

    private void OnDefault()
    {
        icons[(int)IconType.Skill].gameObject.SetActive(true);
        icons[(int)IconType.Mount].gameObject.SetActive(false);
        icons[(int)IconType.Lock].gameObject.SetActive(true);
    }
    #endregion

    #region Events
    public override void UnlockSkillSlot()
    {
        base.UnlockSkillSlot();

        levelText.gameObject.SetActive(true);
        icons[(int)IconType.Lock].gameObject.SetActive(false);
    }

    public void ChangeSkillArts()
    {
        icons[(int)IconType.Skill].sprite = skill.originSkillDB.skillIcon;
        levelText.text = string.Format("lv.{0}", skill.skillStatusSave.level);
    }
    #endregion
}
