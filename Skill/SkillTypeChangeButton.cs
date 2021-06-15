using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTypeChangeButton : MonoBehaviour, IPointerClickHandler
{
    #region Public Fields
    public SkillInvenUI skillInvenUI;
    public SkillActType skillType;
    #endregion

    #region Click Events
    public void OnPointerClick(PointerEventData eventData)
    {
        skillInvenUI.ChangeSkillType(skillType);
    }
    #endregion
}
