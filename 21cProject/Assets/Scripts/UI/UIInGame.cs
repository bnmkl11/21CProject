using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInGame : UIBase
{
    #region Override

    public override void Initialize()
    {
        base.Initialize();

        BindUI<GridLayoutGroup>(gameObject);
        BindUI<ScrollRect>(gameObject);

        InitPool();
    }

    #endregion

    #region Init

    private void InitPool()
    {
        PoolingManager.Instance.CreatePool<UIAppraiseBaseTool>(Common.kPREFAB_INGAME_APPRAISETOOL);
        PoolingManager.Instance.CreatePool<UINpc>(Common.kPREFAB_INGAME_NPC);
    }

    #endregion

    #region OnTouch
    
    public void OnTouchStart()
    {
        
    }

    #endregion
}
