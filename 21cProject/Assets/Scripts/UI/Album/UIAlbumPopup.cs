using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIAlbumPopup : UIBase
{
    [SerializeField]
    private Image m_Image;

    [SerializeField]
    private TMPro.TextMeshProUGUI m_Contents;

    [SerializeField]
    private TMPro.TextMeshProUGUI m_Title;

    public void UpdateUI(int index, string contents, string title)
    {
        var sprite = ResourceManager.Instance.GetSprite(index.ToString());

        m_Image.sprite = sprite;
        m_Title.text = title;
        m_Contents.text = contents;
    }

    public void OnTouchExit()
    {
        UIManager.Instance.Pop<UIAlbumPopup>();
    }


}
