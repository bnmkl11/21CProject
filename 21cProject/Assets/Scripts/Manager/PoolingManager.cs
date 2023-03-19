using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : SingletonBase<PoolingManager> 
{
    Dictionary<string, ObjectPool> m_DicOfPool = new Dictionary<string, ObjectPool>();

    public override void InitManager()
    {
        
    }

    /// <summary>
    /// Ǯ ����.
    /// </summary>
    public void CreatePool<T>(string path, int startCount = 1) where T : PoolingObjectBase
    {
        if (m_DicOfPool.ContainsKey(typeof(T).ToString()))
        {
            Debug.LogError(typeof(T).ToString() + " Ǯ �̹� ������.");
            return;
        }

        GameObject newPoolObject = new GameObject(typeof(T).ToString());
        newPoolObject.transform.SetParent(transform);
        ObjectPool newPool = newPoolObject.AddComponent<ObjectPool>();

        GameObject tobj = Resources.Load<GameObject>(path);
        if (tobj == null)
        {
            Debug.LogError("Fail Load Resource " + path);
        }

        newPool.InitializePool<T>(startCount, tobj);
        m_DicOfPool.Add(typeof(T).ToString(), newPool);

        Debug.Log(typeof(T).ToString() + "���� �Ϸ�.");
    }

    /// <summary>
    /// ������Ʈ Ǫ��.
    /// </summary>
    public void Push(System.Type type, PoolingObjectBase go)
    {
        if (go == null)
            return;

        ObjectPool pool = null;
        if (m_DicOfPool.TryGetValue(type.ToString(), out pool) == false)
        {
            Debug.LogError(type.ToString() + " Ǯ ���� �����ϼ���.");
            return;
        }

        pool.PushToPool(go);
    }

    /// <summary>
    /// ������Ʈ Ǫ��.
    /// </summary>
    public void Push<T>(T go) where T : PoolingObjectBase
    {
        if (go == null) 
            return;

        ObjectPool pool = null;
        if (m_DicOfPool.TryGetValue(typeof(T).ToString(), out pool) == false)
        {
            Debug.LogError(typeof(T).ToString() + " Ǯ ���� �����ϼ���.");
            return;
        }

        pool.PushToPool(go);
    }

    /// <summary>
    /// ������Ʈ Ǯ��.
    /// </summary>
    public T Pop<T>(Transform parent = null, System.Action onDiposeCallback = null) where T : PoolingObjectBase
    {
        return Pop<T>(Vector3.zero, Quaternion.identity, parent, onDiposeCallback);
    }

    /// <summary>
    /// ������Ʈ Ǯ��.
    /// </summary>
    public T Pop<T>(Vector3 pos, Quaternion quat, Transform parent = null, System.Action onDiposeCallback = null) where T : PoolingObjectBase
    {
        ObjectPool pool = null;
        if (m_DicOfPool.TryGetValue(typeof(T).ToString(), out pool) == false)
        {
            Debug.LogError(typeof(T).ToString() + " Ǯ ���� �����ϼ���.");
            return null;
        }

        var obj = pool.PopFromPool<T>();
        if (obj == null)
        {
            return null;
        }
        else
        {
            obj.transform.SetParent(parent);
            obj.transform.localPosition = pos;
            obj.transform.localRotation = quat;
            obj.SetDisposeCallback(onDiposeCallback);

            return obj;
        }
    }

    /// <summary>
    /// ��� ������Ʈ�� Ǯ�� �������.
    /// </summary>
    public void PushAllObjectToPool()
    {
        foreach (var p in m_DicOfPool)
        {
            p.Value.AllPushToPool();
        }
    }

    /// <summary>
    /// ��� Ǯ ����.
    /// </summary>
    public void AllClearPool()
    {
        foreach (var p in m_DicOfPool)
        {
            p.Value.AllClearPool();
            Destroy(p.Value.gameObject);
        }

        m_DicOfPool.Clear();
    }

    /// <summary>
    /// ��� ������Ʈ�� Ǯ�� ����ִ´�.
    /// </summary>
    public void PushAllObjectToPool<T>()
    {
        ObjectPool pool = null;

        if (m_DicOfPool.TryGetValue(typeof(T).ToString(), out pool))
        {
            pool.AllPushToPool();
        }
    }

    /// <summary>
    /// �������� ������ ��ȯ.
    /// </summary>
    public GameObject GetOriginalFromPool<T>()
    {
        ObjectPool pool = null;
        if (m_DicOfPool.TryGetValue(typeof(T).ToString(), out pool))
        {
            return pool.GetOriginal();
        }

        return null;
    }

    /// <summary>
    /// Ư�� Ǯ �����ϰ� Ŭ����.
    /// </summary>
    public void ClearPool<T>()
    {
        
        ObjectPool pool;

        if (m_DicOfPool.TryGetValue(typeof(T).ToString(), out pool))
        {
            pool.AllClearPool();
        }
    }
}