using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : SingletonBase<SceneChanger>
{
    // 씬 타입.
    private kSCNENE_TYPE m_CurrentSceneType;
    public kSCNENE_TYPE CurrentSceneType { get; private set; } = kSCNENE_TYPE.None;

    // 현재 씬 메인이 되는 오브젝트.
    private SceneMain m_CurrentSceneMain;
    public SceneMain CurrentSceneMain { get; set; }

    // 로딩에서 다음 씬 넘어갈 씬타입.
    kSCNENE_TYPE m_NextSceneType;

    // 로딩 코루틴.
    Coroutine m_LoadingCoroutine;

    public override void InitManager()
    {
        Debug.Log(gameObject.name + "Initialize Success!!");
        return;
    }

    /// <summary>
    /// 씬 불러오기.
    /// </summary>
    public void LoadScene(kSCNENE_TYPE _sceneType)
    {
        if (m_CurrentSceneMain != null)
        {
            m_CurrentSceneMain.ExitSceneInit();
        }

        m_NextSceneType = _sceneType;
        SceneManager.LoadScene("Loading", LoadSceneMode.Single);
    }

    /// <summary>
    /// 씬 로딩.
    /// </summary>
    public void LoadReservedScene(System.Action<float> callback)
    {
        if (m_LoadingCoroutine != null)
        {
            StopCoroutine(m_LoadingCoroutine);
        }

        m_LoadingCoroutine = StartCoroutine(LoadScene_C(m_NextSceneType, callback));
    }

    /// <summary>
    /// 씬 부르는 코루틴.
    /// </summary>
    IEnumerator LoadScene_C(kSCNENE_TYPE sceneType, System.Action<float> callback)
    {
        yield return null;

        Debug.Log(sceneType.ToString() + " Try to loading Scene");

        string sceneName = sceneType.ToString();
        AsyncOperation Op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        Op.allowSceneActivation = false;

        while (!Op.isDone)
        {
            yield return null;

            Debug.Log(sceneName.ToString() + " Loading...");
            if (Op.progress >= 0.9f)
            {
                m_CurrentSceneType = sceneType;
                Op.allowSceneActivation = true;
            }

            callback?.Invoke(Op.progress);
        }
    }

}