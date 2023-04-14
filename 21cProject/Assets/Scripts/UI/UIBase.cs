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
    // 씬의 메인이 되는 UI인지.
    private bool m_IsMainScene;
    public bool IsMainScene{ get => m_IsMainScene; }


    private int m_OrderLayer;
    public int OrderLayer { get => m_OrderLayer; }

    private Canvas m_Canvas;

    /// <summary>
    /// 캔버스 반환.
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
    /// 스트링으로 UI 가져오기.
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
    /// 인덱스로 UI 가져오기.
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
    /// 리스트에 담긴 스트링 이름을 가진 UI 찾아서 리스트에 집어넣기.
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
    /// enum 타입을 넘기면 enum에 선언한 이름대로 UI 찾아서 리스트에 집어넣기.
    /// </summary>
    protected void BindUI<T>(Type type) where T : UnityEngine.Component
    {
        string[] names = Enum.GetNames(type);
        BindUI<T>(names.ToList());
    }

    /// <summary>
    /// 해당 타입의 자식을 찾아 저장.
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

    // UI 오브젝트 딕셔너리 삽입.
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
            // 이미 삽입한 경우.
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