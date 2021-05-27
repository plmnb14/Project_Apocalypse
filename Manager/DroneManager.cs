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
    //private readonly int maxDroneSummonCount = 4;
    private Drone[] droneCards;
    private DroneDetailPopUp droneDetailPopUp;
    private PopUpUI popUpUI;
    #endregion

    #region Public Field
    public GameObject[] droneSpwanPoint;
    #endregion

    #region Click Event
    public void PopUpDetailUI(int slotIndex)
    {
        PopUpManager popUpManager = PopUpManager.instance;
        popUpManager.AddPopUp(droneDetailPopUp, true);
        droneDetailPopUp.CopyOriginDrone(ref droneCards[slotIndex]);
    }
    #endregion

    #region Awake Event
    private void LoadChildDrone()
    {
        GameObject droneGreed = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        for(int i = 0; i < maxDroneCardCount; i++)
        {
            droneCards[i] = droneGreed.transform.GetChild(i).GetComponent<Drone>();
            droneCards[i].slotIndex = i;
        }
    }

    private void AwakeSetUp()
    {
        droneDetailPopUp = transform.parent.GetChild(7).GetComponent<DroneDetailPopUp>();
        droneCards = new Drone[maxDroneCardCount];
    }

    private void Start()
    {
        for (int i = 0; i < maxDroneCardCount; i++)
        {
            droneCards[i].SetUnlock(false);
        }

        droneCards[0].SetUnlock(true);
        droneCards[1].SetUnlock(true);
    }

    private void Awake()
    {
        AwakeSetUp();
        LoadChildDrone();
    }
    #endregion
}
