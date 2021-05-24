using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    public GameObject gachaGrid;

    public int gachaCount { get; set; }

    #region 인스턴스
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

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
    }
}
