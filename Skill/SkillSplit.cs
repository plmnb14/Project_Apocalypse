using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Split", menuName = "Skill/Split")]
public class SkillSplit : SkillDB
{
    public override void ActiveSkill(Living user, Living target = null, Transform usePosition = null)
    {
        //user.OnSkill(animParamType[]);
    }

    public void OnAction()
    {
        Debug.Log("»§!");
    }
}
