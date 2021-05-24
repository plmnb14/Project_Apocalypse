using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDetail : MonoBehaviour
{
    private Text equipmentTitleName;
    private Equipment myEquipment;
    private Equipment targetEquipment;

    private Text[] equipStatusName;
    private Text[] equipStatusCur;
    private Text[] equipStatusNext;

    private Text[] heldStatusName;
    private Text[] heldStatusCur;
    private Text[] heldStatusNext;

    private Text enchantStatusName;
    private Text enchantStatusCur;
    private Text enchantStatusNext;

    private GameObject equipButton;
    private GameObject enchantButton;
    private GameObject advenceButton;
    private GameObject reinforceButton;

    public void UpdateDetailInfo(ref Equipment equipment)
    {
        targetEquipment = equipment;
        equipmentTitleName.text = equipment.equipmentName.text;
        myEquipment.UpdateCopy(ref targetEquipment);
    }

    private void AwakeSetUp()
    {
        equipmentTitleName = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        myEquipment = transform.GetChild(2).GetComponent<Equipment>();

        equipStatusName = new Text[4];
        for(int i = 0; i < 4; i++) equipStatusName[i] = transform.GetChild(3).GetChild(i + 1).GetComponent<Text>();
        equipStatusCur = new Text[4];
        for (int i = 0; i < 4; i++) equipStatusCur[i] = transform.GetChild(3).GetChild(i + 5).GetComponent<Text>();
        equipStatusNext = new Text[4];
        for (int i = 0; i < 4; i++) equipStatusNext[i] = transform.GetChild(3).GetChild(i + 9).GetComponent<Text>();

        heldStatusName = new Text[3];
        for (int i = 0; i < 3; i++) heldStatusName[i] = transform.GetChild(4).GetChild(i + 1).GetComponent<Text>();
        heldStatusCur = new Text[3];
        for (int i = 0; i < 3; i++) heldStatusCur[i] = transform.GetChild(4).GetChild(i + 4).GetComponent<Text>();
        heldStatusNext = new Text[3];
        for (int i = 0; i < 3; i++) heldStatusNext[i] = transform.GetChild(4).GetChild(i + 7).GetComponent<Text>();

        enchantStatusName = transform.GetChild(5).GetChild(1).GetComponent<Text>();
        enchantStatusCur = transform.GetChild(5).GetChild(2).GetComponent<Text>();
        enchantStatusNext = transform.GetChild(5).GetChild(3).GetComponent<Text>();

        equipButton = transform.GetChild(5).gameObject;
        enchantButton = transform.GetChild(6).gameObject;
        advenceButton = transform.GetChild(7).gameObject;
        reinforceButton = transform.GetChild(8).gameObject;
    }
    private void Awake()
    {
        AwakeSetUp();
    }
}
