/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-06 13:38:37 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-06 13:38:37 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
/// <summary> 角色攻击相关信息(物理攻击和技能攻击信息) </summary>
public class RoleAttackInfo
{
    /// <summary> 
    /// 索引号(技能在配置表中的索引号) 
    /// 如：{104，105，106} 104则为1
    /// </summary>
    public int Index;
    /// <summary> 技能Id </summary>
    public int SkillId;

    /// <summary> 技能冷却时间 </summary>
    public float CD;

    /// <summary> 特效名称(角色在真正的战斗场景使用) </summary>
    public string EffectName;

    /// <summary> 播放特效延迟 </summary>
    public float ShowEffectDelay;

    /// <summary> 特效存活时间 </summary>
    public float EffectLifeTime;

    /// <summary> 攻击范围 </summary>
    public float AttackRange = 0f;
    /// <summary> 对方播放受伤动画延迟时间 </summary>
    public float HurtDelayTime = 0f;

    /// <summary> 是否需要震动屏幕 </summary>
    public bool IsDoCameraShake;

    /// <summary> 震动屏幕延迟时间 </summary>
    public float CameraShakeDelay;
}
