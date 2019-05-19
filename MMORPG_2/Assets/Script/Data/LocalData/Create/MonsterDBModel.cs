
//===================================================
//作    者：
//创建时间：2019-01-23 10:29:23
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Monster数据管理
/// </summary>
public partial class MonsterDBModel : AbstractDBModel<MonsterDBModel, MonsterEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Monster.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override MonsterEntity MakeEntity(GameDataTableParser parse)
    {
        MonsterEntity entity = new MonsterEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Level = parse.GetFieldValue("Level").ToInt();
        entity.HP = parse.GetFieldValue("HP").ToInt();
        entity.Attack = parse.GetFieldValue("Attack").ToInt();
        entity.Defense = parse.GetFieldValue("Defense").ToInt();
        entity.ResDefense = parse.GetFieldValue("ResDefense").ToInt();
        entity.Hit = parse.GetFieldValue("Hit").ToInt();
        entity.Dodge = parse.GetFieldValue("Dodge").ToInt();
        entity.Cri = parse.GetFieldValue("Cri").ToInt();
        entity.Rotate = parse.GetFieldValue("Rotate").ToFloat();
        entity.AttackSpeed = parse.GetFieldValue("AttackSpeed").ToFloat();
        entity.PhyAttackPro = parse.GetFieldValue("PhyAttackPro").ToFloat();
        entity.DelaySecAttack = parse.GetFieldValue("DelaySecAttack").ToFloat();
        entity.ViewRadius = parse.GetFieldValue("ViewRadius").ToFloat();
        entity.PatrolRadius = parse.GetFieldValue("PatrolRadius").ToFloat();
        entity.MoveSpeed = parse.GetFieldValue("MoveSpeed").ToFloat();
        entity.PhyAttackIdArray = parse.GetFieldValue("PhyAttackIdArray").ToIntArray();
        entity.SkillAttackIdArray = parse.GetFieldValue("SkillAttackIdArray").ToIntArray();
        entity.DescId = parse.GetFieldValue("DescId").ToInt();
        entity.IconFloader = parse.GetFieldValue("IconFloader");
        entity.IconName = parse.GetFieldValue("IconName");
        entity.HalfIcon = parse.GetFieldValue("HalfIcon");
        entity.FloaderName = parse.GetFieldValue("FloaderName");
        entity.PrefabName = parse.GetFieldValue("PrefabName");
        entity.WeaponFloader = parse.GetFieldValue("WeaponFloader");
        entity.WeaponPath = parse.GetFieldValue("WeaponPath").ToStringArray();
        entity.WeaponParent = parse.GetFieldValue("WeaponParent").ToStringArray();
        return entity;
    }
}
