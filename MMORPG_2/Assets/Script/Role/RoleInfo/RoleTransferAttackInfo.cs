/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-13 08:23:38 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-13 08:23:38 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 角色攻击传递的信息 </summary>
public class RoleTransferAttackInfo
{

    /// <summary> 攻击者编号 </summary>
    public int AttackRoleId;

    /// <summary> 攻击者位置 </summary>
    public Vector3 AttackRolePos;

    /// <summary> 被攻击者编号 </summary>
    public int BeAttackRoleId;

    /// <summary> 伤害数值 </summary>
    public int HurtValue;

    /// <summary> 攻击者使用的技能Id </summary>
    public int SkillId;

    /// <summary> 攻击者使用的技能等级 </summary>
    public int SkillLevel;

    /// <summary> 是否附加异常状态 </summary>
    public bool IsAbnormal;

    /// <summary> 是否暴击 </summary>
    public bool IsCri;
    
}
