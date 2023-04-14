using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

[RequireComponent(typeof(Canvas))]
public class UIBase : PoolingObjectBase
{
    protected Dictionary<Type, List<UnityEngine.Component>> m_DicUIObjects = new Dictionary<Type, List<UnityEngine.Component>>();

    [SerializeField]
    // ���� ������ �Ǵ� UI����.
    private bool m_IsMainScene;
    public bool IsMainScene{ get => m_IsMainScene; }


    private int m_OrderLayer;
    public int OrderLayer { get => m_OrderLayer; }

    private Canvas m_Canvas;

    /// <summary>
    /// ĵ���� ��ȯ.
    /// </summary>
    public Canvas Canvas
    {
        get
        {
            return m_Canvas;
        }
    }



    public override void Initialize()
    {

    }
    public override void DisposeObject()
    {
        gameObject.SetActive(false);
    }
    public override void UpdateObject()
    {
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        m_Canvas = GetComponent<Canvas>();
        if (m_Canvas)
        {
            m_OrderLayer = GetComponent<Canvas>().sortingOrder;
        }
        else
        {
            m_OrderLayer = 0;
        }
    }

    /// <summary>
    /// ��Ʈ������ UI ��������.
    /// </summary>
    protected T GetUI<T>(string objectName) where T : UnityEngine.Component
    {
        List<UnityEngine.Component> list = null;
        if (m_DicUIObjects.TryGetValue(typeof(T), out list))
        {
            var result = list.Find(go => go.name.CompareTo(objectName) == 0);
            if (result == null)
                return null;

            return result as T;
        }

        return null;
    }

    /// <summary>
    /// �ε����� UI ��������.
    /// </summary>
    protected T GetUI<T>(int idx = 0) where T : UnityEngine.Component
    {
        List<UnityEngine.Component> list = null;
        if (m_DicUIObjects.TryGetValue(typeof(T), out list))
        {
            return list[idx] as T;
        }

        return null;
    }

    /// <summary>
    /// ����Ʈ�� ��� ��Ʈ�� �̸��� ���� UI ã�Ƽ� ����Ʈ�� ����ֱ�.
    /// </summary>
    protected void BindUI<T>(List<string> nameList) where T : UnityEngine.Component
    {
        foreach (string name in nameList)
        {
            T child = Common.FindChild<T>(gameObject, name);
            InsertUIObject(child);
        }
    }

    /// <summary>
    /// enum Ÿ���� �ѱ�� enum�� ������ �̸���� UI ã�Ƽ� ����Ʈ�� ����ֱ�.
    /// </summary>
    protected void BindUI<T>(Type type) where T : UnityEngine.Component
    {
        string[] names = Enum.GetNames(type);
        BindUI<T>(names.ToList());
    }

    /// <summary>
    /// �ش� Ÿ���� �ڽ��� ã�� ����.
    /// </summary>
    protected void BindUI<T>(GameObject parent) where T : UnityEngine.Component
    {
        var co = parent.GetComponent<T>();
        if (co != null)
        {
            InsertUIObject(co);
        }

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var child = parent.transform.GetChild(i);
            BindUI<T>(child.gameObject);
        }
    }

    // UI ������Ʈ ��ųʸ� ����.
    protected void InsertUIObject<T>(T obj) where T : Component
    {
        List<Component> newList = null;
        if (!m_DicUIObjects.TryGetValue(typeof(T), out newList))
        {
            newList = new List<Component>();
            newList.Add(obj);
            m_DicUIObjects.Add(typeof(T), newList);
        }
        else
        {
            // �̹� ������ ���.
            if (newList.Any(foundData => foundData == obj))
                return;
            
            newList.Add(obj);
        }
    }

    public Button GetButton(string str) { return GetUI<Button>(str); }
    public Image GetImage(string str) { return GetUI<Image>(str); }
    public TMPro.TextMeshProUGUI GetText(string str) { return GetUI<TMPro.TextMeshProUGUI>(str); }
    public TMPro.TMP_InputField GetInputField(string str) { return GetUI<TMPro.TMP_InputField>(str); }

    public Button GetButton(int idx) { return GetUI<Button>(idx); }
    public Image GetImage(int idx) { return GetUI<Image>(idx); }
    public TMPro.TextMeshProUGUI GetText(int idx) { return GetUI<TMPro.TextMeshProUGUI>(idx); }
    public TMPro.TMP_InputField GetInputField(int idx) { return GetUI<TMPro.TMP_InputField>(idx); }


}