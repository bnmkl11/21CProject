using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITitle : UIBase
{
    public override void Initialize()
    {
        base.Initialize();

        BindUI<Button>(gameObject);

        var buttonObject = GetButton("ButtonStart");
        buttonObject.onClick.AddListener(OnTouchStart);
    }

    public void OnTouchStart()
    {
        SceneChanger.Instance.LoadScene(kSCNENE_TYPE.Lobby);
    }

}
