using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILobby : UIBase
{
    private ScrollRect m_ScrollRect;

    private GridLayoutGroup m_Grid;

    private List<UIItemSlot> m_ListOfItemSlot = new List<UIItemSlot>();

    public override void Initialize()
    {
        base.Initialize();

        BindUI<GridLayoutGroup>(gameObject);
        BindUI<ScrollRect>(gameObject);

        m_ScrollRect    = GetUI<ScrollRect>();
        m_Grid          = GetUI<GridLayoutGroup>();

        PoolingManager.Instance.CreatePool<UIItemSlot>(Common.kPREFAB_COMMON_ITEM_SLOT);

        for (int i = 0; i< 50; i++)
        {
            var slot = PoolingManager.Instance.Pop<UIItemSlot>(m_Grid.transform);
            slot.Show();
            slot.UpdateUI(1, i);
            m_ListOfItemSlot.Add(slot);
        }

      
    }




    public void OnTouchStart()
    {

        m_Grid.CalculateLayoutInputHorizontal();

        m_Grid.CalculateLayoutInputVertical();

        m_ScrollRect.SetPosition(m_ListOfItemSlot[23].transform);
    }

}
