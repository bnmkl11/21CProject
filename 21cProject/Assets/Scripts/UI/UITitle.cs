using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;


public class UITitle : UIBase
{
    [SerializeField]
    private Light2D m_Light;

    [SerializeField]
    private Light2D m_RedLight;

    public override void Initialize()
    {
        base.Initialize();

        BindUI<Button>(gameObject);

        SoundManager.Instance.SetBGMVolume(0.5f);
        SoundManager.Instance.PlayBGM(kBGM.Title);

        var buttonObject = GetButton("ButtonStart");
        buttonObject.onClick.AddListener(OnTouchStart);

        StartCoroutine(StartLightCorotine());
        StartCoroutine(StartLightCorotine2());
    }

    IEnumerator StartLightCorotine()
    {
        while (true)
        {
            m_Light.intensity = 0.0f;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
            m_Light.intensity = 1.0f;

            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
    }

    IEnumerator StartLightCorotine2()
    {
        float value = 0.0f;
        while (true)
        {
            m_RedLight.intensity = Mathf.Sin(value += Time.deltaTime * 3.0f);
            yield return null;
        }
    }

    public void OnTouchStart()
    {
        SceneChanger.Instance.LoadScene(kSCNENE_TYPE.Lobby);
    }

}
