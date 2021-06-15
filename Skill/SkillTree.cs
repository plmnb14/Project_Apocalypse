using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    #region Private Fields
    private readonly int maxSkillCnt = 4;
    private readonly int maxSkillTreeCnt = 5;
    private SkillIcon[,] skillTrees;
    #endregion

    #region Awake Events
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }

    private void AwakeSetUp()
    {
        skillTrees = new SkillIcon[maxSkillTreeCnt, maxSkillCnt];
    }

    private void LoadChilds()
    {
        var skillTreeGreed = transform.GetChild(1).GetChild(0).gameObject;
        for(int i = 0; i < maxSkillTreeCnt; i++)
        {
            var innerTree = skillTreeGreed.transform.GetChild(i).GetChild(1).gameObject;
            for(int j = 0; j < maxSkillCnt; j++)
            {
                skillTrees[i, j] = innerTree.transform.GetChild(j).GetComponent<SkillIcon>();
                skillTrees[i, j].gameObject.SetActive(true);
            }
        }
    }
    #endregion

    #region Start Events
    private void Start()
    {
        DeactiveSkillTree();
    }

    private void DeactiveSkillTree()
    {
        for (int i = 0; i < maxSkillTreeCnt; i++)
        {
            for (int j = 0; j < maxSkillCnt; j++)
            {
                if(skillTrees[i, j].skillTreeIndex == -1)
                    skillTrees[i, j].gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region Events
    public void SetSkillDBOnIcon(int DBidx)
    {
        var skills = SkillManager.Instance.skillDBDic[DBidx];
        foreach (var skill in skills)
        {
            for (int i = 0; i < maxSkillCnt; i++)
            {
                if (skillTrees[skill.Value.skillTier, i].skillTreeIndex == -1)
                {
                    skillTrees[skill.Value.skillTier, i].skillTreeIndex = i;
                    skillTrees[skill.Value.skillTier, i].gameObject.SetActive(true);
                    skillTrees[skill.Value.skillTier, i].skill.originSkillDB = skill.Value;
                    skillTrees[skill.Value.skillTier, i].ChangeSkillArts();
                    break;
                }
            }
        }
    }
    #endregion
}
