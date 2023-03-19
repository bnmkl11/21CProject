using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneMain : MonoBehaviour
{
    [SerializeField]
    protected kSCNENE_TYPE m_SceneType;

    // 씬이 초기화 되었는가.
    protected bool m_IsInit = false;

    virtual protected void Awake()
    {
        OnInitializeScene();
        SceneChanger.Instance.CurrentSceneMain = this;
    }

    virtual public void ExitSceneInit()
    {
        UIManager.Instance.AllPopUI();
    }

    public abstract void OnInitializeScene();

}