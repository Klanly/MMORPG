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

/// <summary> 成就 </summary>
public class AchievementTreeCtrl : TreeCtrlBase {

    private string[] m_Achievement1List = {"人物等级","章节进度","VIP等级","排行榜","好友数量" };
    //private string[][] m_Achievement2List = {
    //    new string[] { "人物等级达到10级","人物等级达到20级","人物等级达到30级","人物等级达到40级","人物等级达到50级","人物等级达到60级"},
    //    new string[] { "章节进度达到1", "章节进度达到2", "章节进度达到3", "章节进度达到4", "章节进度达到5" },
    //    new string[] { "VIP等级1", "VIP等级3", "VIP等级6", "VIP等级10", "VIP等级15" },
    //    new string[] { "排行榜200", "排行榜100", "排行榜50", "排行榜10", "排行榜1" },
    //    new string[] { "好友数量10", "好友数量20", "好友数量30", "好友数量40", "好友数量50" }
    //};
    private string[][] m_Achievement2List = {
        new string[] { "人物等级达到10级","人物等级达到20级","人物等级达到30级"},
        new string[] { "章节进度达到1", "章节进度达到2" },
        new string[] { "VIP等级1" },
        new string[] { "排行榜200", "排行榜100", "排行榜50", "排行榜10", "排行榜1" },
        new string[] { "好友数量10", "好友数量20", "好友数量30", "好友数量40", "好友数量50" }
    };
    private void Start()
    {
        List<GameObject> list = new List<GameObject>();
        list.Add(Resources.Load<GameObject>("UIPrefab/Tree/ItemPrefab1"));
        list.Add(Resources.Load<GameObject>("UIPrefab/Tree/ItemPrefab2"));
        Init(list,GetChildCountList());
    }

    private List<int> GetChildCountList()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < m_Achievement2List.Length; i++)
        {
            list.Add(m_Achievement2List[i].Length);
        }
        return list;
    }

    public override LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
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
            itemScript.mText.text = treeViewItemData.mName;
            //Debug.Log(index);
            itemScript.mText.text = m_Achievement1List[treeItemIndex];
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
            itemScript.BtnGet.onClick.AddListener(delegate 
            {
                Debug.Log("领取 "+itemScript.mNameText.text);
            });
            Vector3 v3 = itemScript.transform.localPosition;
            itemScript.transform.localPosition = new Vector3(v3.x + 50, v3.y, v3.z);
            itemScript.SetData(m_Achievement2List[treeItemIndex][childIndex]);
            return item;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ExpandAll();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CollapseAll();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            ExpandAll();
            MoveToIndex(1, 2);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            m_Achievement2List = 
                new string[][] {
                    new string[] { "人物等级达到10级", "人物等级达到20级" },
                    new string[] { "章节进度达到1", "章节进度达到2" },
                    new string[] { "VIP等级1" },
                    new string[] { "排行榜200", "排行榜100", "排行榜50", "排行榜10", "排行榜1" },
                    new string[] { "好友数量10", "好友数量20", "好友数量30", "好友数量40", "好友数量50" }
                };
            SetChildTreeItemCount(GetChildCountList());
        }
    }
}
