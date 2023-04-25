using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class UIAlbum : UIBase
{
    public override void Initialize()
    {
        base.Initialize();

        UIManager.Instance.Hide<UITitle>();
        BindUI<Button>(gameObject);

        var buttonObject = GetButton("ButtonAlbum");
        buttonObject.onClick.AddListener(OnTouchAlbum);

        buttonObject = GetButton("ButtonPicture");
        buttonObject.onClick.AddListener(OnTouchPicture);
    }

    public void OnTouchAlbum()
    {

    }

    public void OnTouchPicture()
    {

    }
}
