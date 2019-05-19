
//===================================================
//作    者：
//创建时间：2019-01-10 09:41:39
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// MainMenuIcon1数据管理
/// </summary>
public partial class MainMenuIcon1DBModel : AbstractDBModel<MainMenuIcon1DBModel, MainMenuIcon1Entity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "MainMenuIcon1.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override MainMenuIcon1Entity MakeEntity(GameDataTableParser parse)
    {
        MainMenuIcon1Entity entity = new MainMenuIcon1Entity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Weight = parse.GetFieldValue("Weight").ToInt();
        entity.AtlasName = parse.GetFieldValue("AtlasName");
        entity.IconName = parse.GetFieldValue("IconName");
        entity.ShowLevel = parse.GetFieldValue("ShowLevel").ToInt();
        entity.LimitLevel = parse.GetFieldValue("LimitLevel").ToInt();
        entity.OpenWindowName = parse.GetFieldValue("OpenWindowName");
        entity.Page = parse.GetFieldValue("Page").ToInt();
        return entity;
    }
}
