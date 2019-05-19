
//===================================================
//作    者：
//创建时间：2019-01-17 15:18:26
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Job数据管理
/// </summary>
public partial class JobDBModel : AbstractDBModel<JobDBModel, JobEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Job.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override JobEntity MakeEntity(GameDataTableParser parse)
    {
        JobEntity entity = new JobEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Gender = parse.GetFieldValue("Gender").ToInt();
        entity.Rotate = parse.GetFieldValue("Rotate").ToFloat();
        entity.DescId = parse.GetFieldValue("DescId").ToInt();
        entity.AttackCoefficient = parse.GetFieldValue("AttackCoefficient").ToInt();
        entity.DefenseCoefficient = parse.GetFieldValue("DefenseCoefficient").ToInt();
        entity.HitCoefficient = parse.GetFieldValue("HitCoefficient").ToInt();
        entity.DodgeCoefficient = parse.GetFieldValue("DodgeCoefficient").ToInt();
        entity.CriCoefficient = parse.GetFieldValue("CriCoefficient").ToInt();
        entity.ResCoefficient = parse.GetFieldValue("ResCoefficient").ToInt();
        entity.PhyAttackIdArray = parse.GetFieldValue("PhyAttackIdArray").ToIntArray();
        entity.SkillAttackIdArray = parse.GetFieldValue("SkillAttackIdArray").ToIntArray();
        entity.HeadPic = parse.GetFieldValue("HeadPic");
        entity.JobPic = parse.GetFieldValue("JobPic");
        entity.FloaderName = parse.GetFieldValue("FloaderName");
        entity.PrefabName = parse.GetFieldValue("PrefabName");
        entity.WeaponFloader = parse.GetFieldValue("WeaponFloader");
        entity.WeaponPath = parse.GetFieldValue("WeaponPath").ToStringArray();
        entity.WeaponParent = parse.GetFieldValue("WeaponParent").ToStringArray();
        return entity;
    }
}
