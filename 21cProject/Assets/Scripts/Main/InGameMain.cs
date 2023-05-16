using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMain : SceneMain
{
    public override void OnInitializeScene()
    {
        //UIManager.Instance.Push<UIInGame>(Common.kPREFAB_SCENE_LOBBY);
        UIManager.Instance.Push<UIInGame>(Common.kPREFAB_SCENE_IN_GAME);
        SoundManager.Instance.SetBGMVolume(0.5f);
        SoundManager.Instance.PlayBGM(kBGM.Lobby);

        //TableManager.Instance.GetData();
    }
}
