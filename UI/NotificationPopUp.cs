using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPopUp : PopUpUI
{
    #region Private Field
    private Text textScripts;
    #endregion

    #region Events
    public void SetScripts(string scriptsCode)
    {
        textScripts.text = DataManager.Instance.scriptsDictionary[scriptsCode];
    }
    #endregion

    #region Awake Event
    private void Awake()
    {
        textScripts = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        gameObject.SetActive(false);
    }
    #endregion
}
