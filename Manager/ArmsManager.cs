using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmsType { Head, Heart, Spine, Leg, Implant, BattleCore, End }

public class ArmsManager : MonoBehaviour
{
    #region Instance
    static public ArmsManager Instance
    {
        get
        {
            if(null == instance)
            {
                instance = FindObjectOfType<ArmsManager>();
            }
            return instance;
        }
    }
    static private ArmsManager instance;
    #endregion

    #region Public Fields
    public ArmsContentsMenu armsContentsMenu;
    #endregion

    #region Property Fields
    public List<Arms>[] armsList { get; set; }
    #endregion

    #region Events
    public void PopUpArmsManagement(ArmsType armsType)
    {
        armsContentsMenu.PopUpArmsManagement(armsType);
    }

    public void PopUpArmsMount()
    {
        armsContentsMenu.PopUpArmsMount();
    }
    #endregion

    #region Awake Event
    private void AwakeSetUp()
    {
        armsList = new List<Arms>[(int)ArmsType.End];
        for(int i = 0; i < (int)ArmsType.End; i++)
        {
            armsList[i] = new List<Arms>();
        }
    }

    private void Awake()
    {
        AwakeSetUp();
    }
    #endregion
}
