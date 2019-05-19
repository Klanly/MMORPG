using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameServerCtrl : SystemCtrlBase<GameServerCtrl>, ISystemCtrl
{

    private UIGameServerEnterWindow m_GameServerEnterWin;
    private bool m_IsShowSelectWin = false;
    private GameObject m_LeftServerItem;
    private GameObject m_RightServerItem;
    private List<LeftGameServerItem> m_LeftItemList;
    private List<RightGameServerItem> m_RightItemList;
    private int m_LeftLastSelect = -1;
    private int m_LeftNowSelect = -1;
    public GameServerCtrl()
    {
        m_LeftItemList = new List<LeftGameServerItem>();
        m_RightItemList = new List<RightGameServerItem>();
        AddEventListener(ConstDefine.UIGameServerEnterWindow_BtnChange, OnBtnChangeClick);
        AddEventListener(ConstDefine.UIGameServerEnterWindow_BtnEnter, OnBtnEnterClick);


        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.GameServerPageResponse, OnGameServerPageResponse);
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.GameServerOnePageResponse, OnGameServerOnePageResponse);
    }

    private void OnGameServerPageRequest()
    {
        GameServerPageRequestProto proto = new GameServerPageRequestProto();
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }
    private void OnGameServerOnePageRequest(int pageIndex)
    {
        GameServerOnePageRequestProto proto = new GameServerOnePageRequestProto();
        proto.PageIndex = pageIndex;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    /// <summary> 服务器组协议回调 </summary>
    /// <param name="buffer"></param>
    private void OnGameServerPageResponse(byte[] buffer)
    {
        GameServerPageResponseProto proto = GameServerPageResponseProto.GetProto(buffer);
        SetLeftData(proto.ServerPageItemList);
        if (PlayerPrefsUtil.GetServerEnterID()<=0)
        {
            OnGameServerOnePageRequest(m_LeftItemList.Count);
        }
    }
    /// <summary> 服务器组数据 </summary>
    /// <param name="list"></param>
    private void SetLeftData(List<GameServerPageResponseProto.GameServerPageItem> list)
    {
        int needLoadCount = list.Count - m_GameServerEnterWin.LeftGrid.childCount;
        if (needLoadCount > 0)
        {
            //需要加载item
            if (m_LeftServerItem == null)
                m_LeftServerItem = ResourcesManager.Instance.LoadItem("ServerItem/LeftServerItem");
            for (int i = 0; i < needLoadCount; i++)
            {
                LeftGameServerItem leftItem = GameObject.Instantiate(m_LeftServerItem, m_GameServerEnterWin.LeftGrid).GetComponent<LeftGameServerItem>();
                m_LeftItemList.Add(leftItem);
            }
        }
        else
        {
            //需要隐藏Item
            for (int i = 0; i < -needLoadCount; i++)
            {
                m_LeftItemList[m_LeftItemList.Count - i - 1].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < list.Count; i++)
        {
            m_LeftItemList[i].gameObject.SetActive(true);
            m_LeftItemList[i].SetData(list[i].PageIndex, list[i].PageServerName, OnLeftCallBack);
            m_LeftItemList[i].IsSelect(false);
        }
        SetLeftSelect(0);
    }

    /// <summary> 选区左侧Item点击回调 </summary>
    /// <param name="pageIndex"></param>
    private void OnLeftCallBack(int pageIndex)
    {
        if (pageIndex==m_LeftNowSelect)
            return;
        OnGameServerOnePageRequest(pageIndex);
        SetLeftSelect(m_LeftItemList.Count - pageIndex);
    }
    private void SetLeftSelect(int index)
    {
        m_LeftLastSelect = m_LeftNowSelect;
        m_LeftNowSelect = index;
        if (m_LeftLastSelect!=-1)
        {
            m_LeftItemList[m_LeftLastSelect].IsSelect(false);
        }
        m_LeftItemList[m_LeftNowSelect].IsSelect(true);
    }
    /// <summary> 单组服务器数据协议回调 </summary>
    /// <param name="buffer"></param>
    private void OnGameServerOnePageResponse(byte[] buffer)
    {
        GameServerOnePageResponseProto proto = GameServerOnePageResponseProto.GetProto(buffer);
        proto.ServerOnePageItemList.Reverse();
        SetRightData(proto.ServerOnePageItemList);
    }
    /// <summary> 更新选区右侧Item </summary>
    /// <param name="list"></param>
    private void SetRightData(List<GameServerOnePageResponseProto.GameServerOnePageItem> list)
    {
        int needLoadCount = list.Count - m_GameServerEnterWin.RightGrid.childCount;
        if (needLoadCount > 0)
        {
            //需要加载item
            if (m_RightServerItem == null)
                m_RightServerItem = ResourcesManager.Instance.LoadItem("ServerItem/RightServerItem");
            for (int i = 0; i < needLoadCount; i++)
            {
                RightGameServerItem rightItem = GameObject.Instantiate(m_RightServerItem, m_GameServerEnterWin.RightGrid).GetComponent<RightGameServerItem>();
                m_RightItemList.Add(rightItem);
            }
        }
        else
        {
            //需要隐藏Item
            for (int i = 0; i < -needLoadCount; i++)
            {
                m_RightItemList[m_RightItemList.Count - i - 1].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < list.Count; i++)
        {
            m_RightItemList[i].gameObject.SetActive(true);
            m_RightItemList[i].SetData(i,list[i] ,OnRightCallBack);
            //m_RightItemList[i].IsSelect(i == 0);
        }
        if (PlayerPrefsUtil.GetServerEnterID() <= 0)
        {
            UpdateWin(m_RightItemList[0]);
        }
    }
    /// <summary> 单服点击回调 </summary>
    /// <param name="index"></param>
    private void OnRightCallBack(int index)
    {
        RightGameServerItem item = m_RightItemList[index];
        if (item.ServerState==0||item.ServerState>3)
        {
            TipsUtil.ShowTextTips(StringUtil.GetStringById(1000205));
        }
        else
        {
            UpdateWin(item);
            m_GameServerEnterWin.ServerSelectWin.DOPlayBackwards();
            //m_GameServerEnterWin.NowServerText.text = item.ServerName;
            //m_GameServerEnterWin.Status.sprite = AssetBundleManager.Instance.LoadSprite("Login", "YanChi0"+item.ServerState);
            //m_GameServerEnterWin.ChangeText.text = StringUtil.RichString(1000202);
            //m_IsShowSelectWin = false;
            //m_GameServerEnterWin.Logo.SetActive(true);
        }
    }
    /// <summary> 选择区服 </summary>
    /// <param name="p"></param>
    private void OnBtnChangeClick(object[] p)
    {
        if (!m_IsShowSelectWin)
        {
            m_GameServerEnterWin.ServerSelectWin.DOPlayForward();
            OnGameServerPageRequest();
            m_IsShowSelectWin = true;
            m_GameServerEnterWin.Logo.SetActive(false);
            m_GameServerEnterWin.ChangeText.text = StringUtil.GetStringById(1000206);
            
        }
        else
        {
            m_GameServerEnterWin.ServerSelectWin.DOPlayBackwards();
            m_IsShowSelectWin = false;
            m_GameServerEnterWin.Logo.SetActive(true);
            m_GameServerEnterWin.ChangeText.text = StringUtil.GetStringById(1000202);
        }
    }

    private void UpdateWin(RightGameServerItem item)
    {
        m_GameServerEnterWin.NowServerText.text = item.ServerName;
        //m_GameServerEnterWin.Status.sprite = AssetBundleManager.Instance.LoadSprite("Login", "YanChi0" + item.ServerState);
        m_GameServerEnterWin.Status.SetSprite("Login", "YanChi0" + item.ServerState);
        m_GameServerEnterWin.NowSelectServerState.color = ColorUtil.GetServerColor(item.ServerState);
        m_GameServerEnterWin.NowSelectServerName.text = item.ServerName;
        m_GameServerEnterWin.ChangeText.text = StringUtil.GetStringById(1000202);
        m_IsShowSelectWin = false;
        m_GameServerEnterWin.Logo.SetActive(true);
        PlayerPrefsUtil.SetServerEnterID(item.ServerId);
        PlayerPrefsUtil.SetServerEnterName(item.ServerName);
        PlayerPrefsUtil.SetServerEnterIp(item.Ip);
        PlayerPrefsUtil.SetServerEnterPort(item.Port);
    }

    /// <summary> 进入游戏 </summary>
    /// <param name="p"></param>
    private void OnBtnEnterClick(object[] p)
    {
        SceneMgr.Instance.LoadToSelectRole();
        //SceneMgr.Instance.LoadToCity();

    }
    /// <summary> 加载完成回调 </summary>
    /// <param name="obj"></param>
    private void OnLoadFinishCallBack(UnityEngine.Object obj)
    {
        GameObject.Instantiate((GameObject)obj);
    }

    public void OpenWindow(string winName)
    {
        switch (winName)
        {
            case Window.GameServerEnter:
                OpenGameServerEnter();
                break;
        }

    }

    public void OpenGameServerEnter()
    {
        m_GameServerEnterWin = WindowUtil.Instance.OpenWindow(Window.GameServerEnter).GetComponent<UIGameServerEnterWindow>();
        if (PlayerPrefsUtil.GetServerEnterID()<=0)
        {
            //如果本地没有存储选中区服(第一次进入选区界面)
            OnGameServerPageRequest();
        }
        else
        {
            m_GameServerEnterWin.NowServerText.text = PlayerPrefsUtil.GetServerEnterName();
            //m_GameServerEnterWin.Status.sprite = AssetBundleManager.Instance.LoadSprite("Login", "YanChi03");
            m_GameServerEnterWin.Status.SetSprite("Login", "YanChi03");
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        RemoveEventListener(ConstDefine.UIGameServerEnterWindow_BtnChange, OnBtnChangeClick);
        RemoveEventListener(ConstDefine.UIGameServerEnterWindow_BtnEnter, OnBtnEnterClick);
    }
}
