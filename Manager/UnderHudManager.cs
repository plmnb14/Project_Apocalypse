using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderHudManager : MonoBehaviour
{
    public enum UnderInfo { HeroInfo, Status, Weapon, Arms, Drone, End}
    private UnderInfo curInfo;
    private ContentsMenu[] contentsMenuChild;
    ButtonUnderHud[] childButton;

    static public UnderHudManager instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<UnderHudManager>();

            return m_instance;
        }
    }
    private static UnderHudManager m_instance;

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
            contentsMenuChild[i] = transform.GetChild(i+1).GetComponent<ContentsMenu>();

            childButton[i] = transform.GetChild(0).
                transform.GetChild(i).gameObject.GetComponent<ButtonUnderHud>();
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
