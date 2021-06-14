using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    #region Public Fields
    public readonly int maxSkillCount = 6;
    public int curSkillCount = 2;
    public SkillMenuUI skillMenuUI;
    public PlayerSkillSlot playerSkillSlot;
    #endregion

    #region Property Fields
    public Dictionary<int, SkillDB> skillDBDic { get; private set; }
    #endregion

    #region Awake Events
    private void AwakeSetUp()
    {
        skillDBDic = new Dictionary<int, SkillDB>();
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

        AwakeSetUp();
        LoadSkillDB();
    }

    private void LoadSkillDB()
    {
        var skillSplit = Resources.Load<SkillDB>("SkillDB/Skill_Split");
        skillSplit.explain = skillSplit.explain.Replace("\\n", "\n");
        skillDBDic.Add(skillSplit.skillID, skillSplit);

        var skillHatred = Resources.Load<SkillDB>("SkillDB/Skill_Hatred");
        skillHatred.explain = skillHatred.explain.Replace("\\n", "\n");
        skillDBDic.Add(skillHatred.skillID, skillHatred);
    }
    #endregion

    #region Start Events
    private void Start()
    {
        playerSkillSlot.UnlockSkillSlot();
        //다음으로 저장된 플레이어 스킬 정보 불러오기
    }
    #endregion

    #region Public Events
    public void MountSkill()
    {
        skillMenuUI.MountSkill();
    }

    public void SetMountMode(bool isActive = true)
    {
        if(isActive)
        {
            skillMenuUI.SetMountMode();
        }
        else
        {
            PopUpManager.instance.RemovePopUp(2);
        }

        playerSkillSlot.SetMountMode(isActive);
    }

    public SkillIcon GetSelectedSkill()
    {
        return skillMenuUI.GetSelectedSkill();
    }
    #endregion
}
