using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenuUI : ContentsMenu
{
    #region Private Fields
    public WeaponInvenUI weaponInvenUI { get; set; }
    public WeaponInfoUI weaponInfoUI { get; set; }
    #endregion

    #region Events
    public void WeaponInfoPopUp(ref Weapon weapon)
    {
        weaponInfoUI.AddPopUpUI(true);
        weaponInfoUI.CopyWeaponInfo(ref weapon);
    }
    #endregion

    #region Reset Events
    public void ResetPopUpActive(bool isActive)
    {
        weaponInvenUI.gameObject.SetActive(isActive);
        weaponInfoUI.gameObject.SetActive(isActive);
    }

    public override void ResetOnEnable()
    {
        ResetPopUpActive(false);
        weaponInvenUI.AddPopUpUI();
    }
    #endregion

    #region Awake Event
    private void LoadChilds()
    {
        weaponInvenUI = transform.GetChild(0).GetComponent<WeaponInvenUI>();
        weaponInfoUI = transform.GetChild(1).GetComponent<WeaponInfoUI>();
    }
    private void Awake()
    {
        LoadChilds();
        //ResetPopUpActive(false);
    }
    #endregion
}
