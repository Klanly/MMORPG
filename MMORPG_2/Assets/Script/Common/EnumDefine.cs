using UnityEngine;
using System.Collections;


/// <summary> 上弹提示类型 </summary>
public enum TextTipsType
{
    /// <summary> 纯文本提示 </summary>
    TextTips=0,
    /// <summary> 获得经验提示 </summary>
    ExpTips,
    /// <summary> 获得金币提示 </summary>
    CoinTips,
    /// <summary> 获得绑元提示 </summary>
    GoldBindingTips,
    /// <summary> 获得元宝提示 </summary>
    GoldTips
}


#region SceneType 场景类型
/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
{
    LogOn,
    SelectRole,
    City
}
#endregion

#region WindowUIType 窗口类型
/// <summary>
/// 窗口类型
/// </summary>
public enum WindowType
{
    /// <summary> 未设置 </summary>
    None,
    /// <summary> 登录窗口 </summary>
    LogOn,
    /// <summary> 注册窗口 </summary>
    Register,
    /// <summary> 角色信息窗口 </summary>
    RoleInfo,
    /// <summary> 选择服务器进入游戏 </summary>
    GameServerEnter
}
#endregion

#region WindowUIContainerType UI容器类型
/// <summary>
/// UI容器类型
/// </summary>
public enum WindowUIContainerType
{
    /// <summary>
    /// 左上
    /// </summary>
    TopLeft,
    /// <summary>
    /// 右上
    /// </summary>
    TopRight,
    /// <summary>
    /// 左下
    /// </summary>
    BottomLeft,
    /// <summary>
    /// 右下
    /// </summary>
    BottomRight,
    /// <summary>
    /// 居中
    /// </summary>
    Center
}
#endregion

#region WindowShowStyle 窗口打开方式
/// <summary>
/// 窗口打开方式
/// </summary>
public enum WindowShowStyle
{
    /// <summary>
    /// 正常打开
    /// </summary>
    Normal,
    /// <summary>
    /// 从中间放大
    /// </summary>
    CenterToBig,
    /// <summary>
    /// 从上往下
    /// </summary>
    FromTop,
    /// <summary>
    /// 从下往上
    /// </summary>
    FromDown,
    /// <summary>
    /// 从左向右
    /// </summary>
    FromLeft,
    /// <summary>
    /// 从右向左
    /// </summary>
    FromRight
}
#endregion

#region RoleType 角色类型
/// <summary> 角色类型 </summary>
public enum RoleType
{
    /// <summary> 未设置 </summary>
    None = 0,
    /// <summary> 当前玩家 </summary>
    MainPlayer = 1,
    /// <summary> 怪 </summary>
    Monster = 2,
        /// <summary> 其他玩家 </summary>
    OtherPlayer = 3
}
#endregion

/// <summary> 角色状态 </summary>
public enum RoleState
{
    /// <summary> 未设置 </summary>
    None = 0,
    /// <summary> 待机 </summary>
    Idle = 1,
    /// <summary> 跑了 </summary>
    Run = 4,
    /// <summary> 攻击 </summary>
    Attack = 3,
    /// <summary> 死亡 </summary>
    Die = 10,
    /// <summary> 受伤 </summary>
    Hurt = 12,
        /// <summary> 战斗胜利 </summary>
    Select = 13
}

/// <summary>角色动画状态名称</summary>
public enum RoleAnimatorState
{
    Idle=1,
    Run=2,
    Hurt=3,
    Die=4,
    Jump_01=5,
    PhyAttack1=6,
    PhyAttack2=7,
    PhyAttack3=8,
    PhyAttack4=9,
    Skill1=10,
    Skill2 = 11,
    Skill3 = 12,
    Skill4 = 13,
    Skill5 = 14,
    Skill6 = 15,
    Skill7 = 16,
    Skill8 = 17,
    Dali_Pose = 18,
    Dali_Selected = 19,
    Dali_Selected_Loop = 20,
    Marry = 21,
    Idle_Fight = 22,
}

/// <summary> 角色待机状态 </summary>
public enum RoleIdleState
{
    /// <summary> 普通待机 </summary>
    IdleNormal,
    /// <summary> 战斗待机 </summary>
    IdleFight
}

/// <summary> 角色攻击类型 </summary>
public enum RoleAttackType
{
    /// <summary> 物理攻击 </summary>
    PhyAttack,
    /// <summary> 技能攻击 </summary>
    SkillAttack
}

public enum ToAnimatorCondition
{
    ToIdleNormal,
    ToIdleFight,
    ToRun,
    ToHurt,
    ToDie,
    ToPhyAttack,
    ToSkill,
    ToPose,
    CurrState
}

/// <summary> 数值类型 </summary>
public enum ValueChnageType
{
    /// <summary> 增加 </summary>
    Add = 0,
    /// <summary> 减少 </summary>
    Reduce = 1
}