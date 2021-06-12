using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlotButton : MonoBehaviour, IPointerClickHandler
{
    #region Public Fields
    public SkillWithCoreUI skillWithCoreUI;
    #endregion

    #region Click Events
    public void OnPointerClick(PointerEventData eventData)
    {
        skillWithCoreUI.PopUpSkillInven();
    }
    #endregion
}
