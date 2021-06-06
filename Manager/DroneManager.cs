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

    // 드론 프리팹들
    // 미리 소환된 드론들
    // 드론이 소환될 위치 4개

    #region Private Field
    private readonly int maxDroneCardCount = 15;
    private readonly int maxDroneSlotCount = 4;

    private Vector3[] droneSpawnVector;
    private DroneCard[] droneCards;
    private DroneSlot[] droneSlots;
    private Drone[] dronePrefabs;
    private Drone[] droneSummoned;
    private bool isHeroRunning;
    #endregion

    #region Property
    public bool isPickMode { get; set; }
    public int pickSlotIndex { get; set; }
    public int currentPopUpIndex { get; set; }
    #endregion

    #region Public Field
    //public Drone[] summonDrones { get; set; }
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
        isHeroRunning = isMove;
        for (int i = 0; i < maxDroneSlotCount; i++)
        {
            if(droneSlots[i].isDroneMounted)
            {
                int cardIndex = droneSlots[i].mountedDrone.cardIndex;
                droneSummoned[cardIndex].SetMoveAnimation(isMove);
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
        int cardIndex = droneSlots[pickSlotIndex].mountedDrone.cardIndex;
        droneSummoned[cardIndex].gameObject.SetActive(isSummon);

        if(isSummon)
        {
            droneSummoned[cardIndex].transform.position = droneSpawnVector[pickSlotIndex];
            droneSlots[pickSlotIndex].mountedImage.sprite = droneCards[slotIndex].dronePortrait.sprite;
            // 드론 스프라이트 변경하기
            if(isHeroRunning)
            {
                droneSummoned[cardIndex].SetMoveAnimation(isHeroRunning);
            }
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
                pickSlotIndex = droneMountedSlotIndex;
                SummonDrone(false);
                droneSlots[droneMountedSlotIndex].DismountDrone();
                droneDetailPopUp.CopyOriginDrone(ref droneCards[currentPopUpIndex]);
            }
        }
        else
        {
            PopUpManager.instance.ShowNotificationPopUp("N_1000");
        }
    }

    public void UpgradeDrone()
    {
        if(droneCards[currentPopUpIndex].isUnlock)
        {
            if (droneCards[currentPopUpIndex].droneStatusForSave.tier < 10)
            {
                droneCards[currentPopUpIndex].droneStatusForSave.tier += 1;
                droneCards[currentPopUpIndex].UpdateStatus();
                droneDetailPopUp.UpgradeDroneEvent();
            }

            else
            {
                PopUpManager.instance.ShowNotificationPopUp("N_1001");
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
    private void DroneInit(ref Drone drone)
    {
        drone.transform.position = Vector3.zero;
        drone.transform.parent = this.transform;
    }
    private void DroneInstnasiate(int slotIndex)
    {
        droneSummoned[slotIndex] =  Instantiate(dronePrefabs[slotIndex]);
        DroneInit(ref droneSummoned[slotIndex]);
        droneSummoned[slotIndex].gameObject.SetActive(false);
    }

    private void LoadPrefab(int slotIndex, string path)
    {
        dronePrefabs[slotIndex] = Resources.Load<Drone>(path);
    }

    private void LoadResources()
    {
        LoadPrefab(0, "Prefab/Drone0");
        LoadPrefab(1, "Prefab/Drone1");
        DroneInstnasiate(0);
        DroneInstnasiate(1);
    }

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
            droneSpawnVector[i] = transform.GetChild(i).transform.position;
        }
    }
    private void AwakeSetUp()
    {
        droneCards = new DroneCard[maxDroneCardCount];
        dronePrefabs = new Drone[maxDroneCardCount];
        droneSummoned = new Drone[maxDroneCardCount];
        droneSlots = new DroneSlot[maxDroneSlotCount];
        droneSpawnVector = new Vector3[maxDroneSlotCount];
        pickSlotIndex = 0;
        isPickMode = false;
    }

    private void Awake()
    {
        AwakeSetUp();
        LoadResources();
        LoadChildDrone();
    }
    #endregion
}
