using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMain : SceneMain
{
    public override void OnInitializeScene()
    {
        UIManager.Instance.Push<UILobby>(Common.kPREFAB_SCENE_LOBBY);
        SoundManager.Instance.SetBGMVolume(0.5f);
        SoundManager.Instance.PlayBGM(kBGM.Lobby);
    }
}
