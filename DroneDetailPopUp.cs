using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneDetailPopUp : PopUpUI
{
    #region Private Field
    private const int maxStatCount = 2;
    private Drone targetDrone;
    private Drone droneClone;
    private Text titleName;
    private Text[] currentStat;
    private Text[] nextStat;
    #endregion

    #region Update Event
    public void CopyOriginDrone(ref Drone copyDrone)
    {
        targetDrone = copyDrone;
        droneClone.DeepCopy(ref targetDrone);
    }
    #endregion

    #region Awake Event
    private void AwakeSetUp()
    {
        droneClone = transform.GetChild(2).GetComponent<Drone>();
        titleName = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        currentStat = new Text[maxStatCount];
        nextStat = new Text[maxStatCount];
        GameObject statTextPanel = transform.GetChild(3).GetChild(1).gameObject;
        for(int i = 0; i < maxStatCount; i++)
        {
            currentStat[i] = statTextPanel.transform.GetChild(2 + i).GetComponent<Text>();
            nextStat[i] = statTextPanel.transform.GetChild(4 + i).GetComponent<Text>();
        }
    }

    private void Awake()
    {
        AwakeSetUp();
    }
    #endregion
}
