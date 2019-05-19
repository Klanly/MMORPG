
//===================================================
//作    者：
//创建时间：2019-05-18 11:12:46
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// ChapterLevel数据管理
/// </summary>
public partial class ChapterLevelDBModel : AbstractDBModel<ChapterLevelDBModel, ChapterLevelEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "ChapterLevel.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override ChapterLevelEntity MakeEntity(GameDataTableParser parse)
    {
        ChapterLevelEntity entity = new ChapterLevelEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Chapter = parse.GetFieldValue("Chapter").ToInt();
        entity.Level = parse.GetFieldValue("Level").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.WorldMapId = parse.GetFieldValue("WorldMapId").ToInt();
        entity.Star2 = parse.GetFieldValue("Star2").ToInt();
        entity.Star3 = parse.GetFieldValue("Star3").ToInt();
        entity.IsBoos = parse.GetFieldValue("IsBoos").ToInt();
        entity.FolderName = parse.GetFieldValue("FolderName");
        entity.IconName = parse.GetFieldValue("IconName");
        entity.RecommendFight = parse.GetFieldValue("RecommendFight").ToInt();
        entity.MaxCount = parse.GetFieldValue("MaxCount").ToInt();
        entity.NeedVitality = parse.GetFieldValue("NeedVitality").ToInt();
        entity.FirstThreeStar = parse.GetFieldValue("FirstThreeStar").ToInt();
        entity.Desc = parse.GetFieldValue("Desc");
        return entity;
    }
}
