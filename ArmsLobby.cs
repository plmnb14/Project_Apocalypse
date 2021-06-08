using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsLobby : PopUpUI
{
    #region Private Fields
    private GameObject playerAvatar;
    #endregion

    #region Events

    #endregion

    #region Awake Events
    private void LoadChilds()
    {
        playerAvatar = transform.GetChild(0).gameObject;
    }

    private void Awake()
    {
        LoadChilds();
    }
    #endregion
}
