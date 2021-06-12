using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillIcon : MonoBehaviour, IPointerClickHandler
{
    #region Enum
    private enum IconType { Skill, Mount, Lock, End }
    #endregion

    #region Private Fields
    private bool isMounted;
    private bool isUnlock;
    private Image[] icons;
    private Text levelText;
    #endregion

    #region Property Fields
    public Skill skill { get; set; }
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

    private void AwakeSetUp()
    {
        isMounted = false;
        icons = new Image[(int)IconType.End];
        skill = GetComponent<Skill>();
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

    #region Start Events
    private void Start()
    {
        StartSetUp();
    }

    private void StartSetUp()
    {
        skill.LoadSkillDB();
        ChangeSkillArts();
    }

    public void ChangeSkillArts()
    {
        icons[(int)IconType.Skill].sprite = skill.originSkillDB.skillIcon;
        levelText.text = string.Format("lv.{0}", skill.skillStatusSave.level);
    }
    #endregion

    #region Events
    public void UnlockSkill()
    {
        isUnlock = true;
        levelText.gameObject.SetActive(true);
        icons[(int)IconType.Lock].gameObject.SetActive(false);
    }
    #endregion
}
