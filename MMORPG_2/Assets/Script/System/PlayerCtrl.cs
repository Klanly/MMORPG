/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-05 01:10:14 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-05 01:10:14 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 玩家控制器 </summary>
public class PlayerCtrl : SystemCtrlBase<PlayerCtrl>,ISystemCtrl {

    /// <summary> 最后进入的世界地图Id </summary>
    public int LastInWorldMapId;

    private UIRoleInfoWindow m_RoleInfoWindow;
    private UIRoleEquipInfoView m_RoleEquipInfoView;
    private UIRoleInfoView m_RoleInfoView;

    private int m_LastInWorldMapId = 0;
    public TransferData PlayerTransferData
    {
        get
        {
            return GetPlayerTransferData();
        }
    }

    public PlayerCtrl()
    {
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleInfoResponse, OnRoleInfoResponse);
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleSkillDataResponse, OnRoleSkillDataResponse);
    }

    public void OpenWindow(string winName)
    {
        switch (winName)
        {
            case Window.RoleInfo:
                OpenRoleInfoWindow();
                break;
        }
    }

    /// <summary> 设置主城角色信息 </summary>
    public void SetMainCityRoleInfo()
    {
        UIMainCityRoleInfoView.Instance.SetUI(PlayerTransferData);
        GlobalInit.Instance.CurrPlayer.OnHPChange = OnHPChangeCallBack;
        GlobalInit.Instance.CurrPlayer.OnMPChange = OnMPChangeCallBack;
    }

    private void OnHPChangeCallBack(ValueChnageType type)
    {
        
        UIMainCityRoleInfoView.Instance.SetHP(GlobalInit.Instance.CurrPlayer.CurrRoleInfo.CurrHP, GlobalInit.Instance.CurrPlayer.CurrRoleInfo.MaxHP);
    }

    private void OnMPChangeCallBack(ValueChnageType type)
    {
        UIMainCityRoleInfoView.Instance.SetMP(GlobalInit.Instance.CurrPlayer.CurrRoleInfo.CurrMP, GlobalInit.Instance.CurrPlayer.CurrRoleInfo.MaxMP);
    }

    /// <summary> 打开角色信息界面 </summary>
    public void OpenRoleInfoWindow()
    {
        m_RoleInfoWindow = WindowUtil.Instance.OpenWindow(Window.RoleInfo).GetComponent<UIRoleInfoWindow>();
        GameObject equipInfoObj = ResourcesManager.Instance.LoadOther("RolePackage/PlayerEquipInfo");
        equipInfoObj.transform.SetParent(m_RoleInfoWindow.Container);
        equipInfoObj.transform.localScale = Vector3.one;
        equipInfoObj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        equipInfoObj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        m_RoleEquipInfoView = equipInfoObj.GetComponent<UIRoleEquipInfoView>();
        GameObject roleInfoobj = ResourcesManager.Instance.LoadOther("RolePackage/PlayerRoleInfo");
        roleInfoobj.transform.SetParent(m_RoleInfoWindow.Container);
        roleInfoobj.transform.localScale = Vector3.one;
        roleInfoobj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        roleInfoobj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        m_RoleInfoView = roleInfoobj.GetComponent<UIRoleInfoView>();
        m_RoleInfoView.SetData(PlayerTransferData);
    }


    /// <summary> 获取玩家数据 </summary  
    /// <returns></returns>
    private TransferData GetPlayerTransferData()
    {
        TransferData roleInfoData = new TransferData();
        RoleInfoMainPlayer playerInfo = GlobalInit.Instance.PlayerInfo;
        roleInfoData.SetValue(ConstDefine.RoleId, playerInfo.RoleId);
        roleInfoData.SetValue(ConstDefine.JobId, playerInfo.JobId);
        JobEntity jobEntity = JobDBModel.Instance.Get(playerInfo.JobId);
        if (jobEntity != null)
            roleInfoData.SetValue(ConstDefine.RoleIcon, jobEntity.HeadPic);
        roleInfoData.SetValue(ConstDefine.NickName, playerInfo.NickName);
        roleInfoData.SetValue(ConstDefine.PapalName, "");
        roleInfoData.SetValue(ConstDefine.Sex, playerInfo.Sex);
        roleInfoData.SetValue(ConstDefine.Level, playerInfo.Level);
        roleInfoData.SetValue(ConstDefine.VIPLevel, 99);
        roleInfoData.SetValue(ConstDefine.Money, playerInfo.Money);
        roleInfoData.SetValue(ConstDefine.Gold, playerInfo.Gold);
        roleInfoData.SetValue(ConstDefine.CurrHP, playerInfo.CurrHP);
        roleInfoData.SetValue(ConstDefine.MaxHP, playerInfo.MaxHP);
        roleInfoData.SetValue(ConstDefine.CurrMP, playerInfo.CurrMP);
        roleInfoData.SetValue(ConstDefine.CurrMP, playerInfo.CurrMP);
        roleInfoData.SetValue(ConstDefine.MaxMP, playerInfo.MaxMP);
        roleInfoData.SetValue(ConstDefine.CurrExp, playerInfo.Exp);
        roleInfoData.SetValue(ConstDefine.MaxExp, GetMaxExp());
        roleInfoData.SetValue(ConstDefine.Attack, playerInfo.Attack);
        roleInfoData.SetValue(ConstDefine.AttackAddition, playerInfo.AttackAddition);
        roleInfoData.SetValue(ConstDefine.FinalAttack, playerInfo.GetFinalAttack());
        roleInfoData.SetValue(ConstDefine.Defense, playerInfo.Defense);
        roleInfoData.SetValue(ConstDefine.DefenseAddition, playerInfo.DefenseAddition);
        roleInfoData.SetValue(ConstDefine.FinalDefense, playerInfo.GetFinalDefense());
        roleInfoData.SetValue(ConstDefine.Res, playerInfo.Res);
        roleInfoData.SetValue(ConstDefine.ResAddition, playerInfo.ResAddition);
        roleInfoData.SetValue(ConstDefine.FinalRes, playerInfo.GetFinalRes());
        roleInfoData.SetValue(ConstDefine.Hit, playerInfo.Hit);
        roleInfoData.SetValue(ConstDefine.HitAddition, playerInfo.HitAddition);
        roleInfoData.SetValue(ConstDefine.FinalHit, playerInfo.GetFinalHit());
        roleInfoData.SetValue(ConstDefine.Cri, playerInfo.Cri);
        roleInfoData.SetValue(ConstDefine.CriAddition, playerInfo.CriAddition);
        roleInfoData.SetValue(ConstDefine.FinalCri, playerInfo.GetFinalCri());
        roleInfoData.SetValue(ConstDefine.Dodge, playerInfo.Dodge);
        roleInfoData.SetValue(ConstDefine.DodgeAddition, playerInfo.DodgeAddition);
        roleInfoData.SetValue(ConstDefine.FinalDodge, playerInfo.GetFinalDodge());
        roleInfoData.SetValue(ConstDefine.Fighting, playerInfo.Fighting);
        roleInfoData.SetValue(ConstDefine.FightingAddition, playerInfo.FightingAddition);
        roleInfoData.SetValue(ConstDefine.FinalFighting, playerInfo.Fighting + playerInfo.FightingAddition);
        return roleInfoData;
    }

    public int GetMaxExp()
    {
        return GlobalInit.Instance.CurrPlayer.CurrRoleInfo.Level * 1000;
    }

    /// <summary> 服务器返回角色信息 </summary>
    /// <param name="buffer"></param>
    private void OnRoleInfoResponse(byte[] buffer)
    {
        RoleInfoResponseProto proto = RoleInfoResponseProto.GetProto(buffer);
        if (proto.IsSuccess)
        {
            GlobalInit.Instance.PlayerInfo = new RoleInfoMainPlayer(proto);
            Debuger.LogError("GlobalInit.Instance.PlayerInfo.level = " + GlobalInit.Instance.PlayerInfo.Level);
            m_LastInWorldMapId = proto.LastInWorldMapId;
        }
        else
        {
            TipsUtil.ShowTextTips(proto.MsgCode);
        }
    }

    private void OnRoleSkillDataResponse(byte[] buffer)
    {
        RoleSkillDataResponseProto proto = RoleSkillDataResponseProto.GetProto(buffer);
        GlobalInit.Instance.PlayerInfo.LoadSkill(proto);
        PlayerCtrl.Instance.LastInWorldMapId = m_LastInWorldMapId;
        SceneMgr.Instance.LoadToWorldMap(m_LastInWorldMapId);
    }

    public override void Dispose()
    {
        base.Dispose();
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleInfoResponse, OnRoleInfoResponse);
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleSkillDataResponse, OnRoleSkillDataResponse);
    }
}
