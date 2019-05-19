using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Test数据管理
/// </summary>
public partial class TestDBModel : AbstractDBModel<TestDBModel, TestEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Test.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override TestEntity MakeEntity(GameDataTableParser parse)
    {
        TestEntity entity = new TestEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Gender = parse.GetFieldValue("Gender").ToInt();
        entity.Rotate = parse.GetFieldValue("Rotate").ToFloat();
        entity.NameList = parse.GetFieldValue("NameList").ToStringArray();
        entity.NumList = parse.GetFieldValue("NumList").ToIntArray();
        entity.DoubleNameList = parse.GetFieldValue("DoubleNameList").ToDoubleStringArray();
        return entity;
    }
}
