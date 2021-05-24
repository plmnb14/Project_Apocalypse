using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FixedMenu { Adventure, Dungeon, Hide, Union, Store, Gacha, FixedMenu_end }

public class FixedMenuManager : MonoBehaviour
{
    #region 인스턴스
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

    public FixedMenu currentFixedMenu;
    public GameObject resourceCanvas;
    public PopUpUI gachaPopUpCanvas;
    public GachaResultPopUp gachaResultPopUp;
    public GameObject[] contentsCanvas;

    public void ChangeCurrentFixedMenu(FixedMenu fixedMenu)
    {
        if(currentFixedMenu == fixedMenu)
        {
            if (fixedMenu == FixedMenu.Adventure) return;

            fixedMenu = FixedMenu.Adventure;
        }

        contentsCanvas[(int)currentFixedMenu].SetActive(false);
        currentFixedMenu = fixedMenu;
        contentsCanvas[(int)currentFixedMenu].SetActive(true);
    }

    public void PopUpGachaResult()
    {
        PopUpManager.instance.AddPopUp(gachaPopUpCanvas);
    }

    public void GachaCount(int gachaCount)
    {
        gachaResultPopUp.gachaCount = gachaCount;
    }

    public void GachaEvent()
    {
        StartCoroutine(gachaResultPopUp.ShowGachaResult());
    }

    private void AwakeSetUp()
    {

    }

    private void Start()
    {
        resourceCanvas.SetActive(true);
        contentsCanvas[(int)currentFixedMenu].SetActive(true);
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        AwakeSetUp();
    }
}
