
//===================================================
//作    者：
//创建时间：2019-05-18 11:12:46
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Chapter数据管理
/// </summary>
public partial class ChapterDBModel : AbstractDBModel<ChapterDBModel, ChapterEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Chapter.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override ChapterEntity MakeEntity(GameDataTableParser parse)
    {
        ChapterEntity entity = new ChapterEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.LevelCount = parse.GetFieldValue("LevelCount").ToInt();
        entity.FolderName = parse.GetFieldValue("FolderName");
        entity.IconName = parse.GetFieldValue("IconName");
        return entity;
    }
}
