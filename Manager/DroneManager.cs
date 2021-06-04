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

    #region Private Field
    private readonly int maxDroneCardCount = 15;
    private readonly int maxDroneSlotCount = 4;
    private DroneCard[] droneCards;
    private DroneSlot[] droneSlots;
    private DroneDetailPopUp droneDetailPopUp;
    #endregion

    #region Property
    public bool isPickMode { get; set; }
    public int pickSlotIndex { get; set; }
    public int currentPopUpIndex { get; set; }
    #endregion

    #region Public Field
    public SpriteRenderer[] droneSpwanPoint;
    #endregion

    #region Click Event
    public void PopUpDetailUI(int slotIndex)
    {
        currentPopUpIndex = slotIndex;

        PopUpManager popUpManager = PopUpManager.instance;
        popUpManager.AddPopUp(this.GetComponent<PopUpUI>());
        popUpManager.AddPopUp(droneDetailPopUp, true);
        droneDetailPopUp.CopyOriginDrone(ref droneCards[slotIndex]);
    }
    #endregion

    #region Event
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
        droneSpwanPoint[pickSlotIndex].gameObject.SetActive(isSummon);
        if(isSummon)
        {
            droneSlots[pickSlotIndex].mountedImage.sprite = droneCards[slotIndex].dronePortrait.sprite;
            droneSpwanPoint[pickSlotIndex].sprite = droneCards[slotIndex].dronePortrait.sprite;
        }
    }

    public void MountDrone()
    {
        if(droneCards[currentPopUpIndex].isUnlock)
        {
            if (!droneCards[currentPopUpIndex].isOnTeam)
            {
                bool isFull = false;
                for (int i = 0; i < maxDroneSlotCount; i++)
                {
                    if (!droneSlots[i].isDroneMounted)
                    {
                        pickSlotIndex = i;
                        isFull = true;
                        break;
                    }
                }

                if (isFull)
                {
                    droneCards[currentPopUpIndex].SetPickOnTeam();
                    droneDetailPopUp.CopyOriginDrone(ref droneCards[currentPopUpIndex]);
                }
            }
        }
        else
        {
            Debug.Log("소유하지 않은 \n 동료입니다.");
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
    }
    #endregion

    #region Awake Event
    private void LoadChildDrone()
    {
        GameObject droneGreed = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        for (int i = 0; i < maxDroneCardCount; i++)
        {
            droneCards[i] = droneGreed.transform.GetChild(i).GetComponent<DroneCard>();
            droneCards[i].slotIndex = i;
        }

        GameObject droneSlotGreed = transform.GetChild(1).GetChild(1).gameObject;
        for(int i = 0; i < maxDroneSlotCount; i++)
        {
            droneSlots[i] = droneSlotGreed.transform.GetChild(i).GetComponent<DroneSlot>();
            droneSlots[i].slotIndex = i;
        }
    }
    private void AwakeSetUp()
    {
        droneDetailPopUp = transform.parent.GetChild(7).GetComponent<DroneDetailPopUp>();
        droneCards = new DroneCard[maxDroneCardCount];
        droneSlots = new DroneSlot[maxDroneSlotCount];
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
