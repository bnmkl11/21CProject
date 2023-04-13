using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAppraiseBaseTool : PoolingObjectBase
{
    [SerializeField]
    private Image m_ImageOfTool;

    private ToolData m_ToolData;

    #region Override

    public override void Initialize()
    {
    }

    public override void UpdateObject()
    {
    }
    
    #endregion

    public virtual void Update(ToolData data)
    {
        m_ToolData = data;

        m_ImageOfTool.sprite = ResourceManager.Instance.GetSprite(data.IMAGE_NAME);
    }

    protected virtual void hi()
    {
        
    }
}