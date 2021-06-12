using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsContentsMenu : ContentsMenu
{
    #region Enum
    private enum ArmsMenu { Lobby, Management, End };
    #endregion

    #region Private Fields
    private ArmsLobby armsLobby;
    private ArmsManagement armsManagement;
    #endregion

    #region Reset Events
    public void ResetPopUpActive(bool isActive)
    {
        armsLobby.gameObject.SetActive(isActive);
        armsManagement.gameObject.SetActive(isActive);
    }

    public override void ResetOnEnable()
    {
        ResetPopUpActive(false);
        armsLobby.AddPopUpUI();
    }
    #endregion

    #region Events
    public void PopUpArmsManagement(ArmsType armsType, int slotIndex)
    {
        armsManagement.AddPopUpUI(true);
        armsManagement.ChangeArmsType(armsType);
        armsManagement.ChangeArmsMenu(ref armsLobby.armsSlots[slotIndex]);
    }

    public void PopUpArmsMount()
    {
        armsManagement.PopUpDismountPanel();
    }
    #endregion

    #region Awake Event
    private void LoadChilds()
    {
        armsLobby = transform.GetChild((int)ArmsMenu.Lobby).GetComponent<ArmsLobby>();
        armsManagement = transform.GetChild((int)ArmsMenu.Management).GetComponent<ArmsManagement>();
    }
    private void Awake()
    {
        LoadChilds();
        ResetPopUpActive(false);
    }
    #endregion
}
