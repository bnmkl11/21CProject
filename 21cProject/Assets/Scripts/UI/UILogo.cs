using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILogo : UIBase
{
    [SerializeField]
    private Image m_Image;

    [SerializeField]
    private string kPATH_LOGO = "Image/Logo";

    public override void Initialize()
    {
        base.Initialize();
        m_Image.sprite = ResourceManager.Instance.Load<Sprite>(kPATH_LOGO);
    }

    public void OnEndAnimation()
    {
        UIManager.Instance.Pop<UILogo>();
        UIManager.Instance.Push<UITitle>(Common.kPREFAB_SCENE_TITLE);
    }

}
