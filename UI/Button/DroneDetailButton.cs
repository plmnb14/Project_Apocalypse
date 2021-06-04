using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroneDetailButton : MonoBehaviour, IPointerClickHandler
{
    public enum DroneDetailPopUpOption { Mount, Upgrade, SkillCore, End };
    public DroneDetailPopUpOption droneDetailPopUpOption;

    #region Click Events
    public void OnPointerClick(PointerEventData eventData)
    { 
        switch(droneDetailPopUpOption)
        {
            case DroneDetailPopUpOption.Mount:
                {
                    DroneManager.instance.MountDrone();
                    break;
                }
            case DroneDetailPopUpOption.Upgrade:
                {
                    PopUpManager.instance.ShowNotificationPopUp("N_1001");
                    break;
                }
            case DroneDetailPopUpOption.SkillCore:
                {
                    PopUpManager.instance.ShowNotificationPopUp("N_1002");
                    break;
                }
        }
    }
    #endregion
}
