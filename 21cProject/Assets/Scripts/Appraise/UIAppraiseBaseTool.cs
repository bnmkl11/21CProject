using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIAppraiseBaseTool : PoolingObjectBase, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    protected Image m_ImageOfTool;

    protected ToolData m_ToolData;

    #region Override

    public override void Initialize()
    {
        //Common.AddUIEvent(gameObject, (pointer) =>
        //{
        //    (UIManager.Instance.GetUI<UIAppraise>() as UIAppraise).UpdateAppraiseTool(m_ToolData.TOOLTYPE);
        //    print("begindrag succes");
        //}, kUIEVENT.BeginDarg);

        //Common.AddUIEvent(gameObject, (pointer) =>
        //{
        //    gameObject.transform.localPosition = pointer.position;
        //}, kUIEVENT.Drag);
    }

    public override void UpdateObject() {}

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta / transform.parent.gameObject.GetComponent<Canvas>().scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var appraise = GameObject.FindGameObjectWithTag("Appraise").GetComponent<UIAppraise>();
        appraise.UpdateAppraiseTool(ToolData.kTOOL_TYPE.READING_GLASSESS);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var appraise = GameObject.FindGameObjectWithTag("Appraise").GetComponent<UIAppraise>();
        appraise.UpdateAppraiseTool(ToolData.kTOOL_TYPE.None);
    }

    #endregion

    public virtual void UpdateUI(ToolData data)
    {
        m_ToolData = data;

        m_ImageOfTool.sprite = ResourceManager.Instance.GetSprite(data.IMAGE_NAME);
    }
}