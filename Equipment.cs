using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Equipment : MonoBehaviour, IPointerClickHandler
{
    public int level;
    public int heldCount;

    public int upgradeValueCurrent { get; set; }
    public bool unlockEquipment { get; set; }
    public int equipIndex { get; set; }
    private string suffix;
    private int UpgradeValueOrigin;
    private float upgradeMultiple;

    private StringBuilder countStringBuilder;
    private StringBuilder levelStringBuilder;
    private Slider equipmentSlider;
    private Text heldCountText;
    private Text levelText;
    private GameObject lockScreen;

    public Text equipmentName { get; set; }
    public Text tierText { get; set; }
    public Image backgroundImage { get; set; }
    public Image rarityIcon { get; set; }
    public Image equipmentIcon { get; set; }

    public void UpdateCopy(ref Equipment equipment)
    {
        level = equipment.level;
        heldCount = equipment.heldCount;

        unlockEquipment = equipment.unlockEquipment;
        upgradeValueCurrent = equipment.upgradeValueCurrent;
        equipIndex = equipment.equipIndex;

        UpdateHeldCount();
        UpdateSliderValue();
        UpdateLevel(true);
        UnLockUpdate(unlockEquipment);

        backgroundImage.sprite = equipment.backgroundImage.sprite;
        rarityIcon.sprite = equipment.rarityIcon.sprite;
        equipmentIcon.sprite = equipment.equipmentIcon.sprite;
        tierText.text = equipment.tierText.text;
        equipmentName.text = equipment.equipmentName.text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EquipmentManager.instance.DetailPopUp(equipIndex);
    }

    public void UnLockUpdate(bool value)
    {
        unlockEquipment = value;

        levelText.gameObject.SetActive(unlockEquipment);
        equipmentSlider.gameObject.SetActive(unlockEquipment);
        equipmentName.gameObject.SetActive(unlockEquipment);

        lockScreen.gameObject.SetActive(!unlockEquipment);
    }

    private void UpdateHeldCount()
    {
        countStringBuilder.Remove(0, countStringBuilder.Length);
        countStringBuilder.Append(heldCount);
        countStringBuilder.Append(suffix);
        heldCountText.text = countStringBuilder.ToString();
    }

    private void UpdateLevel(bool onlyUpdate = false, int value = 1)
    {
        if(!onlyUpdate)
        {
            level += value;
            UpdateDamage();
        }

        levelStringBuilder.Remove(0, levelStringBuilder.Length);
        levelStringBuilder.Append("+");
        levelStringBuilder.Append(level);
        levelText.text = levelStringBuilder.ToString();
    }

    private void UpdateDamage()
    {
        EquipmentManager.instance.damage -= upgradeValueCurrent;
        upgradeValueCurrent = level * UpgradeValueOrigin;
        EquipmentManager.instance.damage += upgradeValueCurrent;
        EquipmentManager.instance.UpdateDamage();
    }

    private void UpdateSliderValue()
    {
        equipmentSlider.value = heldCount;
    }

    private void GetChild()
    {
        equipmentSlider = transform.GetChild(2).GetComponent<Slider>();
        heldCountText = equipmentSlider.transform.GetChild(4).GetComponent<Text>();
        tierText = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        levelText = transform.GetChild(0).GetChild(2).GetComponent<Text>();
        equipmentName = transform.GetChild(3).GetComponent<Text>();
        lockScreen = transform.GetChild(4).gameObject;
    }

    private void Start()
    {
        //UpdateDamage();
    }

    private void InitializeMember()
    {
        heldCount = 0;
        level = 1;
        UpgradeValueOrigin = 5;
        upgradeValueCurrent = UpgradeValueOrigin * level;
        unlockEquipment = false;
        suffix = " / 4";

        countStringBuilder = new StringBuilder(10, 10);
        levelStringBuilder = new StringBuilder(10, 10);

        backgroundImage = transform.GetChild(0).GetComponent<Image>();
        rarityIcon = transform.GetChild(1).GetComponent<Image>();
        equipmentIcon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    private void AwakeSetUp()
    {
        InitializeMember();
        GetChild();
        UpdateHeldCount();
        UpdateSliderValue();
        UpdateLevel(true);
        UnLockUpdate(false);
    }

    private void Awake()
    {
        AwakeSetUp();
    }
}
