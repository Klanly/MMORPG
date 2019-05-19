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

/// <summary>  </summary>
public class LevelLimitCtrlBase : MonoBehaviour {

    /// <summary> 最小等级限制列表 </summary>
    public LoopListView2 LoopListViewMin;
    /// <summary> 最大等级限制列表 </summary>
    public LoopListView2 LoopListViewMax;
    /// <summary> 列表x轴偏移 </summary>
    public int SpacingX = 0;
    /// <summary> 列表x轴偏移 </summary>
    public int SpacingY = 10;
    /// <summary> 最高等级 </summary>
    public int MaxLevel = 100;
    private int m_LevelMin = 1;
    private int m_LevelMax = 1;

    public int LevelMin { get { return m_LevelMin; } }
    public int LevelMax { get { return m_LevelMax; } }
    private ListItemArrangeType ArrangeType = ListItemArrangeType.TopToBottom;
    private Transform[] m_ContentArray = new Transform[2];

    [HideInInspector]
    public List<string> ItemNameList;
    public void Init(List<GameObject> objList)
    {
        m_ContentArray[0] = LoopListViewMin.transform.Find("Viewport/Content");
        m_ContentArray[1] = LoopListViewMax.transform.Find("Viewport/Content");
        LoopListViewMin.ArrangeType = LoopListViewMax.ArrangeType = ArrangeType;
        ItemNameList = new List<string>();
        List<ItemPrefabConfData> list = new List<ItemPrefabConfData>();
        for (int i = 0; i < 2; i++)
        {
            GameObject insObj = Instantiate(objList.Count>1? objList[i]: objList[0]);
            insObj.name = i==0?"ItemMin":"ItemMax";
            ItemNameList.Add(insObj.name);
            RectTransform rt = insObj.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 1f);
            rt.anchorMax = new Vector2(0, 1f);
            rt.pivot = new Vector2(0, 1);
            insObj.transform.SetParent(m_ContentArray[i]);
            insObj.transform.localScale = Vector2.one;
            ItemPrefabConfData itemData = new ItemPrefabConfData();
            itemData.mItemPrefab = insObj;
            itemData.mPadding = SpacingY;
            itemData.mInitCreateCount = 3;
            itemData.mStartPosOffset = SpacingX;
            list.Add(itemData);
        }
        List<ItemPrefabConfData> list0 = new List<ItemPrefabConfData>();
        list0.Add(list[0]);
        List<ItemPrefabConfData> list1 = new List<ItemPrefabConfData>();
        list1.Add(list[1]);
        LoopListViewMin.mItemPrefabDataList = list0;
        LoopListViewMax.mItemPrefabDataList = list1;
        LoopListViewMin.InitListView(-1, OnGetMinItemByIndex);
        LoopListViewMax.InitListView(-1, OnGetMaxItemByIndex);
        LoopListViewMin.mOnSnapNearestChanged = OnMinLevelChanged;
        LoopListViewMax.mOnSnapNearestChanged = OnMaxLevelChanged;
    }

    private LoopListViewItem2 OnGetMinItemByIndex(LoopListView2 listView, int index)
    {
        LoopListViewItem2 item = listView.NewListViewItem(ItemNameList[0]);
        LevelLimitItem itemScript = item.GetComponent<LevelLimitItem>();
        if (item.IsInitHandlerCalled == false)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init();
        }
        int firstItemVal = 1;
        int itemCount = MaxLevel;
        int val = 0;
        if (index >= 0)
        {
            val = index % itemCount;
        }
        else
        {
            val = itemCount + ((index + 1) % itemCount) - 1;
        }
        val = val + firstItemVal;
        itemScript.Level = val;
        itemScript.SetData(val.ToString());
        LoopListViewMax.RefreshAllShownItem();
        return item;
    }

    private LoopListViewItem2 OnGetMaxItemByIndex(LoopListView2 listView, int index)
    {
        LoopListViewItem2 item = listView.NewListViewItem(ItemNameList[1]);
        LevelLimitItem itemScript = item.GetComponent<LevelLimitItem>();
        if (item.IsInitHandlerCalled == false)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init();
        }
        int firstItemVal = 1;
        int itemCount = MaxLevel;
        int val = 0;
        if (index >= 0)
        {
            val = index % itemCount;
        }
        else
        {
            val = itemCount + ((index + 1) % itemCount) - 1;
        }
        val = val + firstItemVal;
        itemScript.Level = val;
        itemScript.SetData(val.ToString());
        return item;
    }

    private void OnMinLevelChanged(LoopListView2 listView, LoopListViewItem2 item)
    {
        int index = listView.GetIndexInShownItemList(item);
        if (index < 0)
            return;
        LevelLimitItem itemScript = item.GetComponent<LevelLimitItem>();
        m_LevelMin = itemScript.Level;
    }

    private void OnMaxLevelChanged(LoopListView2 listView, LoopListViewItem2 item)
    {
        int index = listView.GetIndexInShownItemList(item);
        if (index < 0)
            return;
        LevelLimitItem itemScript = item.GetComponent<LevelLimitItem>();
        m_LevelMax = itemScript.Level;
    }
}
