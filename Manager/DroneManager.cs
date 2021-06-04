using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    #region Instnace Field
    static public DroneManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<DroneManager>();
            }
            return m_instance;
        }
    }
    static private DroneManager m_instance;
    #endregion

    #region Public Field
    public PopUpUI droneCanvas;
    public DroneDetailPopUp droneDetailPopUp;
    #endregion

    #region Private Field
    private readonly int maxDroneCardCount = 15;
    private readonly int maxDroneSlotCount = 4;

    private DroneCard[] droneCards;
    private DroneSlot[] droneSlots;
    #endregion

    #region Property
    public bool isPickMode { get; set; }
    public int pickSlotIndex { get; set; }
    public int currentPopUpIndex { get; set; }
    #endregion

    #region Public Field
    public Drone[] summonDrones { get; set; }
    #endregion

    #region Click Event
    public void PopUpDetailUI(int slotIndex)
    {
        currentPopUpIndex = slotIndex;

        PopUpManager popUpManager = PopUpManager.instance;
        popUpManager.AddPopUp(droneCanvas);
        popUpManager.AddPopUp(droneDetailPopUp, true);
        droneDetailPopUp.CopyOriginDrone(ref droneCards[slotIndex]);
    }
    #endregion

    #region Event
    public void MoveDrones(bool isMove)
    {
        for(int i = 0; i < maxDroneSlotCount; i++)
        {
            if(droneSlots[i].isDroneMounted)
            {
                summonDrones[i].SetMoveAnimation(isMove);
            }
        }
    }

    public void SetPickMode(bool isDronePick)
    {
        isPickMode = isDronePick;
    }

    public void PickedDrone(int slotIndex)
    {
        droneSlots[pickSlotIndex].MountDroneOnSlot(ref droneCards[slotIndex]);
    }

    public void SummonDrone(bool isSummon, int slotIndex = 0)
    {
        summonDrones[pickSlotIndex].gameObject.SetActive(isSummon);
        if(isSummon)
        {
            droneSlots[pickSlotIndex].mountedImage.sprite = droneCards[slotIndex].dronePortrait.sprite;
            // 드론 스프라이트 변경하기
        }
    }

    public void MountDrone()
    {
        if(droneCards[currentPopUpIndex].isUnlock)
        {
            if (!droneCards[currentPopUpIndex].isOnTeam)
            {
                bool isNotFull = false;
                for (int i = 0; i < maxDroneSlotCount; i++)
                {
                    if (!droneSlots[i].isDroneMounted)
                    {
                        pickSlotIndex = i;
                        isNotFull = true;
                        break;
                    }
                }

                if (isNotFull)
                {
                    droneCards[currentPopUpIndex].SetPickOnTeam();
                    droneDetailPopUp.CopyOriginDrone(ref droneCards[currentPopUpIndex]);
                }
            }

            else
            {
                int droneMountedSlotIndex = droneCards[currentPopUpIndex].mountedSlotIndex;
                DroneManager droneManager = DroneManager.instance;
                droneManager.pickSlotIndex = droneMountedSlotIndex;
                droneManager.SummonDrone(false);
                droneSlots[droneMountedSlotIndex].DismountDrone();
                droneDetailPopUp.CopyOriginDrone(ref droneCards[currentPopUpIndex]);
            }
        }
        else
        {
            PopUpManager.instance.ShowNotificationPopUp("N_1000");
        }
    }
    #endregion

    #region Pick Event
    //public void 
    #endregion

    #region Start Event
    private void Start()
    {
        for (int i = 0; i < maxDroneCardCount; i++)
        {
            droneCards[i].SetUnlock(false);
        }

        droneCards[0].SetUnlock(true);
        droneCards[1].SetUnlock(true);
        droneCards[2].SetUnlock(true);
        droneCards[3].SetUnlock(true);
    }
    #endregion

    #region Awake Event
    private void LoadChildDrone()
    {
        GameObject droneGreed = droneCanvas.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        for (int i = 0; i < maxDroneCardCount; i++)
        {
            droneCards[i] = droneGreed.transform.GetChild(i).GetComponent<DroneCard>();
            droneCards[i].cardIndex = i;
        }

        GameObject droneSlotGreed = droneCanvas.transform.GetChild(1).GetChild(1).gameObject;
        for(int i = 0; i < maxDroneSlotCount; i++)
        {
            droneSlots[i] = droneSlotGreed.transform.GetChild(i).GetComponent<DroneSlot>();
            droneSlots[i].slotIndex = i;
            summonDrones[i] = transform.GetChild(i).GetComponent<Drone>();
        }
    }
    private void AwakeSetUp()
    {
        droneCards = new DroneCard[maxDroneCardCount];
        droneSlots = new DroneSlot[maxDroneSlotCount];
        summonDrones = new Drone[maxDroneSlotCount];
        pickSlotIndex = 0;
        isPickMode = false;
    }

    private void Awake()
    {
        AwakeSetUp();
        LoadChildDrone();
    }
    #endregion
}
