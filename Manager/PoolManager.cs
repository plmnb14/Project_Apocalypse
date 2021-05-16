using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region 인스턴스
    static public PoolManager instance
    {
        get
        {
            if (null == m_instance) m_instance = FindObjectOfType<PoolManager>();
            return m_instance;
        }
    }

    static private PoolManager m_instance;
    #endregion

    #region public 변수
    public int createCount;
    #endregion

    #region Private 변수
    private Dictionary<string, WorldObject> _prefabDictionary;
    private Dictionary<string, Queue<WorldObject>> _poolingDictionary;
    #endregion

    public WorldObject GetObject(string objectName)
    {
        if (_poolingDictionary[objectName].Count <= 0)
        {
            Queue<WorldObject> queue = _poolingDictionary[objectName];
            AddOnQueue(name, ref queue);
        }

        WorldObject worldObject = _poolingDictionary[objectName].Dequeue();
        worldObject.myName = objectName;
        worldObject.gameObject.SetActive(true);

        return worldObject;
    }

    public void BackObject(string objectName, WorldObject worldObject)
    {
        worldObject.ResetStatus(this.transform);
        worldObject.gameObject.SetActive(false);
        _poolingDictionary[objectName].Enqueue(worldObject);
    }

    private void Create(string name)
    {
        Queue<WorldObject> queue = new Queue<WorldObject>();
        AddOnQueue(name, ref queue);
        _poolingDictionary.Add(name, queue);
    }

    private void AddOnQueue(string name, ref Queue<WorldObject> queue)
    {
        for (int i = 0; i < createCount; i++)
        {
            WorldObject pooledObject = Instantiate(_prefabDictionary[name]);
            pooledObject.transform.SetParent(this.transform);
            pooledObject.gameObject.SetActive(false);
            queue.Enqueue(pooledObject);
        }
    }

    private void LoadPrefab(string prefabName, string Path)
    {
        _prefabDictionary.Add(prefabName, Resources.Load<WorldObject>(Path));
        Create(prefabName);
    }

    private void AwakeSetUp()
    {
        _prefabDictionary = new Dictionary<string, WorldObject>();
        _poolingDictionary = new Dictionary<string, Queue<WorldObject>>();

        LoadPrefab("DamageFont", "Prefab/DamageFont");
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);
        AwakeSetUp();
    }
}
