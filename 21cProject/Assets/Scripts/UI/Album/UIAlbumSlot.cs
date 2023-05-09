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
        var sprite = ResourceManager.Instance.GetSprite(index.ToString());

        m_SpriteOfAlbum.sprite = sprite;
        m_Text.text = text;
    }

    public void OnTouch()
    {

    }
}
