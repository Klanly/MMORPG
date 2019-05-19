
//===================================================
//作    者：
//创建时间：2019-01-17 15:18:26
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Skill数据管理
/// </summary>
public partial class SkillDBModel : AbstractDBModel<SkillDBModel, SkillEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Skill.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override SkillEntity MakeEntity(GameDataTableParser parse)
    {
        SkillEntity entity = new SkillEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.SkillName = parse.GetFieldValue("SkillName");
        entity.SkillDesc = parse.GetFieldValue("SkillDesc");
        entity.EffectName = parse.GetFieldValue("EffectName");
        entity.SkillIconFolder = parse.GetFieldValue("SkillIconFolder");
        entity.SkillIconName = parse.GetFieldValue("SkillIconName");
        entity.HurtValueRate = parse.GetFieldValue("HurtValueRate").ToInt();
        entity.HurtValueRateUp = parse.GetFieldValue("HurtValueRateUp").ToInt();
        entity.LevelLimit = parse.GetFieldValue("LevelLimit").ToInt();
        entity.SpendMP = parse.GetFieldValue("SpendMP").ToInt();
        entity.SpendMPLevelUp = parse.GetFieldValue("SpendMPLevelUp").ToInt();
        entity.IsPhyAttack = parse.GetFieldValue("IsPhyAttack").ToInt();
        entity.IsDoCameraShake = parse.GetFieldValue("IsDoCameraShake").ToInt();
        entity.CameraShakeDelay = parse.GetFieldValue("CameraShakeDelay").ToFloat();
        entity.SkillCD = parse.GetFieldValue("SkillCD").ToFloat();
        entity.SkillUpCDTime = parse.GetFieldValue("SkillUpCDTime").ToFloat();
        entity.EffectLifeTime = parse.GetFieldValue("EffectLifeTime").ToFloat();
        entity.AttackTargetCount = parse.GetFieldValue("AttackTargetCount").ToInt();
        entity.IsTargetMonster = parse.GetFieldValue("IsTargetMonster").ToInt();
        entity.AttackRange = parse.GetFieldValue("AttackRange").ToFloat();
        entity.AreaAttackRadius = parse.GetFieldValue("AreaAttackRadius").ToFloat();
        entity.ShowEffectDelay = parse.GetFieldValue("ShowEffectDelay").ToFloat();
        entity.ShowHurtEffectDelaySecond = parse.GetFieldValue("ShowHurtEffectDelaySecond").ToFloat();
        entity.RedScreen = parse.GetFieldValue("RedScreen").ToInt();
        entity.AttackState = parse.GetFieldValue("AttackState").ToInt();
        entity.AbnormalState = parse.GetFieldValue("AbnormalState").ToInt();
        entity.BuffInfoID = parse.GetFieldValue("BuffInfoID").ToInt();
        entity.BuffTargetFilter = parse.GetFieldValue("BuffTargetFilter").ToInt();
        entity.BuffIsPercentage = parse.GetFieldValue("BuffIsPercentage").ToInt();
        return entity;
    }
}
