/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-23 10:12:12 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-23 10:12:12 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SuperScrollView;

/// <summary>  </summary>
public class UIGameChapterListCtrl : GridViewVerticalCtrlBase {
    
    private List<TransferData> m_TransferDataList;
    public void Init(List<TransferData> list)
    {
        m_TransferDataList = list;
        Init(Resources.Load<GameObject>("UIPrefab/Item/GameLevel/UIGameChapterItem"));
    }

    protected override LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= DataManger.TotalItemCount)
        {
            return null;
        }
        GridItemData itemData = DataManger.GetItemDataByIndex(index);
        if (itemData == null)
        {
            return null;
        }
        LoopListViewItem2 item = listView.NewListViewItem(ItemName);

        for (int i = 0; i < row; i++)
        {
            int itemIndex = index * row + i;
            UIGameChapterItem itemScript = item.transform.GetChild(i).GetComponent<UIGameChapterItem>();
            if (itemIndex >= DataManger.TotalDataCount)
            {
                itemScript.gameObject.SetActive(false);
                continue;
            }
            else
            {
                itemScript.gameObject.SetActive(true);
            }
            itemScript.Init();
            itemScript.SetData(m_TransferDataList[itemIndex]);
        }
        return item;
    }
}
