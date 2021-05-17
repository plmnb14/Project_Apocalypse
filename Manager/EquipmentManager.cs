using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region 인스턴스
    static public EquipmentManager instance
    {
        get
        {
            if(null == _instance)
            {
                _instance = FindObjectOfType<EquipmentManager>();
            }

            return _instance;
        }
    }
    static private EquipmentManager _instance;
    #endregion

    #region 장비
    private int equipmentChildCount;
    private Equipment[] arrayEquipment;
    #endregion

    private EquipmentDetail equipDetail;
    private PopUpUI equipDetailPopUp;

    public long damage { get; set; }

    public void UpdateDamage()
    {
        PlayerStatusManager.instance.UpdateValue(HeroStatusEnum.Damage, (double)damage);
    }

    public void DetailPopUp(int equipIndex)
    {
        PopUpManager.instance.AddPopUp(this.GetComponent<PopUpUI>());
        PopUpManager.instance.AddPopUp(equipDetailPopUp, true);
        equipDetail.UpdateDetailInfo(ref arrayEquipment[equipIndex]);
    }

    private void StartSetUp()
    {
        arrayEquipment[0].UnLockUpdate(true);

        for (int i = 0; i < equipmentChildCount; i++)
        {
            if(arrayEquipment[i].unlockEquipment)
            {
                damage += arrayEquipment[i].upgradeValueCurrent;
            }
        }

        
    }

    private void Start()
    {
        StartSetUp();
        UpdateDamage();

        gameObject.SetActive(false);
    }

    private void AwakeSetUp()
    {
        GameObject equip = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        equipmentChildCount = equip.transform.childCount;
        arrayEquipment = new Equipment[equipmentChildCount];
        for (int i = 0; i < equipmentChildCount; i++)
        {
            arrayEquipment[i] = equip.transform.GetChild(i).GetComponent<Equipment>();
            arrayEquipment[i].equipIndex = i;
        }

        equipDetail = transform.parent.GetChild(6).GetComponent<EquipmentDetail>();
        equipDetailPopUp = transform.parent.GetChild(6).GetComponent<PopUpUI>();
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        AwakeSetUp();
    }
}
