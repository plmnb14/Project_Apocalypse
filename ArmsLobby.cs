using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsLobby : PopUpUI
{
    #region Private Fields
    private const int maxSlotCount = 6;
    private int currentSlotCount;
    private GameObject playerAvatar;
    #endregion
    #region Property Fields
    public ArmsSlot[] armsSlots { get; set; }
    #endregion

    #region Events

    #endregion

    #region Awake Events
    public void ArmsSlotUnlock()
    {
        for (int i = 0; i < currentSlotCount; i++)
        {
            armsSlots[i].UnlockArms(true);
        }

        for (int i = currentSlotCount; i < maxSlotCount; i++)
        {
            armsSlots[i].UnlockArms(false);
        }
    }

    private void LoadChilds()
    {
        playerAvatar = transform.GetChild(0).gameObject;

        var slotGreed = transform.GetChild(1).gameObject;
        for(int i = 0; i < maxSlotCount; i++)
        {
            armsSlots[i] = slotGreed.transform.GetChild(i).GetComponent<ArmsSlot>();
            armsSlots[i].slotIndex = i;
        }
    }

    private void AwakeSetUp()
    {
        currentSlotCount = 4;
        armsSlots = new ArmsSlot[maxSlotCount];
    }

    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }

    private void Start()
    {
        ArmsSlotUnlock();
        var arms = ArmsManager.Instance.armsList[0][0];
        armsSlots[0].MountArms(ref arms);
    }
    #endregion
}
