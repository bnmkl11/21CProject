using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : UIBase
{
    [SerializeField]
    private UIProgressBar m_Progress;

    public override void Initialize()
    {
        base.Initialize();
        m_Progress.InitFloat(0.0f, 1.0f, kTEXT_TYPE.Percent);
    }

    public override void UpdateObject()
    {
        base.UpdateObject();
        StartCoroutine(StartDirection());
    }

    private IEnumerator StartDirection()
    {
        float time = 0.0f;
        while (time < 1.0f)
        {
            m_Progress.SetProgressBarFloat(time * 100, 100.0f);
            time += Time.deltaTime;
            yield return null;
        }

        SceneChanger.Instance.LoadReservedScene(UpdatePertage);
    }

    private void UpdatePertage(float percentage)
    {
        //Debug.Log("[Load]" + percentage * 100 + "%...");
    }

}
