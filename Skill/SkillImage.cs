using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillImage : SkillSlotBase
{
    #region Enum
    private enum SlotType { SkillIcon, DismountIcon, End};
    #endregion

    #region Awake Events
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }

    protected override void AwakeSetUp()
    {
        base.AwakeSetUp();
        icons = new Image[(int)SlotType.End];
    }

    private void LoadChilds()
    {
        for(int i = 0; i < (int)SlotType.End; i++)
        {
            icons[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }
    #endregion

    #region Events
    public void ChangeImages(bool isMount = true)
    {
        isMounted = isMount;

        icons[(int)SlotType.SkillIcon].sprite = skill.originSkillDB.skillIcon;
        icons[(int)SlotType.SkillIcon].gameObject.SetActive(isMounted);
        icons[(int)SlotType.DismountIcon].gameObject.SetActive(!isMounted);
    }
    #endregion
}
