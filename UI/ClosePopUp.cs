using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePopUp : MonoBehaviour
{
    public void ClosePopUpUI()
    {
        PopUpManager.instance.RemovePopUp();
    }
}
