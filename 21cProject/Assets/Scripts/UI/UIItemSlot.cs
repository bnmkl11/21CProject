using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : PoolingObjectBase
{
    [SerializeField]
    private Image m_ImageOfCurrency;

    [SerializeField]
    private TMPro.TextMeshProUGUI m_Text;

    public override void Initialize()
    {
        
    }

    public override void DisposeObject()
    {
        base.DisposeObject();
    }

    public override void UpdateObject()
    {

    }

    /// <summary>
    /// UI °»½Å.
    /// </summary>
    public void UpdateUI(int itemCode, int qty)
    {
        m_Text.text = qty.ToString();
    }




}
