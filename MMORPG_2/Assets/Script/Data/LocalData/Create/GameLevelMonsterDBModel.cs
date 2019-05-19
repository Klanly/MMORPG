
//===================================================
//作    者：
//创建时间：2019-05-18 11:12:46
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// GameLevelMonster数据管理
/// </summary>
public partial class GameLevelMonsterDBModel : AbstractDBModel<GameLevelMonsterDBModel, GameLevelMonsterEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "GameLevelMonster.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override GameLevelMonsterEntity MakeEntity(GameDataTableParser parse)
    {
        GameLevelMonsterEntity entity = new GameLevelMonsterEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Chapter = parse.GetFieldValue("Chapter").ToInt();
        entity.GameLevel = parse.GetFieldValue("GameLevel").ToInt();
        entity.Grade = parse.GetFieldValue("Grade").ToInt();
        entity.RegionId = parse.GetFieldValue("RegionId").ToInt();
        entity.MonsterId = parse.GetFieldValue("MonsterId").ToInt();
        entity.MonsterCount = parse.GetFieldValue("MonsterCount").ToInt();
        entity.DropExp = parse.GetFieldValue("DropExp").ToInt();
        entity.DropCoin = parse.GetFieldValue("DropCoin").ToInt();
        entity.DropEquip = parse.GetFieldValue("DropEquip").ToDoubleStringArray();
        entity.DropMaterial = parse.GetFieldValue("DropMaterial").ToDoubleStringArray();
        return entity;
    }
}
