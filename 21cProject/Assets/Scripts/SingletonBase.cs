using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    private static T s_Instance;

    public static T Instance
    {
        get
        {
            if (s_Instance == null)
            {
                var go = GameObject.Find(typeof(T).ToString());
                if (go == null)
                {
                    go = new GameObject(typeof(T).ToString());
                }

                var instance = go.GetComponent<T>();

                if (instance == null)
                {
                    s_Instance = go.AddComponent<T>();
                }
                else
                {
                    s_Instance = instance;
                }

                s_Instance.InitManager();

                return s_Instance;
            }
            else
            {
                return s_Instance;
            }
        }
    }

    /// <summary>
    /// 매니저 초기화 함수.
    /// </summary>
    public virtual void InitManager()
    {
        //Debug.Log(typeof(T).ToString() + " 초기화 성공");
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
