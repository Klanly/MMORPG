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

/// <summary> 纵向滑动类表基类 </summary>
public class GridViewVerticalCtrlBase : MonoBehaviour {

    public LoopListView2 LoopListView;
    public ScrollViewData DataManger;
    public int SpacingX = 3;
    public int SpacingY = 10;
    [HideInInspector]
    public string ItemName;
    [HideInInspector]
    public int row = 1;
    public int TotalCount = 20;
    private Transform m_Content;
    public ListItemArrangeType ArrangeType = ListItemArrangeType.TopToBottom;
    public void Init(GameObject obj)
    {
        LoopListView.ArrangeType = ArrangeType;
        m_Content = LoopListView.transform.Find("Viewport/Content");
        //滑动列表 Content Rect
        Rect svRt = m_Content.GetComponent<RectTransform>().rect;
        ItemName = "Container";
        GameObject container = new GameObject(ItemName);
        //GameObject itemObj = Resources.Load<GameObject>("UIPrefab/Item_01");
        GameObject itemObj = obj;
        Rect itemRect = itemObj.GetComponent<RectTransform>().rect;
        float height = itemRect.height;
        RectTransform rt = container.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(svRt.width, height);
        row = Mathf.FloorToInt((svRt.width + SpacingX) / (itemRect.width + SpacingX));
        for (int i = 0; i < row; i++)
        {
            GameObject insObj = Instantiate(itemObj);
            insObj.transform.SetParent(container.transform);
            insObj.transform.localScale = Vector2.one;
            insObj.transform.localPosition = new Vector2(-(svRt.width / 2 - itemRect.width / 2) + i * (itemRect.width + SpacingX), 0);
        }
        container.transform.SetParent(m_Content);
        //container.transform.localPosition = Vector3.zero;
        container.transform.localScale = Vector3.one;
        switch (LoopListView.ArrangeType)
        {
            case ListItemArrangeType.TopToBottom:
            case ListItemArrangeType.BottomToTop:
                rt.anchorMin = new Vector2(0, 1f);
                rt.anchorMax = new Vector2(0, 1f);
                rt.pivot = new Vector2(0, 1);
                break;
            case ListItemArrangeType.LeftToRight:
            case ListItemArrangeType.RightToLeft:

                break;
        }
        ItemPrefabConfData itemData = new ItemPrefabConfData();
        itemData.mItemPrefab = container;
        itemData.mPadding = SpacingY;
        itemData.mInitCreateCount = 0;
        itemData.mStartPosOffset = SpacingX;

        List<ItemPrefabConfData> list = new List<ItemPrefabConfData>();
        list.Add(itemData);
        LoopListView.mItemPrefabDataList = list;


        DataManger = new ScrollViewData();
        DataManger.TotalDataCount = TotalCount;
        int count = DataManger.TotalDataCount / row;
        if (DataManger.TotalDataCount % row > 0)
        {
            count++;
        }
        DataManger.Init();
        //mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount, OnGetItemByIndex);
        LoopListView.InitListView(count, OnGetItemByIndex);
    }

    /// <summary> 子类必须重写OnGetItemByIndex 参照下方注释的OnGetItemByIndex1 </summary>
    /// <param name="listView"></param>
    /// <param name="index"></param>
    /// <returns></returns>
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

    //    for (int i = 0; i < row; i++)
    //    {
    //        int itemIndex = index * row + i;
    //        EquipItem itemScript = item.transform.GetChild(i).GetComponent<EquipItem>();

    //        if (itemIndex >= DataManger.TotalDataCount)
    //        {
    //            itemScript.gameObject.SetActive(false);
    //            continue;
    //        }
    //        if (item.IsInitHandlerCalled == false)
    //        {
    //            item.IsInitHandlerCalled = true;
    //        }
    //        itemScript.Init();
    //        itemScript.SetData(itemIndex, UnityEngine.Random.Range(1, 6), SpriteArray[UnityEngine.Random.Range(0, SpriteArray.Length)].name, UnityEngine.Random.Range(0, 2) == 1,
    //            UnityEngine.Random.Range(1, 1000), UnityEngine.Random.Range(1, 6));
    //        itemScript.GetComponent<Button>().onClick.RemoveAllListeners();
    //        itemScript.GetComponent<Button>().onClick.AddListener(delegate
    //        {
    //            Debug.Log(itemScript.Index);
    //        });
    //    }

    //    return item;
    //}

    /// <summary> 移动到指定索引(0开始) </summary>
    /// <param name="index"></param>
    public void MoveToIndex(int index)
    {
        LoopListView.MovePanelToItemIndex(index / row, 0);
    }

    /// <summary> 设置List列表个数(列表更改后需要重新设置) </summary>
    /// <param name="count"></param>
    public void SetListItemTotalCount(int count)
    {
        DataManger.TotalDataCount = count;
        DataManger.SetDataTotalCount(DataManger.TotalDataCount);
        LoopListView.SetListItemCount(DataManger.TotalDataCount, false);
        LoopListView.RefreshAllShownItem();
    }
}
