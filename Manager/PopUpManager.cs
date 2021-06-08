using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    #region 보관 자료구조
    private Stack<PopUpUI> popUpCur;
    private Stack<PopUpUI> popUpOld;
    #endregion

    #region 인스턴스
    static public PopUpManager instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<PopUpManager>();

            return m_instance;
        }
    }
    private static PopUpManager m_instance;
    #endregion

    #region Public Field
    public NotificationPopUp notificationCanvas;
    #endregion

    public void ShowNotificationPopUp(string scriptsCode)
    {
        notificationCanvas.SetScripts(scriptsCode);
        AddPopUp(notificationCanvas);
    }

    public void RemovePopUp()
    {
        bool isOpenOldPopUp = popUpCur.Peek().isOpenOldPopUp;
        popUpCur.Pop().gameObject.SetActive(false);

        if (0 != popUpOld.Count && isOpenOldPopUp)
        {
            popUpCur.Push(popUpOld.Pop());
            popUpCur.Peek().gameObject.SetActive(true);
        }
    }

    public void ClearAllPopUp()
    {
        while(popUpCur.Count > 0)
        {
            popUpCur.Pop().gameObject.SetActive(false);
        }

        while (popUpOld.Count > 0)
        {
            popUpOld.Pop().gameObject.SetActive(false);
        }
    }

    public void AddPopUp(PopUpUI popUpUI, bool closeBefore = false)
    {
        popUpUI.gameObject.SetActive(true);

        if(closeBefore && popUpCur.Count != 0)
        {
            popUpCur.Peek().gameObject.SetActive(false);
            popUpOld.Push(popUpCur.Pop());
        }

        popUpCur.Push(popUpUI);
    }

    private void AwakeSetUp()
    {
        popUpCur = new Stack<PopUpUI>();
        popUpOld = new Stack<PopUpUI>();

        notificationCanvas.gameObject.SetActive(true);
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);
        AwakeSetUp();
    }
}
