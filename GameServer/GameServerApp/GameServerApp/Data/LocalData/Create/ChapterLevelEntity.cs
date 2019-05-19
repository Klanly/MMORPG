
//===================================================
//作    者：
//创建时间：2019-05-18 11:12:46
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// ChapterLevel实体
/// </summary>
public partial class ChapterLevelEntity : AbstractEntity
{
    /// <summary>
    /// 所属章节
    /// </summary>
    public int Chapter { get; set; }

    /// <summary>
    /// 章节等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 图标名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 世界地图Id
    /// </summary>
    public int WorldMapId { get; set; }

    /// <summary>
    /// 2星通关时间
    /// </summary>
    public int Star2 { get; set; }

    /// <summary>
    /// 3星通关时间
    /// </summary>
    public int Star3 { get; set; }

    /// <summary>
    /// 是否Boss
    /// </summary>
    public int IsBoos { get; set; }

    /// <summary>
    /// 背景所在文件夹名
    /// </summary>
    public string FolderName { get; set; }

    /// <summary>
    /// 图片名
    /// </summary>
    public string IconName { get; set; }

    /// <summary>
    /// 推荐战力
    /// </summary>
    public int RecommendFight { get; set; }

    /// <summary>
    /// 最大挑战次数
    /// </summary>
    public int MaxCount { get; set; }

    /// <summary>
    /// 所需活力
    /// </summary>
    public int NeedVitality { get; set; }

    /// <summary>
    /// 首次三星奖励绑金数
    /// </summary>
    public int FirstThreeStar { get; set; }

    /// <summary>
    /// 图标名称
    /// </summary>
    public string Desc { get; set; }

}
