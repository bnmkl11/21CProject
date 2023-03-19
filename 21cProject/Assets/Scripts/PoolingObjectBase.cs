using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingObjectBase : MonoBehaviour, IPoolingObject
{

    private System.Action m_OnDisposeCallback;

    public bool IsInPool { get; set; } = false;
    public int StartCount { get; set; } = 20;

    /// <summary>
    /// 첫 오브젝트 생성시 초기화 함수.
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// 풀 다시 넣을때 실행하는 콜백 함수.
    /// </summary>
    /// <param name="callback"></param>
    public void SetDisposeCallback(System.Action callback)
    {
        m_OnDisposeCallback = callback;
    }

    /// <summary>
    /// 오브젝트 풀에 다시 넣을때 실행 함수.
    /// </summary>
    public virtual void DisposeObject()
    {
        m_OnDisposeCallback?.Invoke();
        m_OnDisposeCallback = null;
    }

    /// <summary>
    /// 오브젝트 풀에서 나올때 실행 함수.
    /// </summary>
    public abstract void UpdateObject();

    /// <summary>
    /// 보여주기.
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 감추기.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}