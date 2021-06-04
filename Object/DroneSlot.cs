using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroneSlot : MonoBehaviour, IPointerClickHandler
{
    #region Private Field
    private GameObject dismountImage;
    #endregion

    #region Property Field
    public DroneCard mountedDrone { get; set; }
    public bool isDroneMounted { get; set; }
    public Image mountedImage { get; set; }
    public int slotIndex { get; set; }
    #endregion

    #region Click Event
    public void OnPointerClick(PointerEventData eventData)
    {
        DroneManager droneManager = DroneManager.instance;
        if (isDroneMounted)
        {
            droneManager.pickSlotIndex = slotIndex;
            droneManager.SummonDrone(false);
            DismountDrone();
        }

        else
        {
            if (!droneManager.isPickMode)
            {
                droneManager.pickSlotIndex = slotIndex;
                droneManager.SetPickMode(true);
            }
            else
            {
                droneManager.SetPickMode(false);
            }
        }
    }
    #endregion

    #region Event
    public void MountDroneOnSlot(ref DroneCard mountDrone)
    {
        mountedDrone = mountDrone;
        mountedDrone.SetOnTeam(true);
        mountDrone.mountedSlotIndex = slotIndex;
        isDroneMounted = true;
        ChangeMountIcon(isDroneMounted);
    }

    public void DismountDrone()
    {
        // 드론에 해제되는 함수 호출하기
        mountedDrone.SetOnTeam(false);
        mountedDrone = null;
        isDroneMounted = false;
        ChangeMountIcon(isDroneMounted);
    }

    private void ChangeMountIcon(bool isMounted)
    {
        dismountImage.gameObject.SetActive(!isMounted);
        mountedImage.gameObject.SetActive(isMounted);
    }
    #endregion

    #region Awake Event
    private void AwakeSetUp()
    {
        isDroneMounted = false;
        dismountImage = transform.GetChild(1).gameObject;
        mountedImage = transform.GetChild(2).GetComponent<Image>();
        ChangeMountIcon(isDroneMounted);
    }
    private void Awake()
    {
        AwakeSetUp();
    }
    #endregion
}
