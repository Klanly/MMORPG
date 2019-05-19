/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：#CreateTime# 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：#CreateTime# 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SuperScrollView;
using UnityEngine.UI;

/// <summary> 横向滑动列表基类 </summary>
public class GridViewHorizontalCtrlBase : MonoBehaviour
{
    public LoopListView2 LoopListView;
    public ScrollViewData DataManger;
    public int SpacingX = 3;
    public int SpacingY = 10;
    public int TotalCount = 20;
    [HideInInspector]
    public string ItemName;
    private Transform m_Content;
    public ListItemArrangeType ArrangeType = ListItemArrangeType.LeftToRight;
    public void Init(GameObject obj)
    {
        LoopListView.ArrangeType = ArrangeType;
        m_Content = LoopListView.transform.Find("Viewport/Content");
        GameObject insObj = Instantiate(obj);
        ItemName = insObj.name = "Item";
        RectTransform rt = insObj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 1f);
        rt.anchorMax = new Vector2(0, 1f);
        rt.pivot = new Vector2(0, 1);
        insObj.transform.SetParent(m_Content);
        insObj.transform.localScale = Vector2.one;
        ItemPrefabConfData itemData = new ItemPrefabConfData();
        itemData.mItemPrefab = insObj;
        itemData.mPadding = SpacingY;
        itemData.mInitCreateCount = 0;
        itemData.mStartPosOffset = SpacingX;
        List<ItemPrefabConfData> list = new List<ItemPrefabConfData>();
        list.Add(itemData);
        LoopListView.mItemPrefabDataList = list;
        
        DataManger = new ScrollViewData();
        DataManger.TotalDataCount = TotalCount;
        DataManger.Init();
        LoopListView.InitListView(DataManger.TotalDataCount, OnGetItemByIndex);
    }

    protected virtual LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        return null;
    }

    //LoopListViewItem2 OnGetItemByIndex1(LoopListView2 listView, int index)
    //{
    //    if (index < 0 || index >= DataManger.TotalItemCount)
    //    {
    //        return null;
    //    }
    //    ItemData itemData = DataManger.GetItemDataByIndex(index);
    //    if (itemData == null)
    //    {
    //        return null;
    //    }
    //    LoopListViewItem2 item = listView.NewListViewItem(ItemName);

    //    EquipItem itemScript = item.GetComponent<EquipItem>();
    //    if (item.IsInitHandlerCalled == false)
    //    {
    //        item.IsInitHandlerCalled = true;
    //    }
    //    itemScript.Init();
    //    itemScript.SetData(index, UnityEngine.Random.Range(1, 6), SpriteArray[UnityEngine.Random.Range(0, SpriteArray.Length)].name, UnityEngine.Random.Range(0, 2) == 1,
    //        UnityEngine.Random.Range(1, 1000), UnityEngine.Random.Range(1, 6));
    //    itemScript.GetComponent<Button>().onClick.RemoveAllListeners();
    //    itemScript.GetComponent<Button>().onClick.AddListener(delegate
    //    {
    //        Debug.Log(itemScript.Index);
    //    });

    //    return item;
    //}

    /// <summary> 移动到指定索引(0开始) </summary>
    /// <param name="index"></param>
    public void MoveToIndex(int index)
    {
        LoopListView.MovePanelToItemIndex(index, 0);
    }

    /// <summary> 设置List列表个数(列表更改后需要重新设置) </summary>
    /// <param name="count"></param>
    void SetListItemTotalCount(int count)
    {
        DataManger.TotalDataCount = count;
        DataManger.SetDataTotalCount(DataManger.TotalDataCount);
        LoopListView.SetListItemCount(DataManger.TotalDataCount, false);
        LoopListView.RefreshAllShownItem();
    }
}
