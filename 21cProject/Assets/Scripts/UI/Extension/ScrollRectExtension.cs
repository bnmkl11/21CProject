using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScrollRectExtension 
{
    /// <summary>
    /// 스크롤 뷰를 자식 트랜스폼에 지정.
    /// </summary>
    public static void SetPosition(this ScrollRect scroll, Transform childTransform)
    {
        SetPosition(scroll, childTransform.localPosition);
    }

    /// <summary>
    /// 스크롤 뷰를 특정 로컬 포지션으로 지정.
    /// </summary>
    public static void SetPosition(this ScrollRect scroll, Vector3 localPosition)
    {
        // 스크롤 뷰의 컨텐트를 레이아웃 그룹으로 캐스팅
        var layoutGroup = scroll.content.GetComponent<GridLayoutGroup>();
        // 자식 오브젝트의 크기와 레이아웃 그룹의 패딩을 고려한 Y축 위치 계산
        float targetY = localPosition.y;
        // 스크롤 뷰의 위치를 계산된 Y축 위치로 이동
        scroll.verticalNormalizedPosition = (targetY / scroll.content.rect.height);
    }


    /// <summary>
    /// 스크롤 뷰를 0.0 - 1.0 사이 셋팅.
    /// </summary>
    public static void SetPosition(this ScrollRect scroll, float value)
    {
        scroll.normalizedPosition = Vector2.up * value;
    }

    /// <summary>
    /// 스크롤 뷰를 바닥으로 갱신.
    /// </summary>
    public static void ResetBottomPosition(this ScrollRect scroll)
    {
        scroll.normalizedPosition = Vector2.zero;
    }

    /// <summary>
    /// 스크롤 뷰를 상단으로 초기화.
    /// </summary>
    /// <param name="scroll"></param>
    public static void ResetPosition(this ScrollRect scroll)
    {
        scroll.normalizedPosition = Vector2.up;
    }


}
