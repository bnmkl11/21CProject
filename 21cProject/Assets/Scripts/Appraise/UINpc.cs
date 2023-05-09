using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINpc : PoolingObjectBase
{
    [SerializeField]
    private Image m_ImageOfNpc;

    [SerializeField]
    private GameObject m_RootOfScript;

    [SerializeField]
    private TextMeshProUGUI m_TextOfScript;

    private ProfileData m_ProfileData;

    #region Ovrride
    
    public override void Initialize()
    {
        Common.AddUIEvent(gameObject, (pointer) =>
        {
            if ((UIManager.Instance.GetUI<UIAppraise>() as UIAppraise).GetCurrentTool() != ToolData.kTOOL_TYPE.None)
            {
                Speak();
            }
        }, kUIEVENT.Drag);
    }

    public override void UpdateObject()
    {
    }

    #endregion

    #region Update

    public void UpdateNpc(ProfileData data)
    {
        m_ProfileData = data;
    }

    public void UpdateState()
    {
    }

    #endregion

    #region Func

    public void Speak()
    {
        m_RootOfScript.SetActive(true);
        // TODO::나중에 테이블에서 가져오기
        m_TextOfScript.text = "응애";
    }

    #endregion
}
