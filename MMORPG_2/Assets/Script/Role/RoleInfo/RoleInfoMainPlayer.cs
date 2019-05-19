/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-02 14:57:08 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-02 14:57:08 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 主角信息 </summary>
public class RoleInfoMainPlayer : RoleInfoBase {
    
    /// <summary> 职业编号 </summary>
    public int JobId;
    /// <summary> 元宝 </summary>
    public int Money;
    /// <summary> 金币 </summary>
    public int Gold;

    /// <summary> 根据技能槽设置技能Id(服务端技能槽为空的从配置表中依次插入) 长度跟配置表中的技能id数组长度一直 </summary>
    public int[] SkillIdArrayBySlots;
    public RoleInfoMainPlayer(RoleInfoResponseProto proto):base()
    {
        base.Init(proto);
        JobId = proto.JobId;
        Money = proto.Money;
        Gold = proto.Gold;
    }

    /// <summary> 加载角色已经学会的技能 </summary>
    /// <param name="proto">服务端返回的技能列表协议</param>
    public void LoadSkill(RoleSkillDataResponseProto proto)
    {
        SkillDic.Clear();
        int[] dataSkillIdArray = JobDBModel.Instance.Get(JobId).SkillAttackIdArray;
        SkillIdArrayBySlots = new int[dataSkillIdArray.Length];
        for (int i = 0; i < proto.CurrSkillDataList.Count; i++)
        {
            SkillDic[proto.CurrSkillDataList[i].SlotsNode] = new RoleSkillInfo()
            {
                SkillId = proto.CurrSkillDataList[i].SkillId,
                SkillLevel = proto.CurrSkillDataList[i].SkillLevel,
                SlotsNode = proto.CurrSkillDataList[i].SlotsNode
            };
            SkillIdArrayBySlots[proto.CurrSkillDataList[i].SlotsNode-1] = proto.CurrSkillDataList[i].SkillId;
        }

        for (int i = 0; i < dataSkillIdArray.Length; i++)
        {
            bool isHave = false;
            for (int j = 0; j < SkillIdArrayBySlots.Length; j++)
            {
                if (dataSkillIdArray[i] == SkillIdArrayBySlots[j])
                {
                    isHave = true;
                }
            }
            if (!isHave)
            {
                for (int j = 0; j < SkillIdArrayBySlots.Length; j++)
                {
                    if (SkillIdArrayBySlots[j] == 0)
                    {
                        SkillIdArrayBySlots[j] = dataSkillIdArray[i];
                        break;
                    }
                }
            }
        }
    }
}
