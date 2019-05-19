/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-26 10:30:49 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-26 10:30:49 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SuperScrollView;

/// <summary> 游戏关卡列表控制类 </summary>
public class UIGameLevelListCtrl : GridViewVerticalCtrlBase {

    private List<TransferData> m_TransferDataList;
    private UIGameLevelItem m_LastItem;
    private UIGameLevelItem m_NowItem;
    private UIGameLevelView m_GameLevelView;

    private int m_GameLevelId = -1;
    private int m_Star2Time= -1;
    private int m_Star3Time = -1;
    public int GameLevelId
    {
        get { return m_GameLevelId; }
    }

    public int Star2Time
    {
        get { return m_Star2Time; }
    }
    public int Star3Time
    {
        get { return m_Star3Time; }
    }

    /// <summary> 初始化游戏关卡 </summary>
    /// <param name="list">游戏关卡数据列表</param>
    public void Init(List<TransferData> list)
    {
        TotalCount = list.Count;
        m_TransferDataList = list;
        if (m_GameLevelView != null)
        {
            return;
        }
        m_GameLevelView = GetComponent<UIGameLevelView>();
        Init(Resources.Load<GameObject>("UIPrefab/Item/GameLevel/UIGameLevelItem"));
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
            UIGameLevelItem itemScript = item.transform.GetChild(i).GetComponent<UIGameLevelItem>();
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
            m_TransferDataList[itemIndex].ActionOneIntCallBack = CallBack;
            m_TransferDataList[itemIndex].ObjArray = new object[] { itemScript};
            itemScript.SetData(m_TransferDataList[itemIndex]);
        }
        return item;
    }

    public void Select(int index)
    {
        CallBack(index);
    }

    private void CallBack(int itemIndex)
    {
        if (m_NowItem != null && m_NowItem.Index == itemIndex)
            return;
        m_GameLevelId = itemIndex+1;
        m_GameLevelView.SetData(m_TransferDataList[itemIndex]);
        m_GameLevelView.BtnChallengeClickCallBack = OnBtnChallengeClick;
        if (m_NowItem != null)
        {
            m_LastItem = m_NowItem;
            m_LastItem.IsSelect(false);
        }
        m_NowItem = m_TransferDataList[itemIndex].ObjArray[0] as UIGameLevelItem;
        m_NowItem.IsSelect(true);
        m_Star2Time = m_TransferDataList[itemIndex].GetValue<int>(ConstDefine.Star2Time);
        m_Star3Time = m_TransferDataList[itemIndex].GetValue<int>(ConstDefine.Star3Time);
    }

    private void OnBtnChallengeClick()
    {
        int worldMapId = ChapterLevelDBModel.Instance.GetList(GameLevelCtrl.Instance.ChapterId)[GameLevelId - 1].WorldMapId;
        GameLevelCtrl.Instance.OnGameLevelEnterRequest(worldMapId, 1);
        //SceneMgr.Instance.LoadToWorldMap(5);
    }
}
