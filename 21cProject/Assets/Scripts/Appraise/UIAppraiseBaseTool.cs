using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAppraiseBaseTool : PoolingObjectBase
{
    [SerializeField]
    protected Image m_ImageOfTool;

    protected ToolData m_ToolData;

    #region Override

    public override void Initialize()
    {
        Common.AddUIEvent(gameObject, (pointer) =>
        {
            (UIManager.Instance.GetUI<UIAppraise>() as UIAppraise).UpdateAppraiseTool(m_ToolData.TOOLTYPE);
        }, kUIEVENT.BeginDarg);

        Common.AddUIEvent(gameObject, (pointer) =>
        {
            gameObject.transform.localPosition = pointer.position;
        }, kUIEVENT.Drag);
    }

    public override void UpdateObject()
    {
    
    }
    
    #endregion

    public virtual void UpdateUI(ToolData data)
    {
        m_ToolData = data;

        m_ImageOfTool.sprite = ResourceManager.Instance.GetSprite(data.IMAGE_NAME);
    }
}