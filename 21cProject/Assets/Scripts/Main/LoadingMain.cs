using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMain : SceneMain
{

    public override void OnInitializeScene()
    {
        //Debug.Log("dd");
        UIManager.Instance.Push<UILoading>(Common.kPREFAB_SCENE_LOADING);
    }


    public override void ExitSceneInit()
    {
        base.ExitSceneInit();
        UIManager.Instance.Pop<UILoading>();
    }

}
