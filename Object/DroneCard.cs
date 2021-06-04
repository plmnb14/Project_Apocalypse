using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DroneStatusForDB
{
    public int uniqueNumber;
    public string myName;
    public float damagePercent;
    public float growthDamagePercent;
    public float attackSpeed;
    public float growthAttackSpeed;
}

public class DroneStatusForSave
{
    public int uniqueNumber;
    public int tier;
}

public class DroneStatusForLocal
{
    public string myName;
    public float damagePercent;
    public float attackSpeed;
}

public class DroneCard : MonoBehaviour, IPointerClickHandler
{
    #region Private Field
    private GameObject lockScreen;
    private GameObject mountedFrame;
    #endregion

    #region Property
    public Image dronePortrait { get; set; }
    public DroneStatusForLocal droneStatusForLocal { get; set; }
    public DroneStatusForSave droneStatusForSave { get; set; }
    public bool isUnlock { get; set; }
    public bool isOnTeam { get; set; }
    public int slotIndex { get; set; }
    #endregion

    #region Click Event
    public void OnPointerClick(PointerEventData eventData)
    {
        DroneManager droneManager = DroneManager.instance;
        if (!DroneManager.instance.isPickMode)
        {
            droneManager.PopUpDetailUI(slotIndex);
        }
        else
        {
            if(!isOnTeam)
            {
                SetOnTeam(true);
                SetPickOnTeam();
            }
            else
            {
                // 이미 선택된 친구입니다.
            }
        }
    }

    public void SetOnTeam(bool isOn)
    {
        isOnTeam = isOn;
        mountedFrame.gameObject.SetActive(isOnTeam);
    }

    public void SetPickOnTeam()
    {
        DroneManager droneManager = DroneManager.instance;
        droneManager.PickedDrone(slotIndex);
        droneManager.SummonDrone(true, slotIndex);
        droneManager.SetPickMode(false);
    }
    #endregion

    #region Event
    public void DeepCopy(ref DroneCard droneCard)
    {
        SetUnlock(droneCard.isUnlock);
        if(isUnlock)
        {
            dronePortrait.sprite = droneCard.dronePortrait.sprite;
        }
    }

    public void SetStatsFromDataBase()
    {
        DataManger dataManager = DataManger.instance;
        droneStatusForSave.uniqueNumber = dataManager.droneStatusDBList[slotIndex].uniqueNumber;
        droneStatusForLocal.damagePercent = dataManager.droneStatusDBList[slotIndex].damagePercent;
        droneStatusForLocal.attackSpeed = dataManager.droneStatusDBList[slotIndex].attackSpeed;
        droneStatusForLocal.myName = dataManager.droneStatusDBList[slotIndex].myName;
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
    private void DebugStatusSetUp()
    {
        droneStatusForSave.uniqueNumber = 1000;
        droneStatusForSave.tier = 1;

        droneStatusForLocal.damagePercent = 10.0f;
        droneStatusForLocal.attackSpeed = 0.15f;
    }

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
        droneStatusForLocal = new DroneStatusForLocal();
        droneStatusForSave = new DroneStatusForSave();
        DebugStatusSetUp();
    }

    private void Awake()
    {
        AwakeSetUp();
        LoadChild();
    }
    #endregion
}
