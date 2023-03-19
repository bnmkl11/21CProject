using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : SingletonBase<SceneChanger>
{
    // �� Ÿ��.
    private kSCNENE_TYPE m_CurrentSceneType;
    public kSCNENE_TYPE CurrentSceneType { get; private set; } = kSCNENE_TYPE.None;

    // ���� �� ������ �Ǵ� ������Ʈ.
    private SceneMain m_CurrentSceneMain;
    public SceneMain CurrentSceneMain { get; set; }

    // �ε����� ���� �� �Ѿ ��Ÿ��.
    kSCNENE_TYPE m_NextSceneType;

    // �ε� �ڷ�ƾ.
    Coroutine m_LoadingCoroutine;

    public override void InitManager()
    {
        Debug.Log(gameObject.name + "Initialize Success!!");
        return;
    }

    /// <summary>
    /// �� �ҷ�����.
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
    /// �� �ε�.
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
    /// �� �θ��� �ڷ�ƾ.
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