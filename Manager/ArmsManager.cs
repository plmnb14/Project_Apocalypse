using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmsType { Head, Heart, Spine, Leg, Implant, BattleCore, End }

public class ArmsManager : Singleton<ArmsManager>
{
    #region Public Fields
    public ArmsContentsMenu armsContentsMenu;
    #endregion

    #region Property Fields
    public List<Arms>[] armsList { get; set; }
    #endregion

    #region Events
    public void PopUpArmsManagement(ArmsType armsType, int slotIndex)
    {
        armsContentsMenu.PopUpArmsManagement(armsType, slotIndex);
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

    private void CreateArms()
    {
        Arms arms = Instantiate(Resources.Load<Arms>("Prefab/Arms"));
        armsList[0].Add(arms);
    }

    private void Awake()
    {
        AwakeSetUp();
        CreateArms();
    }
    #endregion
}
