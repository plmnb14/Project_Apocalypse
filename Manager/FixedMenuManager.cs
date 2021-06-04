using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FixedMenu { Adventure, Dungeon, Hide, Union, Store, Gacha, End }

public class FixedMenuManager : MonoBehaviour
{
    #region Instance
    static public FixedMenuManager instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = FindObjectOfType<FixedMenuManager>();
            }

            return _instance;
        }
    }
    static private FixedMenuManager _instance;
    #endregion

    #region Public Fields
    public FixedMenu currentFixedMenu;
    public GameObject resourceCanvas;
    public GameObject contentsCanvas;
    #endregion

    #region Private Fields
    private GameObject[] contents;
    #endregion

    #region Awake Events
    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        AwakeSetUp();
    }

    private void AwakeSetUp()
    {
        contents = new GameObject[(int)FixedMenu.End];
        contents[(int)FixedMenu.Adventure] = contentsCanvas.transform.GetChild(0).gameObject;
        contents[(int)FixedMenu.Gacha] = contentsCanvas.transform.GetChild(1).gameObject;
    }
    #endregion

    public void ChangeCurrentFixedMenu(FixedMenu fixedMenu)
    {
        if(currentFixedMenu == fixedMenu)
        {
            if (fixedMenu == FixedMenu.Adventure) return;

            fixedMenu = FixedMenu.Adventure;
        }

        contents[(int)currentFixedMenu].SetActive(false);
        currentFixedMenu = fixedMenu;
        contents[(int)currentFixedMenu].SetActive(true);
    }

    private void Start()
    {
        resourceCanvas.SetActive(true);
        contents[(int)currentFixedMenu].SetActive(true);
    }
}
