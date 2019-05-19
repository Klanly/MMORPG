/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-17 10:27:28 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-17 10:27:28 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public partial class MonsterDBModel
{
    private Dictionary<int, List<RoleAttackInfo>> PhyAttackInfoDic = new Dictionary<int, List<RoleAttackInfo>>();
    private Dictionary<int, List<RoleAttackInfo>> SkillAttackInfoDic = new Dictionary<int, List<RoleAttackInfo>>();

    public List<RoleAttackInfo> GetPhyAttakInfoList(int monsterId)
    {
        if (PhyAttackInfoDic.ContainsKey(monsterId))
        {
            return PhyAttackInfoDic[monsterId];
        }
        List<RoleAttackInfo> list = new List<RoleAttackInfo>();
        int[] phyAttakIdArray = Get(monsterId).PhyAttackIdArray;
        for (int i = 0; i < phyAttakIdArray.Length; i++)
        {
            SkillEntity skillEntity = SkillDBModel.Instance.Get(phyAttakIdArray[i]);
            RoleAttackInfo info = new RoleAttackInfo();
            info.Index = i + 1;
            info.SkillId = phyAttakIdArray[i];
            info.EffectName = skillEntity.EffectName;
            info.ShowEffectDelay = skillEntity.ShowEffectDelay;
            info.EffectLifeTime = skillEntity.EffectLifeTime;
            info.AttackRange = skillEntity.AttackRange;
            info.HurtDelayTime = skillEntity.ShowHurtEffectDelaySecond;
            info.IsDoCameraShake = skillEntity.IsDoCameraShake > 0;
            info.CameraShakeDelay = skillEntity.CameraShakeDelay;
            list.Add(info);
        }
        PhyAttackInfoDic[monsterId] = list;
        return PhyAttackInfoDic[monsterId];
    }
    public List<RoleAttackInfo> GetSkillAttakInfoList(int monsterId)
    {
        if (SkillAttackInfoDic.ContainsKey(monsterId))
        {
            return SkillAttackInfoDic[monsterId];
        }
        List<RoleAttackInfo> list = new List<RoleAttackInfo>();
        int[] skillAttakIdArray = Get(monsterId).PhyAttackIdArray;
        for (int i = 0; i < skillAttakIdArray.Length; i++)
        {
            SkillEntity skillEntity = SkillDBModel.Instance.Get(skillAttakIdArray[i]);
            RoleAttackInfo info = new RoleAttackInfo();
            info.Index = i + 1;
            info.SkillId = skillAttakIdArray[i];
            info.EffectName = skillEntity.EffectName;
            info.ShowEffectDelay = skillEntity.ShowEffectDelay;
            info.EffectLifeTime = skillEntity.EffectLifeTime;
            info.AttackRange = skillEntity.AttackRange;
            info.HurtDelayTime = skillEntity.ShowHurtEffectDelaySecond;
            info.IsDoCameraShake = skillEntity.IsDoCameraShake > 0;
            info.CameraShakeDelay = skillEntity.CameraShakeDelay;
            list.Add(info);
        }
        SkillAttackInfoDic[monsterId] = list;
        return SkillAttackInfoDic[monsterId];
    }
}
