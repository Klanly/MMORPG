
//===================================================
//作    者：
//创建时间：2019-01-10 09:41:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// NPCDataXinShouCun数据管理
/// </summary>
public partial class NPCDataXinShouCunDBModel : AbstractDBModel<NPCDataXinShouCunDBModel, NPCDataEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "NPCDataXinShouCun.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override NPCDataEntity MakeEntity(GameDataTableParser parse)
    {
        NPCDataEntity entity = new NPCDataEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.PrefabName = parse.GetFieldValue("PrefabName");
        entity.RoleBirthPos = parse.GetFieldValue("RoleBirthPos").ToFloatArray();
        entity.HeadPicAtlas = parse.GetFieldValue("HeadPicAtlas");
        entity.HeadPic = parse.GetFieldValue("HeadPic");
        entity.HalfBodyPath = parse.GetFieldValue("HalfBodyPath");
        entity.Talk = parse.GetFieldValue("Talk");
        return entity;
    }
}
