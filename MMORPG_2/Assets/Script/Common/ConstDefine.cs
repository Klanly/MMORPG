using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 定义点击事件 本地持久化使用 </summary>
public class ConstDefine {

    #region ------ 本地持久化账号 ------

    public const string LogOn_AccountID = "LogOn_AccountID";
    public const string LogOn_AccountUserName = "LogOn_AccountUserName";
    public const string LogOn_AccountPwd = "LogOn_AccountPwd";

    #endregion

    #region ------ 本地持久化选区 ------

    public const string GameServerEnter_Id = "GameServerEnter_Id";
    public const string GameServerEnter_Name = "GameServerEnter_Name";
    public const string GameServerEnter_Ip = "GameServerEnter_Ip";
    public const string GameServerEnter_Port = "GameServerEnter_Port";

    #endregion


    #region ------ 场景相关 ------

    //================ 场景类型 ================
    /// <summary> 城市(场景类型) </summary>
    public const string City = "City";
    /// <summary> 关卡(场景类型) </summary>
    public const string GameLevel = "GameLevel";


    //================ 场景名 ================
    public const string Scene_LogOn = "Scene_LogOn";
    public const string Scene_Loading = "Scene_Loading";
    public const string Scene_SelectRole = "Scene_SelectRole";
    public const string Scene_CunZhuang = "Scene_CunZhuang";
    public const string Scene_ShanGu = "Scene_ShanGu";

    #endregion

    public const string UILogOnWindow_LogOn = "UILogOnWindow_LogOn";
    public const string UILogOnWindow_ToRegister = "UILogOnWindow_ToRegister";

    public const string UIRegisterWindow_Register = "UIRegisterWindow_Register";
    public const string UIRegisterWindow_ToLogOn = "UIRegisterWindow_ToLogOn";

    #region ------ 选择区服 ------

    public const string UIGameServerEnterWindow_BtnChange = "UIGameServerEnterWindow_BtnChange";
    public const string UIGameServerEnterWindow_BtnEnter = "UIGameServerEnterWindow_BtnEnter";

    #endregion

    #region ------ 数据传输相关 ------

