using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDetailUI : PopUpInnerUI
{
    #region Private Fields
    private readonly int maxSkillTag = 4;
    private SkillIcon curSkillIcon;
    private GameObject[] skillTags;
    private Image skillIconImg;
    private Text skillName;
    private Text skillTier;
    private Text skillLevel;
    private Text skillExplain;
    private Text skillValueExplain;
    #endregion

    #region Awake Events
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }

    private void AwakeSetUp()
    {
        curSkillIcon = null;
        skillTags = new GameObject[maxSkillTag];
    }

    private void LoadChilds()
    {
        var skillInfo = transform.GetChild(0).GetChild(1).gameObject;
        skillIconImg = skillInfo.transform.GetChild(0).GetComponent<Image>();
        skillName = skillInfo.transform.GetChild(1).GetComponent<Text>();
        skillTier = skillInfo.transform.GetChild(2).GetComponent<Text>();
        skillLevel = skillInfo.transform.GetChild(3).GetComponent<Text>();

        var skillTag = transform.GetChild(0).GetChild(2).gameObject;
        for(int i = 0; i < maxSkillTag; i++)
        {
            skillTags[i] = skillTag.transform.GetChild(i).gameObject;
        }

        skillExplain = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
        skillValueExplain = transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>();
    }
    #endregion

    #region Open Events
    public void SetSkillOnDetail(SkillIcon skillIcon)
    {
        if(curSkillIcon != skillIcon)
        {
            curSkillIcon = skillIcon;
            ChangeArts();
            ChangeTag();
        }
    }

    private void ChangeArts()
    {
        SkillDB skillDB = curSkillIcon.skill.originSkillDB;
        skillIconImg.sprite = skillDB.skillIcon;
        skillName.text = skillDB.skillName;
    }

    private void ChangeTag()
    {
        SkillDB skillDB = curSkillIcon.skill.originSkillDB;
        skillTags[0].SetActive(skillDB.isAttack ? true : false);
        skillTags[1].SetActive(skillDB.isProjectile ? true : false);
        skillTags[2].SetActive(skillDB.isElement ? true : false);
        skillTags[3].SetActive(skillDB.isAura ? true : false);
    }
    #endregion
}
