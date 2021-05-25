using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDetailInfo : MonoBehaviour
{
    public enum FunctionIndex { Equip, Reinforce, Promote, Inchant, FunctionIndex_End }

    private WeaponManager weaponManager;

    private Text equipmentTitleName;
    private Weapon weaponClone;
    private Weapon targetEquipment;

    private Text[] equipStatusName;
    private Text[] equipStatusCur;
    private Text[] equipStatusNext;

    private Text[] heldStatusName;
    private Text[] heldStatusCur;
    private Text[] heldStatusNext;

    private Text enchantStatusName;
    private Text enchantStatusCur;
    private Text enchantStatusNext;

    #region 버튼
    private readonly string[] equipText =
        new string[4] { "장착 하기", "장착 중", "Mount", "Mounting" };
    private Text equipButtonText;

    private GameObject equipButton;
    private GameObject enchantButton;
    private GameObject advenceButton;
    private GameObject reinforceButton;
    #endregion

    public void UpdateDetailInfo(ref Weapon weaponOrigin)
    {
        targetEquipment = weaponOrigin;
        equipmentTitleName.text = weaponOrigin.equipmentName.text;
        weaponClone.DeepCopy(ref targetEquipment);

        ChangeMountText();
        UpdateNewWeaponInfo();
    }

    public void ChangeMountText()
    {
        equipButtonText.text = targetEquipment.CheckMounted() ? equipText[1] : equipText[0];
    }

    public void OnFuction(FunctionIndex fuctionIndex)
    {
        switch(fuctionIndex)
        {
            case FunctionIndex.Equip:
                {
                    OnEquip();
                    break;
                }
            case FunctionIndex.Reinforce:
                {
                    OnReinforce();
                    break;
                }
            case FunctionIndex.Promote:
                {
                    OnPromote();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void OnEquip()
    {
        if(targetEquipment.CheckMounted())
        {
            Debug.Log("이미 장착된 장비입니다.");
        }
        else
        {
            weaponManager.ChangeMountedEquipment(targetEquipment.equipIndex);
            ChangeMountText();
        }
    }

    public void OnReinforce()
    {
        if(targetEquipment.OnReinforce())
        {
            targetEquipment.DeepCopy(ref targetEquipment);
        }

        else
        {
            Debug.Log("돈이 부족해요, 강화실패");
        }
    }

    public void OnPromote()
    {
        int addEquipmentCount = targetEquipment.OnPromote();

        if (addEquipmentCount > 0)
        {
            weaponManager.UpdateCountwithIndex(targetEquipment.equipIndex + 1, addEquipmentCount);
            weaponClone.DeepCopy(ref targetEquipment);
        }

        else
        {
            Debug.Log("장비 개수가 부족해여");
        }
    }

    public void OnEnchant()
    {

    }

    private void UpdateNewWeaponInfo()
    {
        if(targetEquipment.statForLocal.damagePercent > 0)
        {
            equipStatusCur[0].gameObject.SetActive(true);
            equipStatusNext[0].gameObject.SetActive(true);
            equipStatusName[0].gameObject.SetActive(true);
            int itemCode = targetEquipment.statForSave.itemCode;
            float finalValue = DataManger.instance.weaponStatForDataDictionary[itemCode].damagePercentOrigin + targetEquipment.statForLocal.damagePercent;
            equipStatusNext[0].text = finalValue.ToString();
            equipStatusCur[0].text = targetEquipment.statForLocal.damagePercent.ToString();
        }
        else
        {
            equipStatusCur[0].gameObject.SetActive(false);
            equipStatusNext[0].gameObject.SetActive(false);
            equipStatusName[0].gameObject.SetActive(false);
        }

        if (targetEquipment.statForLocal.damageFixed > 0)
        {
            equipStatusCur[1].gameObject.SetActive(true);
            equipStatusNext[1].gameObject.SetActive(true);
            equipStatusName[1].gameObject.SetActive(true);
            int itemCode = targetEquipment.statForSave.itemCode;
            float finalValue = DataManger.instance.weaponStatForDataDictionary[itemCode].damageFixedOrigin + targetEquipment.statForLocal.damageFixed;
            equipStatusNext[1].text = finalValue.ToString();
            equipStatusCur[1].text = targetEquipment.statForLocal.damageFixed.ToString();
        }
        else
        {
            equipStatusCur[1].gameObject.SetActive(false);
            equipStatusNext[1].gameObject.SetActive(false);
            equipStatusName[1].gameObject.SetActive(false);
        }

        if (targetEquipment.statForLocal.criticalChance > 0)
        {
            equipStatusCur[2].gameObject.SetActive(true);
            equipStatusNext[2].gameObject.SetActive(true);
            equipStatusName[2].gameObject.SetActive(true);
            int itemCode = targetEquipment.statForSave.itemCode;
            float finalValue = DataManger.instance.weaponStatForDataDictionary[itemCode].criticalChanceOrigin + targetEquipment.statForLocal.criticalChance;
            equipStatusNext[2].text = finalValue.ToString();
            equipStatusCur[2].text = targetEquipment.statForLocal.criticalChance.ToString();
        }
        else
        {
            equipStatusCur[2].gameObject.SetActive(false);
            equipStatusNext[2].gameObject.SetActive(false);
            equipStatusName[2].gameObject.SetActive(false);
        }

        if (targetEquipment.statForLocal.criticalDamage > 0)
        {
            equipStatusCur[3].gameObject.SetActive(true);
            equipStatusNext[3].gameObject.SetActive(true);
            equipStatusName[3].gameObject.SetActive(true);
            int itemCode = targetEquipment.statForSave.itemCode;
            float finalValue = DataManger.instance.weaponStatForDataDictionary[itemCode].criticalDamageOrigin + targetEquipment.statForLocal.criticalDamage;
            equipStatusNext[3].text = finalValue.ToString();
            equipStatusCur[3].text = targetEquipment.statForLocal.criticalDamage.ToString();
        }
        else
        {
            equipStatusCur[3].gameObject.SetActive(false);
            equipStatusNext[3].gameObject.SetActive(false);
            equipStatusName[3].gameObject.SetActive(false);
        }
    }

    private void AwakeSetUp()
    {
        weaponManager = WeaponManager.instance;

        equipmentTitleName = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        weaponClone = transform.GetChild(2).GetComponent<Weapon>();

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

        equipButton = transform.GetChild(6).gameObject;
        enchantButton = transform.GetChild(7).gameObject;
        advenceButton = transform.GetChild(8).gameObject;
        reinforceButton = transform.GetChild(9).gameObject;

        equipButtonText = equipButton.transform.GetChild(0).GetComponent<Text>();
    }
    private void Awake()
    {
        AwakeSetUp();
    }
}
