
//===================================================
//作    者：
//创建时间：2019-02-07 10:26:42
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// WorldMap数据管理
/// </summary>
public partial class WorldMapDBModel : AbstractDBModel<WorldMapDBModel, WorldMapEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "WorldMap.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override WorldMapEntity MakeEntity(GameDataTableParser parse)
    {
        WorldMapEntity entity = new WorldMapEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.SceneName = parse.GetFieldValue("SceneName");
        entity.SceneType = parse.GetFieldValue("SceneType");
        entity.RoleBirthPos = parse.GetFieldValue("RoleBirthPos").ToFloatArray();
        entity.NPCExcel = parse.GetFieldValue("NPCExcel");
        entity.NPCFloader = parse.GetFieldValue("NPCFloader");
        entity.CameraRotation = parse.GetFieldValue("CameraRotation").ToFloat();
        entity.NearSceneIdArray = parse.GetFieldValue("NearSceneIdArray").ToIntArray();
        return entity;
    }
}
