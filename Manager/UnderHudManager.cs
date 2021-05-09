using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderHudManager : MonoBehaviour
{
    public enum UnderInfo { HEROINFO, STATUSUPGRADE, PET, EQUIP, ARTIFECT }
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
        childCanvas = new GameObject[5];
        childCanvas[0] = transform.GetChild(1).gameObject;
        childCanvas[1] = transform.GetChild(2).gameObject;

        childButton = new ButtonUnderHud[5];
        for(int i = 0; i < 2; i++)
        {
            childButton[i] = transform.GetChild(0).
                transform.GetChild(i).gameObject.GetComponent<ButtonUnderHud>();
        }

        curInfo = UnderInfo.HEROINFO;
    }

    private void Start()
    {
        childCanvas[0].SetActive(true);
        childButton[0].ChangeActivate(true);
    }

    private void Awake()
    {
        SetUp();
    }
}
