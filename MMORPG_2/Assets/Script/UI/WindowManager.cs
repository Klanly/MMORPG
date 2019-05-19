using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Singleton<WindowManager>
{

    private Dictionary<string, ISystemCtrl> m_SystemCtrlDic = new Dictionary<string, ISystemCtrl>();

    public WindowManager()
    {
        m_SystemCtrlDic.Add(Window.LogOn, AccountCtrl.Instance);//登录
        m_SystemCtrlDic.Add(Window.Register, AccountCtrl.Instance);//注册
        m_SystemCtrlDic.Add(Window.GameServerEnter, GameServerCtrl.Instance);//进入选择服务器
        m_SystemCtrlDic.Add(Window.RoleInfo, PlayerCtrl.Instance);//角色信息(也包括背包等相关界面)
        m_SystemCtrlDic.Add(Window.GameLevelMap, GameLevelCtrl.Instance);//剧情关卡地图
        m_SystemCtrlDic.Add(Window.GameLevelDetail, GameLevelCtrl.Instance);//剧情关卡详情
        m_SystemCtrlDic.Add(Window.FightResult, FightResultCtrl.Instance);//战斗结算界面
    }
    public void OpenWindow(string winName)
    {
        m_SystemCtrlDic[winName].OpenWindow(winName);
    }

    public void CloseWindow(WindowType type)
    {

    }
}
