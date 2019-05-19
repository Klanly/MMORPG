/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-10 22:14:50 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-10 22:14:50 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public partial class SkillDBModel {

    private Dictionary<int, List<RoleAttackInfo>> PhyAttackInfoDic = new Dictionary<int, List<RoleAttackInfo>>();
    private Dictionary<int, List<RoleAttackInfo>> SkillAttackInfoDic = new Dictionary<int, List<RoleAttackInfo>>();

    public List<RoleAttackInfo> GetPhyAttakInfoList(int jobId)
    {
        if (PhyAttackInfoDic.ContainsKey(jobId))
        {
            return PhyAttackInfoDic[jobId];
        }
        List<RoleAttackInfo> list = new List<RoleAttackInfo>();
        int[] phyAttakIdArray = JobDBModel.Instance.Get(jobId).PhyAttackIdArray;
        for (int i = 0; i < phyAttakIdArray.Length; i++)
        {
            SkillEntity skillEntity = Get(phyAttakIdArray[i]);
            RoleAttackInfo info = new RoleAttackInfo();
            info.Index = i + 1;
            info.SkillId = phyAttakIdArray[i];
            info.EffectName = skillEntity.EffectName;
            info.ShowEffectDelay = skillEntity.ShowEffectDelay;
            info.EffectLifeTime = skillEntity.EffectLifeTime;
            info.AttackRange = skillEntity.AttackRange;
            info.HurtDelayTime = skillEntity.ShowHurtEffectDelaySecond;
            info.IsDoCameraShake = skillEntity.IsDoCameraShake>0;
            info.CameraShakeDelay = skillEntity.CameraShakeDelay;
            list.Add(info);
        }
        PhyAttackInfoDic[jobId] = list;
        return PhyAttackInfoDic[jobId];
    }
    public List<RoleAttackInfo> GetSkillAttakInfoList(int jobId)
    {
        if (SkillAttackInfoDic.ContainsKey(jobId))
        {
            return SkillAttackInfoDic[jobId];
        }
        List<RoleAttackInfo> list = new List<RoleAttackInfo>();
        int[] skillAttakIdArray = GlobalInit.Instance.PlayerInfo.SkillIdArrayBySlots;
        for (int i = 0; i < skillAttakIdArray.Length; i++)
        {
            SkillEntity skillEntity = Get(skillAttakIdArray[i]);
            RoleAttackInfo info = new RoleAttackInfo();
            //info.Index = i + 1;
            info.Index = GetIndex(jobId, skillAttakIdArray[i]);
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
        SkillAttackInfoDic[jobId] = list;
        return SkillAttackInfoDic[jobId];
    }
    private int GetIndex(int jobId, int skillId)
    {
        int[] skillArray = JobDBModel.Instance.Get(jobId).SkillAttackIdArray;
        for (int i = 0; i < skillArray.Length; i++)
        {
            if (skillArray[i] == skillId)
            {
                return i + 1;
            }
        }
        return 0;
    }
}
