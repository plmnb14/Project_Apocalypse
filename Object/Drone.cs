using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drone : MonoBehaviour, IPointerClickHandler
{
    #region Private Field
    private GameObject lockScreen;
    private GameObject mountedFrame;
    private Image dronePortrait;
    #endregion

    #region Property
    public bool isUnlock { get; set; }
    public bool isOnTeam { get; set; }
    public int slotIndex { get; set; }
    #endregion

    #region Click Event
    public void OnPointerClick(PointerEventData eventData)
    {
        DroneManager.instance.PopUpDetailUI(slotIndex);
    }
    #endregion

    #region Event
    public void DeepCopy(ref Drone drone)
    {
        SetUnlock(drone.isUnlock);
        if(isUnlock)
        {
            dronePortrait.sprite = drone.dronePortrait.sprite;
        }
    }
    #endregion

    #region Unlock Event
    public void SetUnlock(bool beUnlock)
    {
        isUnlock = beUnlock;
        UnlockActive();
    }

    private void UnlockActive()
    {
        lockScreen.gameObject.SetActive(!isUnlock);
        dronePortrait.gameObject.SetActive(isUnlock);
    }
    #endregion

    #region Awake
    private void LoadChild()
    {
        lockScreen = transform.GetChild(2).gameObject;
        mountedFrame = transform.GetChild(0).gameObject;
        dronePortrait = transform.GetChild(1).GetComponent<Image>();
    }

    private void AwakeSetUp()
    {
        isUnlock = false;
        isOnTeam = false;
    }

    private void Awake()
    {
        AwakeSetUp();
        LoadChild();
    }
    #endregion
}
