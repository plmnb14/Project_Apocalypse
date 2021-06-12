using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillCoreIcon : MonoBehaviour, IPointerClickHandler
{
    #region Click Evnets
    public void OnPointerClick(PointerEventData eventData)
    {
        var skillMenu = SkillMenuUI.SkillMenuEnum.CoreInven;
        SkillManager.Instance.skillMenuUI.OpenSkillDetailUI(skillMenu);
    }
    #endregion
}
