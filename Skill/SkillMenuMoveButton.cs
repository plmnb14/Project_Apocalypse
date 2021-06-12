using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillMenuMoveButton : MonoBehaviour, IPointerClickHandler
{
    #region Public Fields
    public SkillMenuUI.SkillMenuEnum skillMenu;
    public bool isSimple;
    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        var skillMenuUI = SkillManager.Instance.skillMenuUI;
        skillMenuUI.OpenSkillUI(skillMenu, isSimple);
    }
}
