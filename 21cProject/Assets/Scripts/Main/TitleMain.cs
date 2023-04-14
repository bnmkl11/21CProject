using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : SceneMain
{
    public override void OnInitializeScene()
    {
        UIManager.Instance.Push<UILogo>(Common.kPREFAB_SCENE_LOGO);
    }

    public override void ExitSceneInit()
    {
        base.ExitSceneInit();
        UIManager.Instance.Pop<UITitle>();
    }
}
