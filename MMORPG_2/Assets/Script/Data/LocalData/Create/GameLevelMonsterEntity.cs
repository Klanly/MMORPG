
//===================================================
//作    者：
//创建时间：2019-05-18 11:12:46
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// GameLevelMonster实体
/// </summary>
public partial class GameLevelMonsterEntity : AbstractEntity
{
    /// <summary>
    /// 所属章节
    /// </summary>
    public int Chapter { get; set; }

    /// <summary>
    /// 所属章节关卡Id
    /// </summary>
    public int GameLevel { get; set; }

    /// <summary>
    /// 难度等级
    /// </summary>
    public int Grade { get; set; }

    /// <summary>
    /// 区域Id
    /// </summary>
    public int RegionId { get; set; }

    /// <summary>
    /// 怪物Id
    /// </summary>
    public int MonsterId { get; set; }

    /// <summary>
    /// 怪物数量
    /// </summary>
    public int MonsterCount { get; set; }

    /// <summary>
    /// 掉落经验
    /// </summary>
    public int DropExp { get; set; }

    /// <summary>
    /// 掉落金币
    /// </summary>
    public int DropCoin { get; set; }

    /// <summary>
    /// 掉落装备
    /// </summary>
    public string[][] DropEquip { get; set; }

    /// <summary>
    /// 掉落材料
    /// </summary>
    public string[][] DropMaterial { get; set; }

}
