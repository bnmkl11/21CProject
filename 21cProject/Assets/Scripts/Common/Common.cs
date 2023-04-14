using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


public class Common
{

    // 기획자가 있다면, XML, JSON 과 같은 자료를 불러와서 
    // 이러한 값들을 셋팅을 진행한다.
    public const string kPREFAB_SCENE_LOGO = "Prefabs/UI/Scene/UILogo";
    public const string kPREFAB_SCENE_TITLE = "Prefabs/UI/Scene/UITitle";
    public const string kPREFAB_SCENE_LOBBY = "Prefabs/UI/Scene/UILobby";
    public const string kPREFAB_SCENE_IN_GAME = "Prefabs/UI/Scene/UIInGame";
    public const string kPREFAB_SCENE_LOADING = "Prefabs/UI/Scene/UILoading";

    public const string kPREFAB_COMMON_ITEM_SLOT = "Prefabs/UI/Common/UIItemSlot";

    public const int RANDOM_RARE_AVATA_PRICE = 0;
    public const int RANDOM_LEGEND_AVATA_PRICE = 0;

    /// <summary>
    /// 오브젝트에 UI 이벤트 더하기.
    /// </summary>
    static public void AddUIEvent(GameObject gameObject, System.Action<PointerEventData> action, kUIEVENT type)
    {
        UIEventHandler ev = Common.GetOrAddComponent<UIEventHandler>(gameObject);

        if (ev == null) return;

        switch (type)
        {
            case kUIEVENT.Click:
                ev.ClickAction = action;
                break;
            case kUIEVENT.Down:
                ev.DownAction = action;
                break;
            case kUIEVENT.Up:
                ev.UpAction = action;
                break;
            case kUIEVENT.BeginDarg:
                ev.BeginDragAction = action;
                break;
            case kUIEVENT.Drag:
                ev.OnDragAction = action;
                break;
            case kUIEVENT.ExitDrag:
                ev.ExitDragAction = action;
                break;

        }
    }

    /// <summary>
    /// 컴포넌트가 없다면 더하고 있다면 가져와서 반환.
    /// </summary>
    static public T GetOrAddComponent<T>(GameObject gameObject) where T : Component
    {
        T obj = gameObject.GetComponent<T>();

        if (obj)
        {
            return obj;
        }

        return gameObject.AddComponent<T>();
    }

    /// <summary>
    /// 자식을 찾아 반환.
    /// </summary>
    static public T FindChild<T>(GameObject parent, string childName = null) where T : UnityEngine.Component
    {
        var co = parent.GetComponent<T>();

        if (co != null && string.IsNullOrEmpty(childName))
            return co;

        if (co != null && parent.name.CompareTo(childName) == 0)
            return co;

        T result = null;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var child = parent.transform.GetChild(i);
            
            result = FindChild<T>(child.gameObject, childName);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}