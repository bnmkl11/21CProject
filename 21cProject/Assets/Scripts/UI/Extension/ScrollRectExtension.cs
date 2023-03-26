using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScrollRectExtension 
{
    /// <summary>
    /// ��ũ�� �並 �ڽ� Ʈ�������� ����.
    /// </summary>
    public static void SetPosition(this ScrollRect scroll, Transform childTransform)
    {
        SetPosition(scroll, childTransform.localPosition);
    }

    /// <summary>
    /// ��ũ�� �並 Ư�� ���� ���������� ����.
    /// </summary>
    public static void SetPosition(this ScrollRect scroll, Vector3 localPosition)
    {
        // ��ũ�� ���� ����Ʈ�� ���̾ƿ� �׷����� ĳ����
        var layoutGroup = scroll.content.GetComponent<GridLayoutGroup>();
        // �ڽ� ������Ʈ�� ũ��� ���̾ƿ� �׷��� �е��� ����� Y�� ��ġ ���
        float targetY = localPosition.y;
        // ��ũ�� ���� ��ġ�� ���� Y�� ��ġ�� �̵�
        scroll.verticalNormalizedPosition = (targetY / scroll.content.rect.height);
    }


    /// <summary>
    /// ��ũ�� �並 0.0 - 1.0 ���� ����.
    /// </summary>
    public static void SetPosition(this ScrollRect scroll, float value)
    {
        scroll.normalizedPosition = Vector2.up * value;
    }

    /// <summary>
    /// ��ũ�� �並 �ٴ����� ����.
    /// </summary>
    public static void ResetBottomPosition(this ScrollRect scroll)
    {
        scroll.normalizedPosition = Vector2.zero;
    }

    /// <summary>
    /// ��ũ�� �並 ������� �ʱ�ȭ.
    /// </summary>
    /// <param name="scroll"></param>
    public static void ResetPosition(this ScrollRect scroll)
    {
        scroll.normalizedPosition = Vector2.up;
    }


}