    /// <summary> 角色Id </summary>
    public const string RoleId = "RoleId";
    /// <summary> 职业Id </summary>
    public const string JobId = "JobId";
    /// <summary> 职业Id </summary>
    public const string RoleIcon = "RoleIcon";
    /// <summary> 昵称 </summary>
    public const string NickName = "NickName";
    /// <summary> 称号 </summary>
    public const string PapalName = "PapalName";
    /// <summary> 性别 </summary>
    public const string Sex = "Sex";
    /// <summary> 角色等级 </summary>
    public const string Level = "Level";
    /// <summary> 角色VIP等级 </summary>
    public const string VIPLevel = "VIPLevel";
    /// <summary> 元宝 </summary>
    public const string Money = "Money";
    /// <summary> 金币 </summary>
    public const string Gold = "Gold";
    /// <summary> 当前气血 </summary>
    public const string CurrHP = "CurrHP";
    /// <summary> 最大气血 </summary>
    public const string MaxHP = "MaxHP";
    /// <summary> 当前真气 </summary>
    public const string CurrMP = "CurrMP";
    /// <summary> 最大真气 </summary>
    public const string MaxMP = "MaxMP";
    /// <summary> 当前经验 </summary>
    public const string CurrExp = "CurrExp";
    /// <summary> 最大经验 </summary>
    public const string MaxExp = "MaxExp";
    /// <summary> 攻击 </summary>
    public const string Attack = "Attack";
    /// <summary> 攻击加成 </summary>
    public const string AttackAddition = "AttackAddition";
    /// <summary> 最终攻击 </summary>
    public const string FinalAttack = "FinalAttack";
    /// <summary> 防御 </summary>
    public const string Defense = "Defense";
    /// <summary> 防御加成 </summary>
    public const string DefenseAddition = "DefenseAddition";
    /// <summary> 最终防御 </summary>
    public const string FinalDefense = "FinalDefense";
    /// <summary> 魔防 </summary>
    public const string Res = "Res";
    /// <summary> 魔防加成 </summary>
    public const string ResAddition = "ResAddition";
    /// <summary> 最终魔防 </summary>
    public const string FinalRes = "FinalRes";
    /// <summary> 命中 </summary>
    public const string Hit = "Hit";
    /// <summary> 命中加成 </summary>
    public const string HitAddition = "HitAddition";
    /// <summary> 最终命中 </summary>
    public const string FinalHit = "FinalHit";
    /// <summary> 闪避 </summary>
    public const string Dodge = "Dodge";
    /// <summary> 闪避加成 </summary>
    public const string DodgeAddition = "DodgeAddition";
    /// <summary> 最终闪避 </summary>
    public const string FinalDodge = "FinalDodge";
    /// <summary> 必杀 </summary>
    public const string Cri = "Cri";
    /// <summary> 必杀加成 </summary>
    public const string CriAddition = "CriAddition";
    /// <summary> 最终必杀 </summary>
    public const string FinalCri = "FinalCri";
    /// <summary> 战斗力 </summary>
    public const string Fighting = "Fighting";
    /// <summary> 战斗力加成 </summary>
    public const string FightingAddition = "FightingAddition";
    /// <summary> 最终战斗力 </summary>
    public const string FinalFighting = "FinalFighting";
    /// <summary> 文件夹名 </summary>
    public const string FolderName = "FolderName";
    /// <summary> 图片名 </summary>
    public const string IconName = "IconName";
    /// <summary> 是否开启 </summary>
    public const string IsOpen = "IsOpen";
    /// <summary> 名称 </summary>
    public const string Name = "Name";
    /// <summary> 当前星级 </summary>
    public const string CurrStar = "CurrStar";
    /// <summary> 当前星级 </summary>
    public const string MaxStar = "MaxStar";
    /// <summary> 当前进度 </summary>
    public const string CurrProgeress = "CurrProgeress";
    /// <summary> 最大进度 </summary>
    public const string MaxProgeress = "MaxProgeress";
    /// <summary> 索引 </summary>
    public const string Index = "Index";
    /// <summary> 一个Int类型参数的委托 </summary>
    public const string ActionOneInt = "ActionOneInt";
    /// <summary> 最大次数 </summary>
    public const string MaxCount = "MaxCount";
    /// <summary> 描述 </summary>
    public const string Desc = "Desc";

    #region ------ UIGameLevelListCtrl/章节关卡列表  ------

    /// <summary> 是否是Boss </summary>
    public const string IsBoss = "IsBoss";
    /// <summary> 推荐战斗力 </summary>
    public const string RecommendFight = "RecommendFight";
    /// <summary> 所需体力 </summary>
    public const string NeedVitality = "NeedVitality";
    /// <summary> 首次三星 </summary>
    public const string FirstThreeStar = "FirstThreeStar";
    /// <summary> 每行的星数量 </summary>
    public const string RowStarCount = "RowStarCount";
    /// <summary> 2星限制时间 </summary>
    public const string Star2Time = "Star2Time";
    /// <summary> 3星限制时间 </summary>
    public const string Star3Time = "Star3Time";

    #endregion

    #region ------ Skill/技能相关 GameSceneCtrlBase  ------

    /// <summary> 技能Id </summary>
    public const string SkillId = "SkillId";
    /// <summary> 技能等级 </summary>
    public const string SkillLevel = "SkillLevel";
    /// <summary> 技能冷却时间 </summary>
    public const string SkillCD = "SkillCD";
    /// <summary> 是否处于激活状态 </summary>
    public const string IsActive = "IsActive";
    #endregion

    #region ------ UIFightResult相关/战斗结算界面相关  ------

    /// <summary> 战斗结果 0失败 1胜利 </summary>
    public const string FightValue = "FightValue";
    /// <summary> 用时 </summary>
    public const string UseTime = "UseTime";
    /// <summary> 获得经验值 </summary>
    public const string ExpValue = "ExpValue";
    /// <summary> 获得金币值 </summary>
    public const string CoinValue = "CoinValue";

    #endregion

    #endregion
}
