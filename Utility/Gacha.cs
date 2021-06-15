using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gacha : MonoBehaviour, IPointerClickHandler
{
    public int gachaCount;
    private Queue<int> gachaItemCode;

    public void OnPointerClick(PointerEventData eventData)
    {
        for(int i = 0; i < gachaCount; i++)
        {
            ItemGacha();
        }

        GachaEvent();
    }

    private void GachaEvent()
    {
        AddGachaPrize();

        GachaManager gachaManager = GachaManager.instance;
        gachaManager.PopUpGachaResult();
        gachaManager.GachaCount(gachaCount);
        gachaManager.GachaResultQueue(ref gachaItemCode);
        gachaManager.GachaEvent();
    }

    private void AddGachaPrize()
    {
        for (int i = 0; i < gachaCount; i++)
        {
            int itemCode = gachaItemCode.Dequeue();
            WeaponManager.instance.UpdateCount(itemCode);
            gachaItemCode.Enqueue(itemCode);
        }
    }

    private void ItemGacha()
    {
        int randomNumber = Random.Range(0, 100000000);

        int count = DataManager.Instance.weaponGachaChanceList.Count;
        for(int i = 0; i < count; i++)
        {
            if(randomNumber <= DataManager.Instance.weaponGachaChanceList[i].accumulateChance)
            {
                gachaItemCode.Enqueue(DataManager.Instance.weaponGachaChanceList[i].itemCode);
                break;
            }
        }
    }

    private void Awake()
    {
        gachaItemCode = new Queue<int>();
    }
}
