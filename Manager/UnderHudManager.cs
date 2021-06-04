using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderHudManager : MonoBehaviour
{
    public enum UnderInfo { HEROINFO, STATUSUPGRADE, EQUIP, Drone, ARTIFECT, UnderInfo_end}
    private UnderInfo curInfo;
    GameObject[] childCanvas;
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

        if(curInfo == UnderInfo.EQUIP || curInfo == UnderInfo.Drone)
        {
            PopUpManager.instance.ClearAllPopUp();
        }

        ActiveCanvas(false);
        curInfo = info;
        ActiveCanvas(true);
    }

    private void ActiveCanvas(bool value)
    {
        childCanvas[(int)curInfo].SetActive(value);
        childButton[(int)curInfo].ChangeActivate(value);
    }

    private void SetUp()
    {
        childCanvas = new GameObject[(int)UnderInfo.UnderInfo_end];
        childButton = new ButtonUnderHud[(int)UnderInfo.UnderInfo_end];
        for(int i = 0; i < (int)UnderInfo.UnderInfo_end; i++)
        {
            childCanvas[i] = transform.GetChild(i+1).gameObject;

            childButton[i] = transform.GetChild(0).
                transform.GetChild(i).gameObject.GetComponent<ButtonUnderHud>();
        }

        curInfo = UnderInfo.HEROINFO;
    }

    private void Start()
    {
        childCanvas[0].SetActive(true);
        childButton[0].ChangeActivate(true);

        for(int i = 1; i < (int)UnderInfo.UnderInfo_end; i++)
        {
            childCanvas[i].SetActive(false);
            childButton[i].ChangeActivate(false);
        }

        childCanvas[2].SetActive(true);
    }

    private void Awake()
    {
        SetUp();

        for (int i = 0; i < (int)UnderInfo.UnderInfo_end; i++)
        {
            childCanvas[i].SetActive(true);
        }
    }
}
