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
    public int cardIndex { get; set; }
    public int mountedSlotIndex { get; set; }
    #endregion

    #region Click Event
    public void OnPointerClick(PointerEventData eventData)
    {
        DroneManager droneManager = DroneManager.Instance;
        if (!DroneManager.Instance.isPickMode)
        {
            droneManager.PopUpDetailUI(cardIndex);
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
        mountedSlotIndex = -1;
        mountedFrame.gameObject.SetActive(isOnTeam);
    }

    public void SetPickOnTeam()
    {
        DroneManager droneManager = DroneManager.Instance;
        droneManager.PickedDrone(cardIndex);
        droneManager.SummonDrone(true, cardIndex);
        droneManager.SetPickMode(false);
    }
    #endregion

    #region Event
    public void UpdateStatus()
    {
        DataManger dataManager = DataManger.instance;
        float finalDamage = dataManager.droneStatusDBList[cardIndex].damagePercent + 
            dataManager.droneStatusDBList[cardIndex].growthDamagePercent * droneStatusForSave.tier;
        float finalAttackSpeed = dataManager.droneStatusDBList[cardIndex].attackSpeed + 
            dataManager.droneStatusDBList[cardIndex].growthAttackSpeed * droneStatusForSave.tier;

        droneStatusForLocal.damagePercent = finalDamage;
        droneStatusForLocal.attackSpeed = finalAttackSpeed;
    }

    public void DeepCopy(ref DroneCard droneCard)
    {
        SetUnlock(droneCard.isUnlock);
        if(isUnlock)
        {
            dronePortrait.sprite = droneCard.dronePortrait.sprite;
        }
        droneStatusForLocal = droneCard.droneStatusForLocal;
        droneStatusForSave = droneCard.droneStatusForSave;
    }

    public void SetStatsFromDataBase()
    {
        DataManger dataManager = DataManger.instance;
        droneStatusForSave.uniqueNumber = dataManager.droneStatusDBList[cardIndex].uniqueNumber;
        droneStatusForLocal.damagePercent = dataManager.droneStatusDBList[cardIndex].damagePercent;
        droneStatusForLocal.attackSpeed = dataManager.droneStatusDBList[cardIndex].attackSpeed;
        droneStatusForLocal.myName = dataManager.droneStatusDBList[cardIndex].myName;
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
    private void Start()
    {
        SetStatsFromDataBase();
    }

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
        mountedSlotIndex = -1;
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
