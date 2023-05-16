using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIManager : SingletonBase<UIManager>
{
    // 현재 메인 씬.
    private UIBase m_CurrentSceneUI;

    // 쌓이는 UI.
    private Dictionary<System.Type, UIBase> m_DicOfUI = new Dictionary<System.Type, UIBase>();

    private Canvas          m_CurrentSceneUICanvas;
    private RectTransform   m_CurrentRectTr;

    private GameObject      m_HUDTextPrefab;
    private GameObject      m_TooltipBoxPrefab;
    private GameObject      m_HealthPrefab;
    private GameObject      m_BaseSlotPrefab;

    /// <summary>
    /// 캔버스 반환.
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
    /// 캔버스 렉트 트랜스폼 반환.
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

    // UI 카메라 반환.
    public Camera UICamara { get; private set; }

    /// <summary>
    /// 매니저 초기화.
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
    /// 모든 UI 숨기기. (파괴 x).
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
    /// 모든 UI 숨기기. (파괴 o).
    /// </summary>
    public void AllPopUI()
    {
        while (m_DicOfUI.Count != 0)
        {
            PopTopUI(true);
        }
    }

    /// <summary>
    /// 모든 UI 보이기. (파괴 x).
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
    /// 모든 UI 숨기기. (파괴 x).
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
    /// 모든 UI 숨기기. (파괴 x).
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
    /// 현재 탑 레이어의 오더.
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
    /// 가장 레이어가 높은 UI 가져오기.
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

            // 비활성화.
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
    /// UI 가져오기.
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
    /// UI 가져오기.
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
    /// UI 출력.
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
    /// UI 출력.
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
    /// 최상단 UI 삭제.
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
    /// UI 출력.
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
            Debug.LogError("해당 프리팹에는 " + typeof(T).ToString() + " 없다.");
        }
        return null;
    }
}