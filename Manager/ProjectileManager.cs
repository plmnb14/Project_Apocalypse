using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance
    {            
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<ProjectileManager>();

            return m_instance;
        }
    }
    private static ProjectileManager m_instance;

    Dictionary<string, Queue<Projectile>> projDictionary;
    Dictionary<string, Projectile> prefabDictionary;
    private int createCount;

    public Projectile GetProjectile(string name)
    {
        Projectile proj = 
            projDictionary[name].Count > 0 ? 
            projDictionary[name].Dequeue() : AddProjectile(name);
        proj.gameObject.SetActive(true);
        proj.myName = name;

        return proj;
    }

    public void BackProjtile(string name, Projectile proj)
    {
        if (null != proj)
        {
            proj.ResetStatus(this.transform);
            proj.gameObject.SetActive(false);
            projDictionary[name].Enqueue(proj);
        }
    }

    private Projectile AddProjectile(string name)
    {
        for(int i = 0; i < createCount; i++)
        {
            Projectile proj = Instantiate<Projectile>(prefabDictionary[name]);
            proj.transform.SetParent(this.transform);
            proj.gameObject.SetActive(false);
            projDictionary[name].Enqueue(proj);
        }

        return projDictionary[name].Dequeue();
    }

    private void CreateProjectile(string name)
    {
        Queue<Projectile> q = new Queue<Projectile>();

        for (int i = 0; i < createCount; i++)
        {
            Projectile proj = Instantiate<Projectile>(prefabDictionary[name]);
            proj.transform.SetParent(this.transform);
            proj.gameObject.SetActive(false);
            q.Enqueue(proj);
        }

        projDictionary.Add(name, q);
    }

    private void LoadPrefab()
    {
        prefabDictionary.Add("Bullet", Resources.Load<Projectile>("Prefab/VFX_Projectile"));
    }

    private void SetUp()
    {
        createCount = 10;

        projDictionary = new Dictionary<string, Queue<Projectile>>();
        prefabDictionary = new Dictionary<string, Projectile>();

        LoadPrefab();
        CreateProjectile("Bullet");
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(this);
        SetUp();
    }
}
