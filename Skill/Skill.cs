using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    #region Public Fields
    public int defaultSkillID;
    #endregion

    #region Property Fields
    public SkillDB originSkillDB { get; set; }
    public SkillStatusLocal skillStatusLocal { get; set; }
    public SkillStatusSave skillStatusSave { get; set; }
    #endregion

    #region Awake Events
    private void Awake()
    {
        AwakeSetUp();
    }

    private void AwakeSetUp()
    {
        skillStatusLocal = new SkillStatusLocal();
        skillStatusSave = new SkillStatusSave();
    }
    #endregion

    #region LevelUp Events
    public void LevelUp(int addLevel = 1)
    {
        skillStatusSave.level += addLevel;
        LevelUpEvent();
    }

    private void LevelUpEvent()
    {
        int level = skillStatusSave.level - 1;
        skillStatusLocal.duration = originSkillDB.duration + level * originSkillDB.growthDuration;
        skillStatusLocal.percent = originSkillDB.percent + level * originSkillDB.growthPercent;
        skillStatusLocal.coolTime = originSkillDB.coolTime + level * originSkillDB.growthCoolTime;
        skillStatusLocal.cost = originSkillDB.cost + level * originSkillDB.growthCost;
        skillStatusLocal.activeCount = originSkillDB.activeCount + level * originSkillDB.growthActiveCount;
    }
    #endregion
}
