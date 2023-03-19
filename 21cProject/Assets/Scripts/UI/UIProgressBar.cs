using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum kTEXT_TYPE
{
    None,
    Percent, // 프로그레스바 위의 텍스트를 퍼센트로 표기할것인지.
    Value   // 프로그레스바 위의 텍스트를 숫자로 표기할 것인지.
}

public class UIProgressBar : MonoBehaviour
{
    [SerializeField]
    private kTEXT_TYPE m_TextStyle;

    //프로그레스의 이미지.
    private Image m_ProgressImage;

    // 프로그레스의 배경 이미지.
    private Image m_ProgressBackImage;
    private Coroutine m_Corutine;
    private TMPro.TextMeshProUGUI m_Text;

    private void Awake()
    {
        m_ProgressBackImage = Common.FindChild<Image>(gameObject, "ProgressBarBack");
        m_ProgressImage = Common.FindChild<Image>(gameObject, "ProgressBar");
        m_Text = Common.FindChild<TMPro.TextMeshProUGUI>(gameObject, "TextBar");

        m_ProgressImage.type = Image.Type.Filled;
    }

    /// <summary>
    /// float 프로그레스바 초기화.
    /// </summary>
    public void InitFloat(float value, float maxValue, kTEXT_TYPE textStyle)
    {
        m_ProgressImage.fillAmount = value / maxValue;
        this.m_TextStyle = textStyle;
        if (m_ProgressBackImage == null) return;
        m_ProgressBackImage.fillAmount = m_ProgressImage.fillAmount;
    }

    /// <summary>
    /// int 프로그레스바 초기화.
    /// </summary>
    public void InitInt(int value, int maxValue, kTEXT_TYPE textStyle)
    {
        m_ProgressImage.fillAmount = value / maxValue;

        this.m_TextStyle = textStyle;

        SetText(value, maxValue);
        if (m_ProgressBackImage == null) return;
        m_ProgressBackImage.fillAmount = m_ProgressImage.fillAmount;
    }

    // 텍스트 셋팅.
    private void SetText(int value, int maxValue)
    {
        if (m_Text == null) return;

        switch (m_TextStyle)
        {
            case kTEXT_TYPE.None:
                m_Text.text = "";
                break;
            case kTEXT_TYPE.Value:
                m_Text.text = $"{value} / {maxValue}";
                break;
            case kTEXT_TYPE.Percent:
                m_Text.text = $"{(value / (float)maxValue) * 100}%";
                break;
        }
    }


    /// <summary>
    /// 프로그레스바 조정.
    /// </summary>
    public void SetProgressBarFloat(float currentValue, float maxValue)
    {
        m_ProgressImage.fillAmount = currentValue / (float)maxValue;
        SetText((int)currentValue, (int)maxValue);

        if (m_ProgressBackImage == null) return;
        if (m_Corutine != null)
        {
            StopCoroutine(m_Corutine);
        }

        m_Corutine = StartCoroutine(ProgressBar_C(0.4f));
    }

    /// <summary>
    /// 프로그레스바 조정.
    /// </summary>
    public void SetProgressBarInt(int currentValue, int maxValue)
    {
        m_ProgressImage.fillAmount = currentValue / (float)maxValue;
        SetText(currentValue, maxValue);

        if (m_ProgressBackImage == null) return;
        if (m_Corutine != null)
        {
            StopCoroutine(m_Corutine);
        }

        m_Corutine = StartCoroutine(ProgressBar_C(0.4f));
    }

    // 프로그레스바 연출 코루틴.
    private IEnumerator ProgressBar_C(float time)
    {
        yield return new WaitForSeconds(time);

        while (m_ProgressImage.fillAmount < m_ProgressBackImage.fillAmount)
        {
            m_ProgressBackImage.fillAmount -= 0.1f * Time.deltaTime;
            yield return null;
        }

        m_ProgressBackImage.fillAmount = m_ProgressImage.fillAmount;

    }

    // 스프라이트 셋팅.
    public void SetSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            m_ProgressImage.gameObject.SetActive(false);
            return;
        }
        m_ProgressImage.gameObject.SetActive(true);
        m_ProgressImage.sprite = sprite;
    }





}