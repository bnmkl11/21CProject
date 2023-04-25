using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINpc : PoolingObjectBase
{
    [SerializeField]
    private Image m_ImageOfNpc;

    private ProfileData m_ProfileData;

    #region Ovrride
    
    public override void Initialize()
    {
    }

    public override void UpdateObject()
    {
    }

    #endregion

    public void UpdateNpc(ProfileData data)
    {
        m_ProfileData = data;
    }

    public void UpdateState()
    {
        
    }
}
