using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICurrencySlot : PoolingObjectBase
{
    [SerializeField]
    private Image m_ImageOfCurrency;

    [SerializeField]
    private TMPro.TextMeshPro m_Text;

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void DisposeObject()
    {
        base.DisposeObject();
    }

    public override void UpdateObject()
    {
        
    }

    public void UpdateUI()
    {

    }




}
