using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Dictionary<string, Queue<UI_HPBar>> UIDictionary;
    Dictionary<string, UI_HPBar> prefabDictionary;
    static public UIManager instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<UIManager>();

            return m_instance;
        }
    }
    private static UIManager m_instance;
    private GameObject hpCanvas;
    public UI_HPBar bossHP { get; set; }
    private int createCount;

    public UI_HPBar GetHpBar(string name)
    {
        UI_HPBar ui =
            UIDictionary[name].Count > 0 ?
            UIDictionary[name].Dequeue() : AddHpBar(name);
        ui.gameObject.SetActive(true);
        ui.myName = name;

        return ui;
    }

    public void BackHpBar(string name, UI_HPBar ui)
    {
        if (null != ui)
        {
            ui.ResetStatus(this.transform);
            ui.gameObject.SetActive(false);
            UIDictionary[name].Enqueue(ui);
        }
    }

    private UI_HPBar AddHpBar(string name)
    {
        for (int i = 0; i < createCount; i++)
        {
            UI_HPBar ui = Instantiate(prefabDictionary[name]);
            ui.transform.SetParent(hpCanvas.transform);
            ui.gameObject.SetActive(false);
            UIDictionary[name].Enqueue(ui);
        }

        return UIDictionary[name].Dequeue();
    }

    private void CreateProjectile(string name)
    {
        Queue<UI_HPBar> q = new Queue<UI_HPBar>();

        for (int i = 0; i < createCount; i++)
        {
            UI_HPBar ui = Instantiate(prefabDictionary[name]);
            ui.transform.SetParent(hpCanvas.transform);
            ui.gameObject.SetActive(false);
            q.Enqueue(ui);
        }

        UIDictionary.Add(name, q);
    }

    private void LoadPrefab()
    {
        prefabDictionary.Add("MonsterHP", Resources.Load<UI_MonsterHP>("Prefab/MonsterHP"));
    }

    private void SetUp()
    {
        createCount = 30;
        hpCanvas = GameObject.Find("HP Canvas");
        bossHP = GameObject.Find("BossHPBar").gameObject.GetComponent<UI_BossHPBar>();
        UIDictionary = new Dictionary<string, Queue<UI_HPBar>>();
        prefabDictionary = new Dictionary<string, UI_HPBar>();

        LoadPrefab();
        CreateProjectile("MonsterHP");
    }

    private void Start()
    {
        bossHP.gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);
        SetUp();
    }
}
