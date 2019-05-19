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

/// <summary> 树状滑动列表基类 </summary>
public class TreeCtrlBase : MonoBehaviour {

    public LoopListView2 LoopListView;
    public TreeViewData TreeData;
    /// <summary> 一级列表x轴偏移 </summary>
    public int SpacingX = 0;
    /// <summary> 一级列表x轴偏移 </summary>
    public int SpacingY = 10;
    /// <summary> 一级列表总个数 </summary>
    public int TotalCount = 5;
    public ListItemArrangeType ArrangeType = ListItemArrangeType.TopToBottom;
    private Transform m_Content;
    [HideInInspector]
    public List<string> ItemNameList;
    public TreeViewItemCountMgr TreeItemCountMgr = new TreeViewItemCountMgr();

    /// <summary> 初始化(子类调用) </summary>
    /// <param name="objList">树item列表(目前最高二级)</param>
    /// <param name="TreeViewChildItemCountList">一级树列表的每个二级树个数列表 长度对应objList长度</param>
    /// <param name="childPosXOffset">二级树x轴偏移</param>
    /// <param name="childPosYOffset">二级树y轴偏移</param>
    public void Init(List<GameObject> objList, List<int> TreeViewChildItemCountList, int childPosXOffset = 50, int childPosYOffset = 10)
    {
        LoopListView.ArrangeType = ArrangeType;
        m_Content = LoopListView.transform.Find("Viewport/Content");
        ItemNameList = new List<string>();
        List<ItemPrefabConfData> list = new List<ItemPrefabConfData>();
        for (int i = 0; i < objList.Count; i++)
        {
            GameObject insObj = Instantiate(objList[i]);
            insObj.name = "Item_" + (i + 1);
            ItemNameList.Add(insObj.name);
            RectTransform rt = insObj.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 1f);
            rt.anchorMax = new Vector2(1, 1f);
            rt.pivot = new Vector2(0, 1);
            insObj.transform.SetParent(m_Content);
            insObj.transform.localScale = Vector2.one;
            ItemPrefabConfData itemData = new ItemPrefabConfData();
            itemData.mItemPrefab = insObj;
            itemData.mPadding = i > 0 ? childPosYOffset : SpacingY;
            itemData.mInitCreateCount = 0;
            itemData.mStartPosOffset = i > 0 ? childPosXOffset : SpacingX;
            list.Add(itemData);
        }
        LoopListView.mItemPrefabDataList = list;
        TreeData = new TreeViewData();
        TreeData.mTreeViewItemCount = TotalCount;
        TreeData.mTreeViewChildItemCountList = TreeViewChildItemCountList;
        TreeData.Init();
        for (int i = 0; i < TreeData.mTreeViewItemCount; i++)
        {
            int childCount = TreeData.GetItemDataByIndex(i).ChildCount;
            //int childCount = TreeViewChildItemCountList[i];
            TreeItemCountMgr.AddTreeItem(childCount, true);
        }
        LoopListView.InitListView(TreeItemCountMgr.GetTotalItemAndChildCount(), OnGetItemByIndex);
    }

    /// <summary> 重新设置每个子树的个数(列表更改后调用) </summary>
    /// <param name="TreeViewChildItemCountList"></param>
    public void SetChildTreeItemCount(List<int> TreeViewChildItemCountList)
    {
        TreeData = new TreeViewData();
        TreeData.mTreeViewItemCount = TotalCount;
        TreeData.mTreeViewChildItemCountList = TreeViewChildItemCountList;
        TreeData.Init();
        TreeItemCountMgr.Clear();
        for (int i = 0; i < TreeData.mTreeViewItemCount; i++)
        {
            int childCount = TreeData.GetItemDataByIndex(i).ChildCount;
            //int childCount = TreeViewChildItemCountList[i];
            TreeItemCountMgr.AddTreeItem(childCount, true);
        }
        LoopListView.RefreshAllShownItem();
    }

    /// <summary> 根据索引获取item 子类必须重写该类 </summary>
    /// <param name="listView"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public virtual LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        return null;
    }
    private LoopListViewItem2 OnGetItemByIndex1(LoopListView2 listView, int index)
    {
        if (index < 0)
        {
            return null;
        }

        /*to check the index'th item is a TreeItem or a TreeChildItem.for example,

         0  TreeItem0
         1      TreeChildItem0_0
         2      TreeChildItem0_1
         3      TreeChildItem0_2
         4      TreeChildItem0_3
         5  TreeItem1
         6      TreeChildItem1_0
         7      TreeChildItem1_1
         8      TreeChildItem1_2
         9  TreeItem2
         10     TreeChildItem2_0
         11     TreeChildItem2_1
         12     TreeChildItem2_2

         the first column value is the param 'index', for example, if index is 1,
         then we should return TreeChildItem0_0 to SuperScrollView, and if index is 5,
         then we should return TreeItem1 to SuperScrollView
        */

        TreeViewItemCountData countData = TreeItemCountMgr.QueryTreeItemByTotalIndex(index);
        if (countData == null)
        {
            return null;
        }
        int treeItemIndex = countData.mTreeItemIndex;
        TreeItemData treeViewItemData = TreeData.GetItemDataByIndex(treeItemIndex);
        if (countData.IsChild(index) == false)// if is a TreeItem
        {
            //get a new TreeItem
            LoopListViewItem2 item = listView.NewListViewItem(ItemNameList[0]);
            TreeItem1 itemScript = item.GetComponent<TreeItem1>();
            if (item.IsInitHandlerCalled == false)
            {
                item.IsInitHandlerCalled = true;
                itemScript.Init();
                itemScript.SetClickCallBack(this.OnExpandClicked);
            }
            //update the TreeItem's content
            itemScript.mText.text = treeViewItemData.mName;
            itemScript.SetItemData(treeItemIndex, countData.mIsExpand);
            return item;
        }
        else
        {
            int childIndex = countData.GetChildIndex(index);
            GridItemData itemData = treeViewItemData.GetChild(childIndex);
            if (itemData == null)
            {
                return null;
            }
            LoopListViewItem2 item = listView.NewListViewItem(ItemNameList[1]);
            TreeItem2 itemScript = item.GetComponent<TreeItem2>();
            if (item.IsInitHandlerCalled == false)
            {
                item.IsInitHandlerCalled = true;
                itemScript.Init();
            }
            return item;
        }
    }
    public void OnExpandClicked(int index)
    {
        TreeItemCountMgr.ToggleItemExpand(index);
        LoopListView.SetListItemCount(TreeItemCountMgr.GetTotalItemAndChildCount(), false);
        LoopListView.RefreshAllShownItem();
    }

    /// <summary> 跳转到指定索引 </summary>
    /// <param name="itemIndex">一级树索引(0开始)</param>
    /// <param name="childIndex">二级树索引(0开始)</param>
    public void MoveToIndex(int itemIndex,int childIndex)
    {
        int finalIndex = 0;
        if (childIndex < 0)
        {
            childIndex = 0;
        }
        TreeViewItemCountData itemCountData = TreeItemCountMgr.GetTreeItem(itemIndex);
        if (itemCountData == null)
        {
            return;
        }
        int childCount = itemCountData.mChildCount;
        if (itemCountData.mIsExpand == false || childCount == 0 || childIndex == 0)
        {
            finalIndex = itemCountData.mBeginIndex;
        }
        else
        {
            if (childIndex > childCount)
            {
                childIndex = childCount;
            }
            if (childIndex < 1)
            {
                childIndex = 1;
            }
            finalIndex = itemCountData.mBeginIndex + childIndex;
        }
        LoopListView.MovePanelToItemIndex(finalIndex, 0);
    }

    /// <summary> 全部展开 </summary>
    public void ExpandAll()
    {
        int count = TreeItemCountMgr.TreeViewItemCount;
        for (int i = 0; i < count; ++i)
        {
            TreeItemCountMgr.SetItemExpand(i, true);
        }
        LoopListView.SetListItemCount(TreeItemCountMgr.GetTotalItemAndChildCount(), false);
        LoopListView.RefreshAllShownItem();
    }

    /// <summary> 全部折叠 </summary>
    public void CollapseAll()
    {
        int count = TreeItemCountMgr.TreeViewItemCount;
        for (int i = 0; i < count; ++i)
        {
            TreeItemCountMgr.SetItemExpand(i, false);
        }
        LoopListView.SetListItemCount(TreeItemCountMgr.GetTotalItemAndChildCount(), false);
        LoopListView.RefreshAllShownItem();
    }
}
