
//===================================================
//作    者：
//创建时间：2019-01-17 15:18:26
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// Job实体
/// </summary>
public partial class JobEntity : AbstractEntity
{
    /// <summary>
    /// 职业名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 旋转
    /// </summary>
    public float Rotate { get; set; }

    /// <summary>
    /// 职业描述
    /// </summary>
    public int DescId { get; set; }

    /// <summary>
    /// 攻击系数
    /// </summary>
    public int AttackCoefficient { get; set; }

    /// <summary>
    /// 防御系数
    /// </summary>
    public int DefenseCoefficient { get; set; }

    /// <summary>
    /// 命中系数
    /// </summary>
    public int HitCoefficient { get; set; }

    /// <summary>
    /// 闪避系数
    /// </summary>
    public int DodgeCoefficient { get; set; }

    /// <summary>
    /// 暴击系数
    /// </summary>
    public int CriCoefficient { get; set; }

    /// <summary>
    /// 魔防系数
    /// </summary>
    public int ResCoefficient { get; set; }

    /// <summary>
    /// 物理技能Id列表
    /// </summary>
    public int[] PhyAttackIdArray { get; set; }

    /// <summary>
    /// 技能Id列表
    /// </summary>
    public int[] SkillAttackIdArray { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string HeadPic { get; set; }

    /// <summary>
    /// 职业半身像
    /// </summary>
    public string JobPic { get; set; }

    /// <summary>
    /// 角色所在文件夹名
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
