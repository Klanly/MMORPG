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

public class TreeItemData
{
    public string mName;
    public string mIcon;
    List<GridItemData> mChildItemDataList = new List<GridItemData>();

    public int ChildCount
    {
        get { return mChildItemDataList.Count; }
    }

    public void AddChild(GridItemData data)
    {
        mChildItemDataList.Add(data);
    }
    public GridItemData GetChild(int index)
    {
        if (index < 0 || index >= mChildItemDataList.Count)
        {
            return null;
        }
        return mChildItemDataList[index];
    }
}

/// <summary>  </summary>
public class TreeViewData
{

    List<TreeItemData> mItemDataList = new List<TreeItemData>();

    public int mTreeViewItemCount = 20;
    public List<int> mTreeViewChildItemCountList = null;
    public int mTreeViewChildItemCount = 30;

    public void Init()
    {
        DoRefreshDataSource();
    }

    public TreeItemData GetItemDataByIndex(int index)
    {
        if (index < 0 || index >= mItemDataList.Count)
        {
            return null;
        }
        return mItemDataList[index];
    }

    public GridItemData GetItemChildDataByIndex(int itemIndex, int childIndex)
    {
        TreeItemData data = GetItemDataByIndex(itemIndex);
        if (data == null)
        {
            return null;
        }
        return data.GetChild(childIndex);
    }

    public int TreeViewItemCount
    {
        get
        {
            return mItemDataList.Count;
        }
    }

    public int TotalTreeViewItemAndChildCount
    {
        get
        {
            int count = mItemDataList.Count;
            int totalCount = 0;
            for (int i = 0; i < count; ++i)
            {
                totalCount = totalCount + mItemDataList[i].ChildCount;
            }
            return totalCount;
        }
    }


    void DoRefreshDataSource()
    {
        mItemDataList.Clear();
        for (int i = 0; i < mTreeViewItemCount; ++i)
        {
            TreeItemData tData = new TreeItemData();
            tData.mName = "Item" + i;
            mItemDataList.Add(tData);
            //int childCount = mTreeViewChildItemCount;
            int childCount = mTreeViewChildItemCountList[i];
            for (int j = 1; j <= childCount; ++j)
            {
                GridItemData childItemData = new GridItemData();
                //childItemData.mName = "Item" + i + ":Child" + j;
                //childItemData.mDesc = "Item Desc For " + childItemData.mName;
                //childItemData.mStarCount = Random.Range(0, 6);
                //childItemData.mFileSize = Random.Range(20, 999);
                tData.AddChild(childItemData);
            }
        }
    }
}

