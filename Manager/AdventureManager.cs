using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureManager : MonoBehaviour
{
    public enum UnderInfo { HeroInfo, Status, Weapon, Arms, Skill, Drone, End}

    #region Private Fields
    private UnderInfo curInfo;
    private ContentsMenu[] contentsMenuChild;
    private ButtonUnderHud[] childButton;
    #endregion

    #region Public Fields
    public GameObject adventureCavnas;
    #endregion

    #region Instance
    static public AdventureManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<AdventureManager>();

            return instance;
        }
    }
    private static AdventureManager instance;
    #endregion

    public void ChangeCanvas(UnderInfo info)
    {
        if (curInfo == info) return;

        if(curInfo != UnderInfo.Status || curInfo != UnderInfo.HeroInfo)
        {
            PopUpManager.instance.ClearAllPopUp();
        }

        ActiveCanvas(false);
        curInfo = info;
        ActiveCanvas(true);
        contentsMenuChild[(int)curInfo].ResetOnEnable();
    }

    private void ActiveCanvas(bool value)
    {
        contentsMenuChild[(int)curInfo].gameObject.SetActive(value);
        childButton[(int)curInfo].ChangeActivate(value);
    }

    private void SetUp()
    {
        contentsMenuChild = new ContentsMenu[(int)UnderInfo.End];
        childButton = new ButtonUnderHud[(int)UnderInfo.End];
        for(int i = 0; i < (int)UnderInfo.End; i++)
        {
            contentsMenuChild[i] = adventureCavnas.transform.GetChild(i).GetComponent<ContentsMenu>();

            childButton[i] = adventureCavnas.transform.GetChild(6).
                GetChild(i).gameObject.GetComponent<ButtonUnderHud>();
        }

        curInfo = UnderInfo.HeroInfo;
    }

    private void Start()
    {
        contentsMenuChild[0].gameObject.SetActive(true);
        childButton[0].ChangeActivate(true);

        for(int i = 1; i < (int)UnderInfo.End; i++)
        {
            contentsMenuChild[i].gameObject.SetActive(false);
            childButton[i].ChangeActivate(false);
        }
    }

    private void Awake()
    {
        SetUp();

        for (int i = 0; i < (int)UnderInfo.End; i++)
        {
            contentsMenuChild[i].gameObject.SetActive(true);
        }
    }
}
