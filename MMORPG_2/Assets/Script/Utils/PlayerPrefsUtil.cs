using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsUtil  {

    #region ------ 用户账号相关 ------

    /// <summary> 设置本地持久化用户ID </summary>
    /// <param name="value">值</param>
    public static void SetAccountID(int value)
    {
        PlayerPrefs.SetInt(ConstDefine.LogOn_AccountID, value);
    }
    /// <summary> 获取本地持久化用户ID </summary>
    public static int GetAccountID()
    {
        return PlayerPrefs.GetInt(ConstDefine.LogOn_AccountID, -1);
    }
    /// <summary> 设置本地持久化用户名 </summary>
    public static void SetAccountUserName(string value)
    {
        PlayerPrefs.SetString(ConstDefine.LogOn_AccountUserName, value);
    }
    /// <summary> 获取本地持久化用户名 </summary>
    public static string GetAccountUserName()
    {
        return PlayerPrefs.GetString(ConstDefine.LogOn_AccountUserName, null);
    }
    /// <summary> 设置本地持久化用户密码 </summary>
    public static void SetAccountPwd(string value)
    {
        PlayerPrefs.SetString(ConstDefine.LogOn_AccountPwd, value);
    }
    /// <summary> 获取本地持久化用户密码 </summary>
    public static string GetAccountPwd()
    {
        return PlayerPrefs.GetString(ConstDefine.LogOn_AccountPwd, null);
    }
    #endregion

    #region ------ 选区相关 ------

    /// <summary> 设置进入游戏选区Id </summary>
    /// <param name="value">值</param>
    public static void SetServerEnterID(int value)
    {
        PlayerPrefs.SetInt(ConstDefine.GameServerEnter_Id, value);
    }
    /// <summary> 获取进入游戏选区Id </summary>
    public static int GetServerEnterID()
    {
        return PlayerPrefs.GetInt(ConstDefine.GameServerEnter_Id, -1);
    }
    /// <summary> 设置进入游戏选区区名 </summary>
    public static void SetServerEnterName(string value)
    {
        PlayerPrefs.SetString(ConstDefine.GameServerEnter_Name, value);
    }
    /// <summary> 获取进入游戏选区区名 </summary>
    public static string GetServerEnterName()
    {
        return PlayerPrefs.GetString(ConstDefine.GameServerEnter_Name, null);
    }

    /// <summary> 设置进入游戏选区Ip  </summary>
    public static void SetServerEnterIp(string value)
    {
        PlayerPrefs.SetString(ConstDefine.GameServerEnter_Ip, value);
    }
    /// <summary> 获取进入游戏选区Ip </summary>
    public static string GetServerEnterIp()
    {
        return PlayerPrefs.GetString(ConstDefine.GameServerEnter_Ip, null);
    }

    /// <summary> 设置进入游戏选区端口号 </summary>
    public static void SetServerEnterPort(int value)
    {
        PlayerPrefs.SetInt(ConstDefine.GameServerEnter_Port, value);
    }
    /// <summary> 获取进入游戏选区端口号 </summary>
    public static int GetServerEnterPort()
    {
        return PlayerPrefs.GetInt(ConstDefine.GameServerEnter_Port, 0);
    }

    #endregion
}
