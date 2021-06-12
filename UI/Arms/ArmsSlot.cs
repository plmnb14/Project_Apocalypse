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
    private Arms mountedArms;
    #endregion

    #region Property
    public int slotIndex { get; set; }
    #endregion

    #region Click Events
    public void OnPointerClick(PointerEventData eventData)
    {
        ArmsManager.Instance.PopUpArmsManagement(armsType, slotIndex);
    }
    #endregion

    #region Events
    public void MountArms(ref Arms arms)
    {
        mountedArms = arms;
        mountIcon.sprite = mountedArms.spriteIcon;

        tierStar.gameObject.SetActive(true);
        mountIcon.gameObject.SetActive(true);
        dismountIcon.gameObject.SetActive(false);
    }

    public void DismountArms()
    {
        mountedArms = null;
        mountIcon.sprite = null;

        tierStar.gameObject.SetActive(false);
        mountIcon.gameObject.SetActive(false);
        dismountIcon.gameObject.SetActive(true);
    }

    public void CopySlots(ref ArmsSlot armsSlot)
    {
        UnlockArms(armsSlot.isUnlock);
        CopyArms(ref armsSlot);
    }

    private void CopyArms(ref ArmsSlot armsSlot)
    {
        if(null != armsSlot.mountedArms)
        {
            isEmpty = false;
            MountArms(ref armsSlot.mountedArms);
        }
        else
        {
            isEmpty = true;
            tierStar.gameObject.SetActive(false);
            mountIcon.gameObject.SetActive(false);
        }

        tierStar.gameObject.SetActive(!isEmpty);
    }

    public void UnlockArms(bool unlock)
    {
        isUnlock = unlock;
        dismountIcon.gameObject.SetActive(isUnlock);
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
        isEmpty = true;
        mountedArms = null;
    }
    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }
    #endregion
}
