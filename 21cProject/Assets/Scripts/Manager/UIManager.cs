using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIManager : SingletonBase<UIManager>
{
    // ���� ���� ��.
    private UIBase m_CurrentSceneUI;

    // ���̴� UI.
    private Dictionary<System.Type, UIBase> m_DicOfUI = new Dictionary<System.Type, UIBase>();

    private Canvas          m_CurrentSceneUICanvas;
    private RectTransform   m_CurrentRectTr;

    private GameObject      m_HUDTextPrefab;
    private GameObject      m_TooltipBoxPrefab;
    private GameObject      m_HealthPrefab;
    private GameObject      m_BaseSlotPrefab;

    /// <summary>
    /// ĵ���� ��ȯ.
    /// </summary>
    public Canvas CurrentUICanvas
    {
        get
        {
            if (m_CurrentSceneUI == null)
                return null;

            if (m_CurrentSceneUICanvas == null)
            {
                m_CurrentSceneUICanvas = m_CurrentSceneUI.GetComponent<Canvas>();
                return m_CurrentSceneUICanvas;
            }
            else
            {
                return m_CurrentSceneUICanvas;
            }
        }
    }

    /// <summary>
    /// ĵ���� ��Ʈ Ʈ������ ��ȯ.
    /// </summary>
    public RectTransform CanvsRectTransform
    {
        get
        {
            if (CurrentUICanvas == null)
                return null;

            if (m_CurrentRectTr == null)
            {
                m_CurrentRectTr = CurrentUICanvas.GetComponent<RectTransform>();
                return m_CurrentRectTr;
            }
            else
            {
                return m_CurrentRectTr;
            }
        }
    }

    // UI ī�޶� ��ȯ.
    public Camera UICamara { get; private set; }

    /// <summary>
    /// �Ŵ��� �ʱ�ȭ.
    /// </summary>
    public override void InitManager()
    {
        base.InitManager();
        GameObject uiCamaraPrefab = ResourceManager.Instance.Load<GameObject>("Prefabs/UICamera");
        GameObject uiCamera = Instantiate(uiCamaraPrefab);
        UICamara = uiCamera.GetComponent<Camera>();
        UICamara.transform.SetParent(transform);
    }

    /// <summary>
    /// ��� UI �����. (�ı� x).
    /// </summary>
    public void AllPopPopupUI()
    {
        var e = m_DicOfUI.GetEnumerator();
        while (e.MoveNext())
        {
            var ui = e.Current.Value;
            if (ui == null)
            {
                continue;
            }

            if (ui.gameObject.activeSelf == false)
            {
                continue;
            }

            e.Current.Value.Hide();
        }
    }

    /// <summary>
    /// ��� UI �����. (�ı� o).
    /// </summary>
    public void AllPopUI()
    {
        while (m_DicOfUI.Count != 0)
        {
            PopTopUI(true);
        }
    }

    /// <summary>
    /// ��� UI ���̱�. (�ı� x).
    /// </summary>
    public void Show<T>() where T : UIBase
    {
        var ui = GetUI<T>();
        if (ui != null)
        {
            ui.Show();
        }
    }

    /// <summary>
    /// ��� UI �����. (�ı� x).
    /// </summary>
    public void Hide<T>() where T : UIBase
    {
        var ui = GetUI<T>();
        if (ui != null)
        {
            ui.Hide();
        }
    }

    /// <summary>
    /// ��� UI �����. (�ı� x).
    /// </summary>
    public void AllHideUI()
    {
        var e = m_DicOfUI.GetEnumerator();
        while (e.MoveNext())
        {
            var ui = e.Current.Value;
            if (ui == null)
            {
                continue;
            }

            if (ui.gameObject.activeSelf == false)
            {
                continue;
            }

            e.Current.Value.Hide();
        }
    }

    /// <summary>
    /// ���� ž ���̾��� ����.
    /// </summary>
    public int GetCurrentTopLayerOrder(bool isContainNoActive = false)
    {
        var currentTopUI = GetCurrentTopUI(isContainNoActive);
        if (currentTopUI == null)
        {
            if (m_CurrentSceneUI == null)
            {
                return -1;
            }

            return m_CurrentSceneUI.OrderLayer;
        }

        return currentTopUI.OrderLayer;
    }

    /// <summary>
    /// ���� ���̾ ���� UI ��������.
    /// </summary>
    public UIBase GetCurrentTopUI(bool isContainNoActive = false)
    {
        if (m_DicOfUI.Count == 0)
            return null;

        UIBase topUI = null;
        int topOrder = -1;

        var e = m_DicOfUI.GetEnumerator();
        while (e.MoveNext())
        {
            var ui = e.Current.Value;
            if (ui == null)
            {
                continue;
            }

            // ��Ȱ��ȭ.
            if (ui.gameObject.activeSelf == false && isContainNoActive == false)
            {
                continue;
            }

            if (ui.OrderLayer > topOrder)
            {
                topOrder = ui.OrderLayer;
                topUI = ui;
            }
        }

        return topUI;
    }

    /// <summary>
    /// UI ��������.
    /// </summary>
    public UIBase GetUI(System.Type type)
    {
        UIBase resultUI = null;
        if (m_DicOfUI.TryGetValue(type, out resultUI))
        {
            return resultUI;
        }

        return null;
    }

    /// <summary>
    /// UI ��������.
    /// </summary>
    public UIBase GetUI<T>() where T : UIBase
    {
        UIBase resultUI = null;
        if (m_DicOfUI.TryGetValue(typeof(T), out resultUI))
        {
            return resultUI;
        }

        return null;
    }

    /// <summary>
    /// UI ���.
    /// </summary>
    public void Pop(System.Type type)
    {
        var scenenUI = GetUI(type);
        if (scenenUI == null)
        {
            return;
        }

        m_DicOfUI.Remove(type);

        scenenUI.DisposeObject();
        Destroy(scenenUI.gameObject);
    }

    /// <summary>
    /// UI ���.
    /// </summary>
    public void Pop<T>() where T : UIBase
    {
        var scenenUI = GetUI<T>();
        if (scenenUI == null)
        {
            return;
        }

        m_DicOfUI.Remove(typeof(T));

        scenenUI.DisposeObject();
        Destroy(scenenUI.gameObject);
    }

    /// <summary>
    /// �ֻ�� UI ����.
    /// </summary>
    public void PopTopUI(bool isContainNoActive = false)
    {
        var scenenUI = GetCurrentTopUI(isContainNoActive);
        if (scenenUI != null)
        {
            m_DicOfUI.Remove(scenenUI.GetType());

            scenenUI.DisposeObject();
            Destroy(scenenUI.gameObject);
        }
    }

    /// <summary>
    /// UI ���.
    /// </summary>
    public T Push<T>(string path, System.Action onDiposeCallback = null) where T : UIBase
    {
        var ui = GetUI<T>();
        if (ui != null)
        {
            Debug.LogError("Duplicated " + typeof(T).ToString());
            return null;
        }

        GameObject go = ResourceManager.Instance.Instantiate(path);
        T uiBase = go.GetComponent<T>();
        
        if (uiBase != null)
        {
            var canvas = uiBase.Canvas;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = UICamara;
            canvas.planeDistance = 1.0f;
            canvas.sortingOrder = GetCurrentTopLayerOrder() + 1;

            uiBase.SetDisposeCallback(onDiposeCallback);
            uiBase.Initialize();
            uiBase.Show();
            uiBase.UpdateObject();

            //Debug.LogError("Create UI ::" + typeof(T).ToString());
            m_DicOfUI.Add(typeof(T), uiBase);

            return uiBase;
        }
        else
        {
            Debug.LogError("�ش� �����տ��� " + typeof(T).ToString() + " ����.");
        }
        return null;
    }
}