using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UpdateCoin(long value);
public delegate void UpdateElement(long value);
public delegate void UpdateCrystal(long value);
public class ResourceManager : MonoBehaviour
{
    public UpdateCoin updateCoin;
    public UpdateElement updateElement;
    public UpdateCrystal updateCrystal;

    private enum resourceIndex { coin, heart, crysytal, element };
    const int maxCount = 4;
    const int createCount = 1000;

    static public ResourceManager instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<ResourceManager>();

            return m_instance;
        }
    }
    private static ResourceManager m_instance;

    private Dictionary<string, Queue<DropResources>> resourceDictionary;
    private DropResources resourcePrefab;
    private Sprite[] resourceSprite = new Sprite[maxCount];
    private Vector3[] UIPosition;

    public long Coin 
    { 
        get { return coin; }
        set { coin = value; if (null != updateCoin) updateCoin(coin); }
    }
    private long coin;
    public long Element
    {
        get { return element; }
        set { element = value; if (null != updateElement) updateElement(element); }
    }
    private long element;
    public long CrystalFree
    {
        get { return crystalFree; }
        set { crystalFree = value; if (null != updateCrystal) updateCrystal(crystalFree + crystalCharged); }
    }
    private long crystalFree;
    public long CrystalCharged
    {
        get { return crystalCharged; }
        set { crystalCharged = value; if (null != updateCrystal) updateCrystal(crystalFree + crystalCharged); }
    }
    private long crystalCharged;

    public DropResources GetResource(string name)
    {
        int index =
            name == "UI_DropCoin" ? 0 :
            name == "UI_DropHeart" ? 1 :
            name == "UI_DropCrystal" ? 2 : 3;

        DropResources dResource =
            resourceDictionary[name].Count > 0 ?
            resourceDictionary[name].Dequeue() : AddResource(name);
        dResource.gameObject.SetActive(true);
        dResource.myName = name;
        dResource.targetVector = UIPosition[index];

        return dResource;
    }

    public void BackResource(string name, DropResources drop)
    {
        if (null != drop)
        {
            drop.ResetStatus(this.transform);
            drop.gameObject.SetActive(false);
            resourceDictionary[name].Enqueue(drop);
        }
    }

    private DropResources AddResource(string name)
    {
        int index =
            name == "UI_DropCoin" ? 0 :
            name == "UI_DropHeart" ? 1 :
            name == "UI_DropCrystal" ? 2 : 3;

        for (int i = 0; i < createCount; i++)
        {
            DropResources dResource = Instantiate(resourcePrefab);
            dResource.transform.SetParent(this.transform);
            dResource.targetMask = LayerMask.NameToLayer(name);
            dResource.GetComponent<SpriteRenderer>().sprite = resourceSprite[index];
            dResource.gameObject.SetActive(false);
            resourceDictionary[name].Enqueue(dResource);
        }

        return resourceDictionary[name].Dequeue();
    }

    private void CreateResource(string name)
    {
        int index =
            name == "UI_DropCoin" ? 0 :
            name == "UI_DropHeart" ? 1 :
            name == "UI_DropCrystal" ? 2 : 3;

        Queue<DropResources> q = new Queue<DropResources>();

        for (int i = 0; i < createCount; i++)
        {
            DropResources dResource = Instantiate(resourcePrefab);
            dResource.transform.SetParent(this.transform);
            dResource.targetMask = LayerMask.NameToLayer(name);
            dResource.GetComponent<SpriteRenderer>().sprite = resourceSprite[index];
            dResource.gameObject.SetActive(false);
            q.Enqueue(dResource);
        }

        resourceDictionary.Add(name, q);
    }

    private void LoadPrefab()
    {
        resourceSprite = Resources.LoadAll<Sprite>("Sprite/UI/Item");
        resourcePrefab = Resources.Load<DropResources>("Prefab/UI_DropResource");
    }

    private void SetUp()
    {
        coin = 9999999999;
        element = 0;
        crystalFree = 0;
        crystalCharged = 0;

        resourceDictionary = new Dictionary<string, Queue<DropResources>>();

        LoadPrefab();
        CreateResource("UI_DropCoin");
        CreateResource("UI_DropElement");
        CreateResource("UI_DropCrystal");
    }

    private void Start()
    {
        updateCrystal(crystalFree + crystalCharged);
        updateCoin(coin);
        updateElement(element);

        Camera tmpCam = Camera.main;

        RectTransform rect = GameObject.Find("Coin_Icon").transform.GetComponent<RectTransform>();
        UIPosition[0] = rect.position;

        rect = GameObject.Find("Crystal_Icon").transform.GetComponent<RectTransform>();
        UIPosition[2] = tmpCam.WorldToViewportPoint(rect.position);

        rect = GameObject.Find("Element_Icon").transform.GetComponent<RectTransform>();
        UIPosition[3] = tmpCam.WorldToViewportPoint(rect.position);
    }

    private void Awake()
    {
        UIPosition = new Vector3[maxCount];
        SetUp();
    }
}
