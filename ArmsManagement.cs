using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmsManagement : PopUpUI
{
    #region Private Fields
    private const int maxSlotsCount = 18;
    private ArmsType currentPopUpArms;
    private Text currentArmsName;
    private Text currentArmsExplain;
    private Slider currentArmsHeldSlider;
    private PopUpUI dismountPanel;
    private ArmsSlot mountArmsSlot;
    private ArmsSlot[] armsSlotArray;
    private Arms[] armsArray;
    #endregion

    #region Events
    public void CopyArms(ref Arms copyArms)
    {

    }
    public void ChangeArmsType(ArmsType armsType)
    {
        currentPopUpArms = armsType;
    }

    public void ChangeArmsMenu(ref ArmsSlot armsSlot)
    {
        mountArmsSlot.CopySlots(ref armsSlot);
    }

    public void PopUpDismountPanel()
    {
        dismountPanel.AddPopUpUI();
    }
    #endregion

    #region Awake Events
    private void LoadChilds()
    {
        dismountPanel = transform.GetChild(3).GetComponent<PopUpUI>();
        mountArmsSlot = transform.GetChild(1).GetChild(0).GetComponent<ArmsSlot>();

        var greedObject = transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
        for(int i = 0; i < maxSlotsCount; i++)
        {
            armsSlotArray[i] = greedObject.transform.GetChild(i).GetComponent<ArmsSlot>();
            armsArray[i] = greedObject.transform.GetChild(i).GetComponent<Arms>();
        }

        currentArmsName = transform.GetChild(1).GetChild(1).GetComponent<Text>();
        currentArmsExplain = transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>();
    }
    private void AwakeSetUp()
    {
        currentPopUpArms = ArmsType.Head;
        isOpenOldPopUp = true;

        armsSlotArray = new ArmsSlot[maxSlotsCount];
        armsArray = new Arms[maxSlotsCount];
    }

    private void Start()
    {
        currentArmsName.text = DataManager.Instance.armsDBDic[2001].itemName;
        currentArmsExplain.text = DataManager.Instance.armsDBDic[2001].explain;
    }

    private void Awake()
    {
        AwakeSetUp();
        LoadChilds();
    }
    #endregion
}
