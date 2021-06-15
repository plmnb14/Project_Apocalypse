using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum skillCostType { Fixed, Percent, Reservation, End };
public enum AnimParamType { Boolen, Trigger, End };
public class SkillStatusDB
{
    public int skillID;
    public string skillName;
    public float duration;
    public float growthDuration;
    public float activeCount;
    public float growthActiveCount;
    public float percent;
    public float growthPercent;
    public float coolTime;
    public float growthCoolTime;
    public float cost;
    public float growthCost;
    public skillCostType costType;
    public bool isActiveSkill;
    public string explain;
}

public class SkillStatusSave
{
    public int skillID;
    public int level;
}

public class SkillStatusLocal
{
    public float duration;
    public float percent;
    public float coolTime;
    public float cost;
    public float activeCount;

    public void CopySkillDB(ref SkillDB skill)
    {
        duration = skill.duration;
        percent = skill.percent;
        coolTime = skill.coolTime;
        cost = skill.cost;
        activeCount = skill.activeCount;
    }
}

public class SkillDB : ScriptableObject
{
    [Header("Skill ID")]
    public int skillID;
    public int skillTier;
    public string skillName;
    public bool isActiveSkill;
    public Sprite skillIcon;
    [Space(3)]

    [Header("Skill Value")]
    public float duration;
    public float activeCount;
    public float percent;
    public float coolTime;
    public float cost;
    public skillCostType costType;
    [Space(3)]

    [Header("Skill Growth Value")]
    public float growthDuration;
    public float growthActiveCount;
    public float growthPercent;
    public float growthCoolTime;
    public float growthCost;
    [Space(3)]

    [Header("Skill Type Tag")]
    public bool isAttack;
    public bool isElement;
    public bool isProjectile;
    public bool isAura;
    [Space(3)]

    [Header("Skill Explanation")]
    [TextArea]
    public string DetailExplain;
    [TextArea]
    public string effectExplain;

    #region Events
    public virtual void ActiveSkill(Living user, Living target = null, Transform usePosition = null) { }
    public virtual void ChangeExplainText() { }
    public virtual void UpgradeValues() { }
    public virtual void ChangeSkillValues() { }
    #endregion
}
