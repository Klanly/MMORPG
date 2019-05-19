/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-13 23:24:54 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-13 23:24:54 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>  </summary>
public class GameLevelCtrl : SystemCtrlBase<GameLevelCtrl>,ISystemCtrl {

    private UIGameLevelMapWindow m_GameLevelMapWin;
    private UIGameChapterView m_GameChapterView;
    private UIGameLevelView m_GameLevelView;
    private UIGameLevelListCtrl GameLevelListCtrl;

    public int ChapterId { get { return m_GameLevelView.Chapter; } }
    public int GameLevelId { get { return GameLevelListCtrl.GameLevelId; } }
    public int Star2Time { get { return GameLevelListCtrl.Star2Time; } }
    public int Star3Time { get { return GameLevelListCtrl.Star3Time; } }
    public int Result { get; set; }

    //public int Star2Time { get { return ChapterLevelDBModel.Instance.Get(} }
    public float UseTime { get; set; }
    /// <summary> 掉落经验 </summary>
    public int DropExp { get; set; }
    /// <summary> 掉落金币 </summary>
    public int DropCoin { get; set; }
    

    /// <summary> 要进入的世界场景ID </summary>
    private int m_WordMapId = 0;

    public int CurrStarCount
    {
        get
        {
            int star = 0;
            if (UseTime <= Star3Time)
                star = 3;
            else if(UseTime <= Star2Time)
                star = 2;
            else
                star = 1;
            return star;
        }
    }

    public GameLevelCtrl()
    {
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.GameLevelEnterResponse, OnGameEnterResponse);
    }

    public void OpenWindow(string winName)
    {
        switch (winName)
        {
            case Window.GameLevelMap:
                OpenGamelevelMapWindow();
                break;
        }
    }

    private void OpenGamelevelMapWindow()
    {
        m_GameLevelMapWin = WindowUtil.Instance.OpenWindow(Window.GameLevelMap).GetComponent<UIGameLevelMapWindow>();
        m_GameLevelMapWin.BtnCallBack = OnViewBtnCallBack;
        GameObject gameLevelMap = ResourcesManager.Instance.LoadOther("GameLevel/UIGameChapterView");
        m_GameChapterView = gameLevelMap.GetComponent<UIGameChapterView>();
        gameLevelMap.SetParent(m_GameLevelMapWin.Container);
        gameLevelMap.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        gameLevelMap.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        List<ChapterEntity> list = ChapterDBModel.Instance.GetList();
        List<TransferData> transferDataList = new List<TransferData>();
        for (int i = 0; i < list.Count; i++)
        {
            TransferData data = new TransferData();
            data.SetValue(ConstDefine.Index, i + 1);
            data.SetValue(ConstDefine.FolderName, list[i].FolderName);
            data.SetValue(ConstDefine.IsOpen, i<3);
            data.SetValue(ConstDefine.IconName, list[i].IconName);
            data.SetValue(ConstDefine.Name, list[i].Name);
            data.SetValue(ConstDefine.CurrStar, 0);
            data.SetValue(ConstDefine.MaxStar, list[i].LevelCount * 3);
            data.SetValue(ConstDefine.CurrProgeress, 0);
            data.SetValue(ConstDefine.MaxProgeress, list[i].LevelCount);
            data.ActionOneIntCallBack = CallBack;
            transferDataList.Add(data);
        }
        gameLevelMap.GetComponent<UIGameChapterListCtrl>().TotalCount = transferDataList.Count;
        gameLevelMap.GetComponent<UIGameChapterListCtrl>().Init(transferDataList);
    }

    private void OnViewBtnCallBack(GameObject go)
    {
        switch (go.name)
        {
            case "Btn_Close":
                if (m_GameLevelView != null && m_GameLevelView.gameObject.activeSelf)
                {
                    m_GameLevelView.gameObject.SetActive(false);
                    m_GameChapterView.gameObject.SetActive(true);
                }
                else
                    m_GameLevelMapWin.Close();
                break;
        }
    }

    public void CallBack(int index)
    {
        m_GameChapterView.gameObject.SetActive(false);
        if (m_GameLevelView == null)
        {
            GameObject gameLevelMap = ResourcesManager.Instance.LoadOther("GameLevel/UIGameLevelView");
            m_GameLevelView = gameLevelMap.GetComponent<UIGameLevelView>();
            m_GameLevelView.Init();
            gameLevelMap.SetParent(m_GameLevelMapWin.Container);
            gameLevelMap.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            gameLevelMap.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        }
        m_GameLevelView.gameObject.SetActive(true);
        m_GameLevelView.SetChapterName(index);
        List<ChapterLevelEntity> list = ChapterLevelDBModel.Instance.GetList(index);
        GameLevelListCtrl = m_GameLevelView.GetComponent<UIGameLevelListCtrl>();
        List<TransferData> transferDataList = new List<TransferData>();
        for (int i = 0; i < list.Count; i++)
        {
            TransferData data = new TransferData();
            data.SetValue(ConstDefine.Index, i);
            data.SetValue(ConstDefine.FolderName, list[i].FolderName);
            data.SetValue(ConstDefine.IsBoss, list[i].IsBoos==1);
            data.SetValue(ConstDefine.IconName, list[i].IconName);
            data.SetValue(ConstDefine.Name, list[i].Name);
            data.SetValue(ConstDefine.RecommendFight, list[i].RecommendFight);
            data.SetValue(ConstDefine.NeedVitality, list[i].NeedVitality);
            data.SetValue(ConstDefine.FirstThreeStar, list[i].FirstThreeStar);
            data.SetValue(ConstDefine.Star2Time, list[i].Star2);
            data.SetValue(ConstDefine.Star3Time, list[i].Star3);
            data.SetValue(ConstDefine.Desc, list[i].Desc);
            data.SetValue(ConstDefine.CurrStar, 0);
            data.SetValue(ConstDefine.MaxStar, 3);
            data.SetValue(ConstDefine.RowStarCount, 3);
            transferDataList.Add(data);
        }
        GameLevelListCtrl.Init(transferDataList);
        GameLevelListCtrl.SetListItemTotalCount(list.Count);
        m_GameLevelView.SetData(transferDataList[0]);
        GameLevelListCtrl.MoveToIndex(0);
        GameLevelListCtrl.Select(0);
    }


    private void OnGameEnterResponse(byte[] buffer)
    {
        GameLevelEnterResponseProto proto = GameLevelEnterResponseProto.GetProto(buffer);
        Debuger.LogError("proto.Issuccess = "+ proto.IsSuccess);
        if (proto.IsSuccess)
            SceneMgr.Instance.LoadToWorldMap(m_WordMapId);
        else
            TipsUtil.ShowTextTips(string.Format(StringUtil.GetStringById(proto.MsgCode), m_WordMapId));


    }

    public void OnGameLevelEnterRequest(int worldMapId,byte grade)
    {
        GameLevelEnterRequestProto proto = new GameLevelEnterRequestProto();
        proto.GameChapterId = ChapterId;
        proto.GameLevelId = GameLevelId;
        proto.Grade = grade;
        m_WordMapId = worldMapId;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    public override void Dispose()
    {
        base.Dispose();
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.GameLevelEnterResponse, OnGameEnterResponse);
    }
}
