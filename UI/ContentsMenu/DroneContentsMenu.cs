using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneContentsMenu : ContentsMenu
{
    #region Public Fields
    #endregion

    #region Private Fields
    private DroneDetailUI droneDetailUI;
    private DroneLobbyUI droneLobbyUI;
    #endregion

    #region Events
    public void CopyDroneToDetail(ref DroneCard copyDrone)
    {
        droneDetailUI.CopyOriginDrone(ref copyDrone);
    }

    public void UpdateDroneInfo()
    {
        droneDetailUI.UpgradeDroneEvent();
    }

    public void PopUpDetailUI(ref DroneCard droneCard)
    {
        droneDetailUI.AddPopUpUI(true);
        droneDetailUI.CopyOriginDrone(ref droneCard);
    }
    #endregion

    #region Reset Events
    public void ResetPopUpActive(bool isActive)
    {
        droneLobbyUI.gameObject.SetActive(isActive);
        droneDetailUI.gameObject.SetActive(isActive);
    }
    public override void ResetOnEnable()
    {
        ResetPopUpActive(false);
        droneLobbyUI.AddPopUpUI();
    }
    #endregion

    #region Awake Event
    private void LoadChilds()
    {
        droneLobbyUI = transform.GetChild(0).GetComponent<DroneLobbyUI>();
        droneDetailUI = transform.GetChild(1).GetComponent<DroneDetailUI>();
    }

    private void AwakeSetUp()
    {

    }

    private void Awake()
    {
        LoadChilds();
    }
    #endregion
}
