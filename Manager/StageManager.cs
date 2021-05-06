using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StageIndex(int index);
public class StageManager : MonoBehaviour
{
    public StageIndex stageindex;
    public static StageManager instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<StageManager>();

            return m_instance;
        }
    }
    private static StageManager m_instance;

    private List<Monster> monList;
    private Dictionary<string, Monster> prefabDictionary;
    private Dictionary<string, Queue<Monster>> monsterDictionary;
    //private Dictionary<string, Queue<Monster>> bossDictionary;
    private int createCount;
    private int summonCount;

    public int stageIndex { get; set; }

    private Hero hero;

    public Monster GetMonster(string name)
    {
        Monster monster =
            monsterDictionary[name].Count > 0 ?
            monsterDictionary[name].Dequeue() : AddMonster(name);
        monster.gameObject.SetActive(true);
        monster.myName = name;

        if ((stageIndex) % 10 == 0 && stageIndex != 0)
        {
            monster.HpBar = UIManager.instance.bossHP;
            monster.HpBar.gameObject.SetActive(true);
        }

        else
        {
            monster.HpBar = UIManager.instance.GetHpBar("MonsterHP");
        }
        monster.HpBar.targetTransform = monster.transform;

        return monster;
    }

    public void BackMonster(string name, Monster monster)
    {
        if (null != monster)
        {
            monster.ResetStatus(this.transform);
            monster.gameObject.SetActive(false);
            monsterDictionary[name].Enqueue(monster);

            --summonCount;
            if (summonCount == 0)
            {
                monList.Clear();
                MoveToNext();
            }
        }
    }

    public void SummonMonster()
    {
        Monster mon = GetMonster("Slime");
        mon.transform.position = new Vector3(3.2f, 1.07f, 0.0f);
        float sqrtValue = (((stageIndex*0.1f) * (stageIndex*0.1f)) * 0.5f) + 1.0f; 
        mon.hitPoint *= sqrtValue;
        mon.HpBar.SetUpHealth(mon.hitPoint);
        monList.Add(mon);

        if(stageIndex > 0 && stageIndex % 10 == 0)
        {
            mon.transform.localScale = Vector3.one * 1.5f;
        }

        else
        {
            mon.transform.localScale = Vector3.one * 1.0f;
        }

        summonCount++;
        stageIndex++;
    }

    private Monster AddMonster(string name)
    {
        for (int i = 0; i < createCount; i++)
        {
            Monster proj = Instantiate<Monster>(prefabDictionary[name]);
            proj.transform.SetParent(this.transform);
            proj.gameObject.SetActive(false);
            monsterDictionary[name].Enqueue(proj);
        }

        return monsterDictionary[name].Dequeue();
    }

    private void CreateMonster(string name)
    {
        Queue<Monster> q = new Queue<Monster>();

        for (int i = 0; i < createCount; i++)
        {
            Monster mon = Instantiate<Monster>(prefabDictionary[name]);
            mon.transform.SetParent(this.transform);
            mon.gameObject.SetActive(false);
            q.Enqueue(mon);
        }

        monsterDictionary.Add(name, q);
    }

    private void LoadPrefab()
    {
        prefabDictionary.Add("Slime", Resources.Load<Monster>("Prefab/Slime"));
    }

    private void SetUp()
    {
        summonCount = 0;
        createCount = 13;

        stageIndex = 1;
        monList = new List<Monster>();
        prefabDictionary = new Dictionary<string, Monster>();
        monsterDictionary = new Dictionary<string, Queue<Monster>>();

        LoadPrefab();
        CreateMonster("Slime");

        hero = Instantiate(Resources.Load<Hero>("Prefab/Hero"));
        hero.transform.position = new Vector3(-1.64f, 1.07f, 0.0f);
    }

    private void MoveToNext()
    {
        stageindex(stageIndex);
        StartCoroutine(hero.BeginStage());
    }

    private void Start()
    {
        MoveToNext();
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);
        SetUp();
    }
}
