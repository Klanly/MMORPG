/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-10 16:31:35 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-10 16:31:35 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 角色技能信息 </summary>
public class RoleSkillInfo
{
    /// <summary> 技能编号 </summary>
    public int SkillId;
    /// <summary> 技能等级 </summary>
    public int SkillLevel;
    /// <summary> 技能槽编号 </summary>
    public byte SlotsNode;

    private float m_SkillCDTime = 0f;

    private int m_SpendMP = 0;

    /// <summary> 技能冷却时间 </summary>
    public float SkillCDTime
    {
        get
        {
            if (m_SkillCDTime > 0)
            {
                return m_SkillCDTime;
            }
            m_SkillCDTime = SkillDBModel.Instance.Get(SkillId).SkillCD - (SkillLevel - 1) * SkillDBModel.Instance.Get(SkillId).SkillUpCDTime;
            return m_SkillCDTime;
        }
    }
    /// <summary> 消耗MP </summary>
    public int SpendMP
    {
        get
        {
            if (m_SpendMP > 0)
            {
                return m_SpendMP;
            }
            m_SpendMP = SkillDBModel.Instance.Get(SkillId).SpendMP - (SkillLevel - 1) * SkillDBModel.Instance.Get(SkillId).SpendMPLevelUp;
            return m_SpendMP;
        }
    }
    /// <summary> 技能冷却结束时间 </summary>
    public float SkillCDEndTime;
}
