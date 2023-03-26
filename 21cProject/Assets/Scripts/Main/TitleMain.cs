using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : SceneMain
{
    public override void OnInitializeScene()
    {
        UIManager.Instance.Push<UITitle>(Common.kPREFAB_SCENE_TITLE);
        SoundManager.Instance.SetBGMVolume(0.5f);
        SoundManager.Instance.PlayBGM(kBGM.Title);
    }

    public override void ExitSceneInit()
    {
        base.ExitSceneInit();
        UIManager.Instance.Pop<UITitle>();
    }
}
