using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class GameManager : SingletonBase<GameManager>
{
    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;


    #region Override.
    public override void InitManager()
    {
        StartGame();
    }
    #endregion Override.

    public void StartGame()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
        }
    }
}
