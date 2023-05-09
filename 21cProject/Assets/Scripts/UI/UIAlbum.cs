using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class UIAlbum : UIBase
{
    public enum kTYPE
    {
        None = 0,
        Album,
        Pictures
    }


    [SerializeField]
    private GameObject m_RootOfAlbum;

    [SerializeField]
    private GameObject m_RootOfPictures;

    [SerializeField]
    private GameObject m_RootOfMain;

    [SerializeField]
    private GridLayoutGroup m_GridOfAlbum;

    [SerializeField]
    private GridLayoutGroup m_GridOfPicture;

    [SerializeField]
    private ScrollRect m_ScrollOfAlbum;

    [SerializeField]
    private ScrollRect m_ScrollOfPicture;

    private List<UIAlbumSlot> m_ListOfAlbumSlot = new List<UIAlbumSlot>(); 
    
    private List<UIAlbumSlot> m_ListOfPictureSlot = new List<UIAlbumSlot>();

    private kTYPE m_Type;


    public override void Initialize()
    {
        base.Initialize();

        UIManager.Instance.Hide<UITitle>();
        BindUI<Button>(gameObject);
        BindUI<GridLayoutGroup>(gameObject);
        BindUI<ScrollRect>(gameObject);

        Common.DisposeList(m_ListOfAlbumSlot);
        Common.DisposeList(m_ListOfPictureSlot);

        var buttonObject = GetButton("ButtonAlbum");
        buttonObject.onClick.AddListener(OnTouchAlbum);

        buttonObject = GetButton("ButtonPicture");
        buttonObject.onClick.AddListener(OnTouchPicture);

        PoolingManager.Instance.CreatePool<UIAlbumSlot>(Common.kPREFAB_POPUP_ALBUM_SLOT);

        OnTouchNone();
    }

    public void UpdateAlbum()
    {
        m_Type = kTYPE.Album;
        Common.DisposeList(m_ListOfAlbumSlot);
        Common.DisposeList(m_ListOfPictureSlot);

        for (int i = 0; i < 2; i++)
        {
            var slot = PoolingManager.Instance.Pop<UIAlbumSlot>(m_GridOfAlbum.transform);
            slot.UpdateUI(i + 2, "My Black Flame Dragon...");
            slot.Show();

            m_ListOfAlbumSlot.Add(slot);
        }
    }

    public void UpdatePicture()
    {
        m_Type = kTYPE.Pictures;
        Common.DisposeList(m_ListOfAlbumSlot);
        Common.DisposeList(m_ListOfPictureSlot);

        for (int i = 0; i < 2; i++)
        {
            var slot = PoolingManager.Instance.Pop<UIAlbumSlot>(m_GridOfPicture.transform);
            slot.UpdateUI(i, "Wa~" + i);
            slot.Show();

            m_ListOfPictureSlot.Add(slot);
        }
    }

    public void OnTouchNone()
    {
        m_RootOfAlbum.SetActive(false);
        m_RootOfPictures.SetActive(false);
        m_RootOfMain.SetActive(true);
    }

    public void OnTouchAlbum()
    {
        m_RootOfMain.SetActive(false);
        m_RootOfAlbum.SetActive(true);
        m_RootOfPictures.SetActive(false);
        UpdateAlbum();
    }

    public void OnTouchPicture()
    {
        m_RootOfMain.SetActive(false);
        m_RootOfAlbum.SetActive(false);
        m_RootOfPictures.SetActive(true);
        UpdatePicture();
    }
}
