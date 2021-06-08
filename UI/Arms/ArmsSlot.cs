using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmsSlot : MonoBehaviour, IPointerClickHandler
{
    #region Public Fields
    public ArmsType armsType;
    #endregion

    #region Private Fields
    private bool isUnlock;
    private bool isNewEvent;
    private bool isEmpty;
    private Image dismountIcon;
    private Image mountIcon;
    private Image newAlert;
    private TierStar tierStar;
    private GameObject lockScreen;
    #endregion

    #region Click Events
    public void OnPointerClick(PointerEventData eventData)
    {
        ArmsManager.Instance.PopUpArmsManagement(armsType);
    }
    #endregion

    #region Events
    public void UnlockArms(bool unlock)
    {
        isUnlock = unlock;
        dismountIcon.gameObject.SetActive(isUnlock);
        newAlert.gameObject.SetActive(isUnlock);
        tierStar.gameObject.SetActive(isUnlock);
        lockScreen.gameObject.SetActive(!isUnlock);
    }

    #endregion

    #region Awake Events
    private void LoadChilds()
    {
        dismountIcon = transform.GetChild(0).GetComponent<Image>();
        mountIcon = transform.GetChild(1).GetComponent<Image>();
        lockScreen = transform.GetChild(2).gameObject;
        newAlert = transform.GetChild(3).GetComponent<Image>();
        tierStar = transform.GetChild(4).GetComponent<TierStar>();
    }
    private void AwakeSetUp()
    {
        isUnlock = false;
        isNewEvent = false;
    }
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }
    #endregion
}
