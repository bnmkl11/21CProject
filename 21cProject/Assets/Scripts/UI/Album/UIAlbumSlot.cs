using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor.UI;
using UnityEngine;

public class UIAlbumSlot : PoolingObjectBase
{
    [SerializeField]
    private  Image m_SpriteOfAlbum;

    [SerializeField]
    private TMPro.TextMeshProUGUI m_Text;

    private int m_Index;


    public override void DisposeObject()
    {
        base.DisposeObject();
        PoolingManager.Instance.Push(this);
    }

    public override void UpdateObject()
    {
        
    }

    public override void Initialize()
    {

    }

    public void UpdateUI(int index, string text)
    {
        m_Index = index;
        var sprite = ResourceManager.Instance.GetSprite(index.ToString());

        m_SpriteOfAlbum.sprite = sprite;
        m_Text.text = text;
    }

    public void OnTouch()
    {
        UIManager.Instance.Push<UIAlbumPopup>(Common.kPREFAB_POPUP_ALBUM_POPUP);
        var popup = UIManager.Instance.GetUI<UIAlbumPopup>();
        popup.UpdateUI(m_Index, "@@", "&&&");
    }
}
