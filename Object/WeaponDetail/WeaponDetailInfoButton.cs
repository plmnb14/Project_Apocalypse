using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponDetailInfoButton : MonoBehaviour, IPointerClickHandler
{
    public WeaponDetailInfo.FunctionIndex fuctionIndex;
    private WeaponDetailInfo weaponDetailInfo;

    public void OnPointerClick(PointerEventData eventData)
    {
        weaponDetailInfo.OnFuction(fuctionIndex);
    }

    private void Start()
    {
        weaponDetailInfo = WeaponManager.instance.weaponDetail;
    }
}
