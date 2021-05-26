using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDetailInfo : MonoBehaviour
{
    public enum FunctionIndex { Equip, Reinforce, Promote, Enchant, FunctionIndex_End }

    private WeaponManager weaponManager;

    private Text equipmentTitleName;
    private Weapon weaponClone;
    private Weapon targetEquipment;

    private Text[] equipStatusName;
    private Text[] equipStatusCur;
    private Text[] equipStatusNext;
    private GameObject[] equipArrow;

    private Text[] heldStatusName;
    private Text[] heldStatusCur;
    private Text[] heldStatusNext;
    private GameObject[] heldArrow;

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
        weaponClone.ActiveMountedFrame(weaponClone.CheckMounted());

        ChangeMountText();
        UpdateNewWeaponInfo();
    }

    public void ChangeMountText()
    {
        equipButtonText.text = targetEquipment.CheckMounted() ? equipText[1] : equipText[0];
    }

    public void UpdateWeaponEnchantInfo()
    {
        if(targetEquipment.statForSave.enchantType != EnchantStat.EnchantStat_End)
        {
            enchantStatusName.gameObject.SetActive(true);
            enchantStatusNext.gameObject.SetActive(true);

            int grade = targetEquipment.statForSave.enchantGrade;
            enchantStatusCur.text =
                grade == 5 ? "<color=red>" + grade + " 등급" + "</color>" :
                grade == 4 ? "<color=orange>" + grade + " 등급" + "</color>" :
                grade == 3 ? "<color=purple>" + grade + " 등급" + "</color>" :
                grade == 2 ? "<color=blue>" + grade + " 등급" + "</color>" : "<color=green>" + grade + " 등급" + "</color>";

            enchantStatusName.text = 
                targetEquipment.statForSave.enchantType == EnchantStat.DamageFixed ? "공격력(고정)" :
                targetEquipment.statForSave.enchantType == EnchantStat.DamagePercent ? "공격력(배수)" :
                targetEquipment.statForSave.enchantType == EnchantStat.CriticalChance ? "치명타 배율" :
                targetEquipment.statForSave.enchantType == EnchantStat.CriticalDamage ? "치명타 대미지" :
                targetEquipment.statForSave.enchantType == EnchantStat.HitPoint ? "체력" : "방어력";
            enchantStatusNext.text = targetEquipment.statForLocal.enchantValue.ToString();
        }
        else
        {
            enchantStatusName.gameObject.SetActive(false);
            enchantStatusNext.gameObject.SetActive(false);
        }
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
            case FunctionIndex.Enchant:
                {
                    OnEnchant();
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
        if (!targetEquipment.CheckUnlocked())
            return;

        if(targetEquipment.CheckMounted())
        {
            Debug.Log("이미 장착된 장비입니다.");
        }
        else
        {
            weaponManager.ChangeMountedEquipment(targetEquipment.equipIndex);
            ChangeMountText();
            weaponClone.DeepCopy(ref targetEquipment);
            weaponClone.ActiveMountedFrame(weaponClone.CheckMounted());
        }
    }

    public void OnReinforce()
    {
        if(targetEquipment.OnReinforce())
        {
            weaponClone.DeepCopy(ref targetEquipment);
            UpdateNewWeaponInfo();
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
        if (targetEquipment.statForSave.enchantType != EnchantStat.EnchantStat_End)
        {
            float enchantValueOld = targetEquipment.statForLocal.enchantValue;
            targetEquipment.AddEnchantStatOnWeaponManager(enchantValueOld, true);
        }

        WeaponEnchant.EnchantWeapon(ref targetEquipment);
        float enchantValue = targetEquipment.statForLocal.enchantValue;
        targetEquipment.AddEnchantStatOnWeaponManager(enchantValue, false);
        weaponClone.DeepCopy(ref targetEquipment);
        UpdateWeaponEnchantInfo();
    }

    private void UpdateEquipStatInfo()
    {
        if (targetEquipment.statForLocal.damagePercent > 0)
        {
            equipArrow[0].gameObject.SetActive(true);
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
            equipArrow[0].gameObject.SetActive(false);
            equipStatusCur[0].gameObject.SetActive(false);
            equipStatusNext[0].gameObject.SetActive(false);
            equipStatusName[0].gameObject.SetActive(false);
        }

        if (targetEquipment.statForLocal.damageFixed > 0)
        {
            equipArrow[1].gameObject.SetActive(true);
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
            equipArrow[1].gameObject.SetActive(false);
            equipStatusCur[1].gameObject.SetActive(false);
            equipStatusNext[1].gameObject.SetActive(false);
            equipStatusName[1].gameObject.SetActive(false);
        }

        if (targetEquipment.statForLocal.criticalChance > 0)
        {
            equipArrow[2].gameObject.SetActive(true);
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
            equipArrow[2].gameObject.SetActive(false);
            equipStatusCur[2].gameObject.SetActive(false);
            equipStatusNext[2].gameObject.SetActive(false);
            equipStatusName[2].gameObject.SetActive(false);
        }

        if (targetEquipment.statForLocal.criticalDamage > 0)
        {
            equipArrow[3].gameObject.SetActive(true);
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
            equipArrow[3].gameObject.SetActive(false);
            equipStatusCur[3].gameObject.SetActive(false);
            equipStatusNext[3].gameObject.SetActive(false);
            equipStatusName[3].gameObject.SetActive(false);
        }
    }

    private void UpdateHeldStatInfo()
    {
        if (targetEquipment.statForLocal.heldStatusValue_0 > 0)
        {
            heldStatusCur[0].gameObject.SetActive(true);
            heldStatusNext[0].gameObject.SetActive(true);
            heldStatusName[0].gameObject.SetActive(true);
            heldArrow[0].gameObject.SetActive(true);
            int itemCode = targetEquipment.statForSave.itemCode;
            float finalValue = DataManger.instance.weaponStatForDataDictionary[itemCode].heldStatusValueOrigin_0 + targetEquipment.statForLocal.heldStatusValue_0;
            heldStatusNext[0].text = finalValue.ToString();
            heldStatusCur[0].text = targetEquipment.statForLocal.heldStatusValue_0.ToString();
        }

        else
        {
            heldStatusCur[0].gameObject.SetActive(false);
            heldStatusNext[0].gameObject.SetActive(false);
            heldStatusName[0].gameObject.SetActive(false);
            heldArrow[0].gameObject.SetActive(false);
        }

        if (targetEquipment.statForLocal.heldStatusValue_1 > 0)
        {
            heldStatusCur[1].gameObject.SetActive(true);
            heldStatusNext[1].gameObject.SetActive(true);
            heldStatusName[1].gameObject.SetActive(true);
            heldArrow[1].gameObject.SetActive(true);
            int itemCode = targetEquipment.statForSave.itemCode;
            float finalValue = DataManger.instance.weaponStatForDataDictionary[itemCode].heldStatusValueOrigin_1 + targetEquipment.statForLocal.heldStatusValue_1;
            heldStatusNext[1].text = finalValue.ToString();
            heldStatusCur[1].text = targetEquipment.statForLocal.heldStatusValue_1.ToString();
        }

        else
        {
            heldArrow[1].gameObject.SetActive(false);
            heldStatusCur[1].gameObject.SetActive(false);
            heldStatusNext[1].gameObject.SetActive(false);
            heldStatusName[1].gameObject.SetActive(false);
        }

        if (targetEquipment.statForLocal.heldStatusValue_2 > 0)
        {
            heldArrow[2].gameObject.SetActive(true);
            heldStatusCur[2].gameObject.SetActive(true);
            heldStatusNext[2].gameObject.SetActive(true);
            heldStatusName[2].gameObject.SetActive(true);
            int itemCode = targetEquipment.statForSave.itemCode;
            float finalValue = DataManger.instance.weaponStatForDataDictionary[itemCode].heldStatusValueOrigin_2 + targetEquipment.statForLocal.heldStatusValue_2;
            heldStatusNext[2].text = finalValue.ToString();
            heldStatusCur[2].text = targetEquipment.statForLocal.heldStatusValue_2.ToString();
        }

        else
        {
            heldArrow[2].gameObject.SetActive(false);
            heldStatusCur[2].gameObject.SetActive(false);
            heldStatusNext[2].gameObject.SetActive(false);
            heldStatusName[2].gameObject.SetActive(false);
        }
    }

    private void UpdateNewWeaponInfo()
    {
        UpdateEquipStatInfo();
        UpdateHeldStatInfo();
        UpdateWeaponEnchantInfo();
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
        equipArrow = new GameObject[4];
        for (int i = 0; i < 4; i++) equipArrow[i] = transform.GetChild(3).GetChild(i + 13).gameObject;

        heldStatusName = new Text[3];
        for (int i = 0; i < 3; i++) heldStatusName[i] = transform.GetChild(4).GetChild(i + 1).GetComponent<Text>();
        heldStatusCur = new Text[3];
        for (int i = 0; i < 3; i++) heldStatusCur[i] = transform.GetChild(4).GetChild(i + 4).GetComponent<Text>();
        heldStatusNext = new Text[3];
        for (int i = 0; i < 3; i++) heldStatusNext[i] = transform.GetChild(4).GetChild(i + 7).GetComponent<Text>();
        heldArrow = new GameObject[3];
        for (int i = 0; i < 3; i++) heldArrow[i] = transform.GetChild(4).GetChild(i + 10).gameObject;

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
