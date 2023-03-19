using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingObjectBase : MonoBehaviour, IPoolingObject
{

    private System.Action m_OnDisposeCallback;

    public bool IsInPool { get; set; } = false;
    public int StartCount { get; set; } = 20;

    /// <summary>
    /// ù ������Ʈ ������ �ʱ�ȭ �Լ�.
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// Ǯ �ٽ� ������ �����ϴ� �ݹ� �Լ�.
    /// </summary>
    /// <param name="callback"></param>
    public void SetDisposeCallback(System.Action callback)
    {
        m_OnDisposeCallback = callback;
    }

    /// <summary>
    /// ������Ʈ Ǯ�� �ٽ� ������ ���� �Լ�.
    /// </summary>
    public virtual void DisposeObject()
    {
        m_OnDisposeCallback?.Invoke();
        m_OnDisposeCallback = null;
    }

    /// <summary>
    /// ������Ʈ Ǯ���� ���ö� ���� �Լ�.
    /// </summary>
    public abstract void UpdateObject();

    /// <summary>
    /// �����ֱ�.
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ���߱�.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}