using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpUI : MonoBehaviour
{
    public bool isOpenOldPopUp;

    public void AddPopUpUI(bool closeBefore = false)
    {
        PopUpManager.instance.AddPopUp(this, closeBefore);
    }

    public virtual void RemoveEvents()
    {
        gameObject.SetActive(false);
    }
}
