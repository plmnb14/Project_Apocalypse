using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmsStatusForSave
{
    public int tier;
    public int heldCount;
}

public class ArmsStatusForDB
{
    public int itemCode;
    public int armsType;
    public float baseValue;
    public float growthValue;
    public float duration;
    public string itemName;
    public string explain;
}

public class Arms : MonoBehaviour, IPointerClickHandler
{
    #region Property Fields
    public Sprite spriteIcon;
    #endregion

    #region Click Events
    public void OnPointerClick(PointerEventData eventData)
    {
        ArmsManager.Instance.PopUpArmsMount();
    }
    #endregion

    #region Awake Events
    private void AwakeSetUp()
    {
        spriteIcon = Resources.Load<Sprite>("Sprite/UI/EquipIcon");
    }

    private void Awake()
    {
        AwakeSetUp();
    }
    #endregion
}
