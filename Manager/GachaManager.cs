using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    #region Instance
    static public GachaManager instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = FindObjectOfType<GachaManager>();
            }

            return _instance;
        }
    }
    static private GachaManager _instance;
    #endregion

    #region Public Fields
    public GachaResultPopUp gachaResultPopUp;
    #endregion

    #region Awake Fields
    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
    }
    #endregion

    #region Events
    public void PopUpGachaResult()
    {
        PopUpManager.instance.AddPopUp(gachaResultPopUp);
    }

    public void GachaCount(int gachaCount)
    {
        gachaResultPopUp.gachaCount = gachaCount;
    }

    public void GachaResultQueue(ref Queue<int> resultQueue)
    {
        gachaResultPopUp.gachaItemCode = resultQueue;
    }

    public void GachaEvent()
    {
        StartCoroutine(gachaResultPopUp.ShowGachaResult());
    }
    #endregion
}
