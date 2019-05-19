
//===================================================
//作    者：
//创建时间：2019-01-17 15:18:26
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// Skill实体
/// </summary>
public partial class SkillEntity : AbstractEntity
{
    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

    /// <summary>
    /// 技能描述
    /// </summary>
    public string SkillDesc { get; set; }

    /// <summary>
    /// 特效名
    /// </summary>
    public string EffectName { get; set; }

    /// <summary>
    /// 技能图片所在图集
    /// </summary>
    public string SkillIconFolder { get; set; }

    /// <summary>
    /// 技能图片名
    /// </summary>
    public string SkillIconName { get; set; }

    /// <summary>
    /// 技能伤害倍率
    /// </summary>
    public int HurtValueRate { get; set; }

    /// <summary>
    /// 技能每升一级提升的倍率
    /// </summary>
    public int HurtValueRateUp { get; set; }

    /// <summary>
    /// 技能最大等级
    /// </summary>
    public int LevelLimit { get; set; }

    /// <summary>
    /// 施放技能所需MP
    /// </summary>
    public int SpendMP { get; set; }

    /// <summary>
    /// 技能每升一级所需要增加的MP
    /// </summary>
    public int SpendMPLevelUp { get; set; }

    /// <summary>
    /// 是否物理攻击
    /// </summary>
    public int IsPhyAttack { get; set; }

    /// <summary>
    /// 是否震屏
    /// </summary>
    public int IsDoCameraShake { get; set; }

    /// <summary>
    /// 震屏延迟时间
    /// </summary>
    public float CameraShakeDelay { get; set; }

    /// <summary>
    /// 技能冷却时间(秒)
    /// </summary>
    public float SkillCD { get; set; }

    /// <summary>
    /// 技能每升一级减多少冷却时间(秒)
    /// </summary>
    public float SkillUpCDTime { get; set; }

    /// <summary>
    /// 特效生命时长
    /// </summary>
    public float EffectLifeTime { get; set; }

    /// <summary>
    /// 伤害目标数量
    /// </summary>
    public int AttackTargetCount { get; set; }

    /// <summary>
    /// 技能是否对怪释放
    /// </summary>
    public int IsTargetMonster { get; set; }

    /// <summary>
    /// 此技能攻击攻击范围(米)
    /// </summary>
    public float AttackRange { get; set; }

    /// <summary>
    /// 群攻的伤害判定半径
    /// </summary>
    public float AreaAttackRadius { get; set; }

    /// <summary>
    /// 动作播放多久后才播放特效
    /// </summary>
    public float ShowEffectDelay { get; set; }

    /// <summary>
    /// 攻击动作发出多少秒后被攻击者才播放受伤效果
    /// </summary>
    public float ShowHurtEffectDelaySecond { get; set; }

    /// <summary>
    /// 主角被这个物理子攻击后，是否会导致屏幕四周泛红
    /// </summary>
    public int RedScreen { get; set; }

    /// <summary>
    /// 攻击后状态
    /// </summary>
    public int AttackState { get; set; }

    /// <summary>
    /// 附加异常状态
    /// </summary>
    public int AbnormalState { get; set; }

    /// <summary>
    /// BuffInfoID
    /// </summary>
    public int BuffInfoID { get; set; }

    /// <summary>
    /// Buff类型
    /// </summary>
    public int BuffTargetFilter { get; set; }

    /// <summary>
    /// Buff效果值是否按百分比计算
    /// </summary>
    public int BuffIsPercentage { get; set; }

}
