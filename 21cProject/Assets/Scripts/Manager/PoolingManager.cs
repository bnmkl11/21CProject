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
    /// 풀 생성.
    /// </summary>
    public void CreatePool<T>(string path, int startCount = 1) where T : PoolingObjectBase
    {
        if (m_DicOfPool.ContainsKey(typeof(T).ToString()))
        {
            Debug.LogError(typeof(T).ToString() + " 풀 이미 생성됨.");
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

        Debug.Log(typeof(T).ToString() + "생성 완료.");
    }

    /// <summary>
    /// 오브젝트 푸시.
    /// </summary>
    public void Push(System.Type type, PoolingObjectBase go)
    {
        if (go == null)
            return;

        ObjectPool pool = null;
        if (m_DicOfPool.TryGetValue(type.ToString(), out pool) == false)
        {
            Debug.LogError(type.ToString() + " 풀 먼저 생성하세요.");
            return;
        }

        pool.PushToPool(go);
    }

    /// <summary>
    /// 오브젝트 푸시.
    /// </summary>
    public void Push<T>(T go) where T : PoolingObjectBase
    {
        if (go == null) 
            return;

        ObjectPool pool = null;
        if (m_DicOfPool.TryGetValue(typeof(T).ToString(), out pool) == false)
        {
            Debug.LogError(typeof(T).ToString() + " 풀 먼저 생성하세요.");
            return;
        }

        pool.PushToPool(go);
    }

    /// <summary>
    /// 오브젝트 풀기.
    /// </summary>
    public T Pop<T>(Transform parent = null, System.Action onDiposeCallback = null) where T : PoolingObjectBase
    {
        return Pop<T>(Vector3.zero, Quaternion.identity, parent, onDiposeCallback);
    }

    /// <summary>
    /// 오브젝트 풀기.
    /// </summary>
    public T Pop<T>(Vector3 pos, Quaternion quat, Transform parent = null, System.Action onDiposeCallback = null) where T : PoolingObjectBase
    {
        ObjectPool pool = null;
        if (m_DicOfPool.TryGetValue(typeof(T).ToString(), out pool) == false)
        {
            Debug.LogError(typeof(T).ToString() + " 풀 먼저 생성하세요.");
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
    /// 모든 오브젝트를 풀에 집어넣음.
    /// </summary>
    public void PushAllObjectToPool()
    {
        foreach (var p in m_DicOfPool)
        {
            p.Value.AllPushToPool();
        }
    }

    /// <summary>
    /// 모든 풀 삭제.
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
    /// 모든 오브젝트를 풀에 집어넣는다.
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
    /// 오리지날 프리팹 반환.
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
    /// 특정 풀 깨끗하게 클리어.
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