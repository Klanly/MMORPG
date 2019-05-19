
//===================================================
//作    者：
//创建时间：2019-01-23 10:29:23
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// Monster实体
/// </summary>
public partial class MonsterEntity : AbstractEntity
{
    /// <summary>
    /// 怪物名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 血量
    /// </summary>
    public int HP { get; set; }

    /// <summary>
    /// 攻击
    /// </summary>
    public int Attack { get; set; }

    /// <summary>
    /// 物理防御
    /// </summary>
    public int Defense { get; set; }

    /// <summary>
    /// 魔法防御
    /// </summary>
    public int ResDefense { get; set; }

    /// <summary>
    /// 命中
    /// </summary>
    public int Hit { get; set; }

    /// <summary>
    /// 闪避
    /// </summary>
    public int Dodge { get; set; }

    /// <summary>
    /// 必杀
    /// </summary>
    public int Cri { get; set; }

    /// <summary>
    /// 旋转
    /// </summary>
    public float Rotate { get; set; }

    /// <summary>
    /// 每隔多少秒攻击一次
    /// </summary>
    public float AttackSpeed { get; set; }

    /// <summary>
    /// 物理攻击概率
    /// </summary>
    public float PhyAttackPro { get; set; }

    /// <summary>
    /// 当主角进入其攻击范围后，延迟多少秒开始攻击
    /// </summary>
    public float DelaySecAttack { get; set; }

    /// <summary>
    /// 怪物的视野范围
    /// </summary>
    public float ViewRadius { get; set; }

    /// <summary>
    /// 怪物的巡逻范围半径
    /// </summary>
    public float PatrolRadius { get; set; }

    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed { get; set; }

    /// <summary>
    /// 物理技能Id列表
    /// </summary>
    public int[] PhyAttackIdArray { get; set; }

    /// <summary>
    /// 技能Id列表
    /// </summary>
    public int[] SkillAttackIdArray { get; set; }

    /// <summary>
    /// 职业描述
    /// </summary>
    public int DescId { get; set; }

    /// <summary>
    /// 怪物头像所在文件夹名
    /// </summary>
    public string IconFloader { get; set; }

    /// <summary>
    /// 怪物头像图片名
    /// </summary>
    public string IconName { get; set; }

    /// <summary>
    /// 怪物半身像
    /// </summary>
    public string HalfIcon { get; set; }

    /// <summary>
    /// 怪物所在文件夹路径
    /// </summary>
    public string FloaderName { get; set; }

    /// <summary>
    /// 预设名称
    /// </summary>
    public string PrefabName { get; set; }

    /// <summary>
    /// 武器所在文件夹
    /// </summary>
    public string WeaponFloader { get; set; }

    /// <summary>
    /// 武器路径
    /// </summary>
    public string[] WeaponPath { get; set; }

    /// <summary>
    /// 武器父对象路径
    /// </summary>
    public string[] WeaponParent { get; set; }

}
