using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 내 풀에 있는 오브젝트.
    private List<PoolingObjectBase> m_ListMyPoolingObject = new List<PoolingObjectBase>();

    // 활성화된 오브젝트.
    private Queue<PoolingObjectBase> m_QActiveObject = new Queue<PoolingObjectBase>();

    // 오리지날 프리팹.
    private GameObject m_OriginalPrefab;

    // 풀이 초기화가 되었는지.
    private bool m_isInitialize = false;

    public void InitializePool<T>(int count, GameObject prefab) where T : PoolingObjectBase
    {
        if (m_isInitialize == true)
        {
            return;
        }

        m_OriginalPrefab = prefab;
        for (int i = 0; i < count; i++)
        {
            CreateAndPushObject<T>(prefab);
        }

        m_isInitialize = true;
    }

    public GameObject GetOriginal()
    {
        return m_OriginalPrefab;
    }

    private T CreateAndPushObject<T>(GameObject prefab) where T : PoolingObjectBase
    {
        if (prefab == null)
            return null;

        GameObject newGameObject = Instantiate(prefab);

        if (newGameObject != null)
        {
            var newObject = newGameObject.GetComponent<T>();
            if (newObject == null)
            {
                return null;
            }

            newGameObject.name = prefab.name;

            newObject.Initialize();
            PushToPool(newObject);
            m_ListMyPoolingObject.Add(newObject);

            return newObject;
        }

        return null;
    }

    public T PopFromPool<T>() where T : PoolingObjectBase
    {
        if (m_QActiveObject.Count == 0)
        {
            CreateAndPushObject<T>(m_OriginalPrefab);
        }

        var newObject = m_QActiveObject.Dequeue();
        newObject.UpdateObject();
        newObject.IsInPool = false;

        return newObject as T;
    }

    public void PushToPool(PoolingObjectBase po)
    {
        if (po == null)
            return;

        // 이미 풀에 넣었는데 또 넣을려한다? 리턴.
        if (po.IsInPool)
            return;

        m_QActiveObject.Enqueue(po);
        po.IsInPool = true;
        
        po.transform.SetParent(transform);

        if (po.gameObject.activeSelf)
        {
            po.Hide();
        }

        po.DisposeObject();
    }

    /// <summary>
    /// 풀 내 내용물 전부 삭제.
    /// </summary>
    public void AllClearPool()
    {
        for (int i = 0; i < m_ListMyPoolingObject.Count; i++)
        {
            Destroy((m_ListMyPoolingObject[i]).gameObject);
        }

        m_QActiveObject.Clear();
        m_ListMyPoolingObject.Clear();
    }

    /// <summary>
    /// 활성화된 오브젝트를 전부 풀에 집어넣는다.
    /// </summary>
    public void AllPushToPool()
    {
        for (int i = 0; i < m_ListMyPoolingObject.Count; i++)
        {
            PushToPool(m_ListMyPoolingObject[i]);
        }
    }

}