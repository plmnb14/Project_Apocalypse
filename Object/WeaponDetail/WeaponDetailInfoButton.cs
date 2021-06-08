using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponDetailInfoButton : MonoBehaviour, IPointerClickHandler
{
    public WeaponInfoUI.FunctionIndex fuctionIndex;
    private WeaponInfoUI weaponDetailInfo;

    public void OnPointerClick(PointerEventData eventData)
    {
        weaponDetailInfo.OnFuction(fuctionIndex);
    }

    private void Start()
    {
        //weaponDetailInfo = WeaponManager.instance.weapon;
    }
}
