using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    #region Public Fields
    [SerializeField]
    public SkillMenuUI skillMenuUI { get; set; }
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
}
