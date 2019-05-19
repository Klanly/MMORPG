using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountCtrl : SystemCtrlBase<AccountCtrl>,ISystemCtrl {

    private UILogOnWindow m_LogOnWindow;

    private UIRegisterWindow m_RegisterWindow;

    public AccountCtrl()
    {
        AddEventListener(ConstDefine.UILogOnWindow_LogOn, OnLogOnClick);
        AddEventListener(ConstDefine.UILogOnWindow_ToRegister, OnToRegisterClick);

        AddEventListener(ConstDefine.UIRegisterWindow_Register, OnRegisterClick);
        AddEventListener(ConstDefine.UIRegisterWindow_ToLogOn, OnToLogOnClick);


        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.AccountRegisterResponse, OnAccountRegisterResponse);
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.AccountLogOnResponse, OnAccountLogOnResponse);
    }

    private void OnLogOnClick(object[] param)
    {
        LogOn(m_LogOnWindow.IFAccount.text, m_LogOnWindow.IFPwd.text);
    }

    private void LogOn(string userName,string pwd)
    {
        if (m_LogOnWindow!=null)
        {
            if (string.IsNullOrEmpty(m_LogOnWindow.IFAccount.text))
            {
                TipsUtil.ShowWindowTips(LanguageUtil.GetStrById(1000101), LanguageUtil.GetStrById(1000109));
                return;
            }
            if (string.IsNullOrEmpty(m_LogOnWindow.IFPwd.text))
            {
                TipsUtil.ShowWindowTips(LanguageUtil.GetStrById(1000102), LanguageUtil.GetStrById(1000109));
                return;
            }
        }
        AccountLogOnRequestProto proto = new AccountLogOnRequestProto();
        proto.UserName = userName;
        proto.Pwd = pwd;
        proto.DeviceIdentifier = DeviceUtil.DeviceIdentifier;
        proto.DeviceModel = DeviceUtil.DeviceModel;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    /// <summary> 登录视图 去注册按钮点击 </summary>
    /// <param name="param"></param>
    private void OnToRegisterClick(object[] param)
    {
        m_LogOnWindow.CloseAndOpenNext(Window.Register);
    }
    private void OnRegisterClick(object[] param)
    {
        if (m_RegisterWindow!=null)
        {
            if (string.IsNullOrEmpty(m_RegisterWindow.IFAccount.text))
            {
                TipsUtil.ShowWindowTips(LanguageUtil.GetStrById(1000101), LanguageUtil.GetStrById(1000110));
                return;
            }
            if (string.IsNullOrEmpty(m_RegisterWindow.IFPwd.text))
            {
                TipsUtil.ShowWindowTips(LanguageUtil.GetStrById(1000102), LanguageUtil.GetStrById(1000110));
                return;
            }
            if (string.IsNullOrEmpty(m_RegisterWindow.IFAgainPwd.text))
            {
                TipsUtil.ShowWindowTips(LanguageUtil.GetStrById(1000103), LanguageUtil.GetStrById(1000110));
                return;
            }
            if (!string.Equals(m_RegisterWindow.IFPwd.text, m_RegisterWindow.IFAgainPwd.text))
            {
                TipsUtil.ShowWindowTips(LanguageUtil.GetStrById(1000104), LanguageUtil.GetStrById(1000110));
                return;
            }
        }
        AccountRegisterRequestProto proto = new AccountRegisterRequestProto();
        proto.Account = m_RegisterWindow.IFAccount.text;
        proto.Pwd = m_RegisterWindow.IFPwd.text;
        proto.Channel = "0";
        proto.DeviceIdentifier = DeviceUtil.DeviceIdentifier;
        proto.DeviceModel = DeviceUtil.DeviceModel;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    /// <summary> 注册视图 返回登录按钮点击 </summary>
    /// <param name="param"></param>
    private void OnToLogOnClick(object[] param)
    {
        m_RegisterWindow.CloseAndOpenNext(Window.LogOn);
        //OpenLogOnWindow();
    }
    /// <summary> 注册视图 返回登录按钮点击 </summary>
    public void OpenLogOnWindow()
    {
        m_LogOnWindow = WindowUtil.Instance.OpenWindow(Window.LogOn).GetComponent<UILogOnWindow>();
    }

    public void OpenRegisterWindow()
    {
        m_RegisterWindow = WindowUtil.Instance.OpenWindow(Window.Register).GetComponent<UIRegisterWindow>();
    }

    public void OpenWindow(string winName)
    {

        switch (winName)
        {
            case Window.LogOn:
                OpenLogOnWindow();
                break;
            case Window.Register:
                OpenRegisterWindow();
                break;
        }
    }

    private void OnAccountRegisterResponse(byte[] buffer)
    {
        AccountRegisterResponseProto proto = AccountRegisterResponseProto.GetProto(buffer);
        if (!proto.IsSuccess)
        {
            TipsUtil.ShowTextTips(1000105);
        }
        else
        {
            //注册成功
            //TipsUtil.ShowTextTips("用户编号 = " + proto.UserId);
            if (m_RegisterWindow != null)
            {
                Stat.Register(proto.UserId, m_RegisterWindow.IFAccount.text);
                PlayerPrefsUtil.SetAccountID(proto.UserId);
                PlayerPrefsUtil.SetAccountUserName(m_RegisterWindow.IFAccount.text);
                PlayerPrefsUtil.SetAccountPwd(m_RegisterWindow.IFPwd.text);
                m_RegisterWindow.CloseAndOpenNext(Window.GameServerEnter);
            }
            else
            {
                Stat.Register(proto.UserId, PlayerPrefsUtil.GetAccountUserName());
                WindowManager.Instance.OpenWindow(Window.GameServerEnter);
            }
        }
    }

    private void OnAccountLogOnResponse(byte[] buffer)
    {
        AccountLogOnResponseProto proto = AccountLogOnResponseProto.GetProto(buffer);
        if (!proto.IsSuccess)
        {
            TipsUtil.ShowTextTips(1000111);
        }
        else
        {
            //登录成功
            //TipsUtil.ShowTextTips("用户编号 = " + proto.UserId);
            if (m_LogOnWindow != null)
            {
                Stat.LogOn(proto.UserId, m_LogOnWindow.IFAccount.text);
                PlayerPrefsUtil.SetAccountID(proto.UserId);
                PlayerPrefsUtil.SetAccountUserName(m_LogOnWindow.IFAccount.text);
                PlayerPrefsUtil.SetAccountPwd(m_LogOnWindow.IFPwd.text);
                m_LogOnWindow.CloseAndOpenNext(Window.GameServerEnter);
            }
            else
            {
                Stat.Register(proto.UserId, PlayerPrefsUtil.GetAccountUserName());
                WindowManager.Instance.OpenWindow(Window.GameServerEnter);
            }
            GlobalInit.Instance.AccountId = proto.UserId;
            GlobalInit.Instance.AccountUserName = PlayerPrefsUtil.GetAccountUserName();
        }
    }

    /// <summary> 快速登录 </summary>
    public void QuickLogOn()
    {
        //PlayerPrefs.DeleteAll();
        //1.首先判断本地账号
        //2.如果本地没有账号,则进入注册界面
        //3.如果本地有账号,则自动登录,登录成功后 进入游戏界面(选区)
        if (!PlayerPrefs.HasKey(ConstDefine.LogOn_AccountID))
        {
            OpenRegisterWindow();
        }
        else
        {
            LogOn(PlayerPrefsUtil.GetAccountUserName(), PlayerPrefsUtil.GetAccountPwd());
        }
    }


    public override void Dispose()
    {
        base.Dispose();
        RemoveEventListener(ConstDefine.UILogOnWindow_LogOn, OnLogOnClick);
        RemoveEventListener(ConstDefine.UILogOnWindow_ToRegister, OnToRegisterClick);

        RemoveEventListener(ConstDefine.UIRegisterWindow_Register, OnRegisterClick);
        RemoveEventListener(ConstDefine.UIRegisterWindow_ToLogOn, OnToLogOnClick);


        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.AccountRegisterResponse, OnAccountRegisterResponse);
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.AccountLogOnResponse, OnAccountLogOnResponse);
    }
}
