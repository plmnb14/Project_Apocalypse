using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    static public VFXManager instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<VFXManager>();

            return m_instance;
        }
    }
    private static VFXManager m_instance;

    Dictionary<string, Queue<VFX>> vfxDictionary;
    Dictionary<string, VFX> prefabDictionary;
    private int createCount;

    public VFX GetVFX(string name)
    {
        VFX vfx =
            vfxDictionary[name].Count > 0 ?
            vfxDictionary[name].Dequeue() : AddVFX(name);
        vfx.gameObject.SetActive(true);
        vfx.myName = name;

        return vfx;
    }

    public void BackVFX(string name, VFX vfx)
    {
        if (null != vfx)
        {
            vfx.ResetStatus(this.transform);
            vfx.gameObject.SetActive(false);
            vfxDictionary[name].Enqueue(vfx);
        }
    }

    private VFX AddVFX(string name)
    {
        for (int i = 0; i < createCount; i++)
        {
            VFX vfx = Instantiate<VFX>(prefabDictionary[name]);
            vfx.transform.SetParent(this.transform);
            vfx.gameObject.SetActive(false);
            vfxDictionary[name].Enqueue(vfx);
        }

        return vfxDictionary[name].Dequeue();
    }

    private void CreateProjectile(string name)
    {
        Queue<VFX> q = new Queue<VFX>();

        for (int i = 0; i < createCount; i++)
        {
            VFX vfx = Instantiate<VFX>(prefabDictionary[name]);
            vfx.transform.SetParent(this.transform);
            vfx.gameObject.SetActive(false);
            q.Enqueue(vfx);
        }

        vfxDictionary.Add(name, q);
    }

    private void LoadPrefab()
    {
        prefabDictionary.Add("VFX_Hit_01", Resources.Load<VFX> ("Prefab/VFX_Hit_01"));
        prefabDictionary.Add("VFX_Shot_00", Resources.Load<VFX>("Prefab/VFX_Shot_00"));
    }

    private void SetUp()
    {
        createCount = 100;

        vfxDictionary = new Dictionary<string, Queue<VFX>>();
        prefabDictionary = new Dictionary<string, VFX>();

        LoadPrefab();
        CreateProjectile("VFX_Hit_01");
        CreateProjectile("VFX_Shot_00");
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);
        SetUp();
    }
}
