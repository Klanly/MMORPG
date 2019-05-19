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

/// <summary>滑动列表Item数据类 </summary>
public class GridItemData
{
    public int Id;
    public bool Checked;
    public bool IsExpand;
}

/// <summary>  </summary>
public class ScrollViewData  {

    List<GridItemData> m_ItemList = new List<GridItemData>();
    Action m_OnRefreshFinished = null;
    Action m_OnLoadMoreFinished = null;
    int m_LoadMoreCount = 20;
    float m_DataLoadLeftTime = 0;
    bool m_IsWaittingRefreshData = false;
    bool m_IsWaitLoadingMoreData = false;

    public int TotalDataCount = 10000;

    public void Init()
    {
        DoRefreshDataSource();
    }
    public GridItemData GetItemDataByIndex(int index)
    {
        if (index < 0 || index >= m_ItemList.Count)
        {
            return null;
        }
        return m_ItemList[index];
    }
    public GridItemData GetItemDataById(int itemId)
    {
        int count = m_ItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            if (m_ItemList[i].Id == itemId)
            {
                return m_ItemList[i];
            }
        }
        return null;
    }
    public int TotalItemCount
    {
        get
        {
            return m_ItemList.Count;
        }
    }

    public void RequestRefreshDataList(System.Action onReflushFinished)
    {
        m_DataLoadLeftTime = 1;
        m_OnRefreshFinished = onReflushFinished;
        m_IsWaittingRefreshData = true;
    }

    public void RequestLoadMoreDataList(int loadCount, System.Action onLoadMoreFinished)
    {
        m_LoadMoreCount = loadCount;
        m_DataLoadLeftTime = 1;
        m_OnLoadMoreFinished = onLoadMoreFinished;
        m_IsWaitLoadingMoreData = true;
    }

    public void Update()
    {
        if (m_IsWaittingRefreshData)
        {
            m_DataLoadLeftTime -= Time.deltaTime;
            if (m_DataLoadLeftTime <= 0)
            {
                m_IsWaittingRefreshData = false;
                DoRefreshDataSource();
                if (m_OnRefreshFinished != null)
                {
                    m_OnRefreshFinished();
                }
            }
        }
        else if (m_IsWaitLoadingMoreData)
        {
            m_DataLoadLeftTime -= Time.deltaTime;
            if (m_DataLoadLeftTime <= 0)
            {
                m_IsWaitLoadingMoreData = false;
                DoLoadMoreDataSource();
                if (m_OnLoadMoreFinished != null)
                {
                    m_OnLoadMoreFinished();
                }
            }
        }

    }

    public void SetDataTotalCount(int count)
    {
        TotalDataCount = count;
        DoRefreshDataSource();
    }

    void DoRefreshDataSource()
    {
        m_ItemList.Clear();
        for (int i = 0; i < TotalDataCount; ++i)
        {
            GridItemData data = new GridItemData();
            data.Id = i;
            data.Checked = false;
            data.IsExpand = false;
            m_ItemList.Add(data);
        }
    }

    void DoLoadMoreDataSource()
    {
        int count = m_ItemList.Count;
        for (int k = 0; k < m_LoadMoreCount; ++k)
        {
            int i = k + count;
            GridItemData data = new GridItemData();
            data.Id = i;
            data.Checked = false;
            data.IsExpand = false;
            m_ItemList.Add(data);
        }
    }

    public void CheckAllItem()
    {
        int count = m_ItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            m_ItemList[i].Checked = true;
        }
    }

    public void UnCheckAllItem()
    {
        int count = m_ItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            m_ItemList[i].Checked = false;
        }
    }

    public bool DeleteAllCheckedItem()
    {
        int oldCount = m_ItemList.Count;
        m_ItemList.RemoveAll(it => it.Checked);
        return (oldCount != m_ItemList.Count);
    }

}
