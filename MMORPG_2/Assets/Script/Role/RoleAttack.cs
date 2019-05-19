/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-06 13:38:10 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-06 13:38:10 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 角色攻击 </summary>
public class RoleAttack
{
    private RoleFSMMgr m_CurrRoleFSMMgr = null;

    /// <summary> 当前角色控制器 </summary>
    private RoleCtrl m_CurrRoleCtrl = null;

    /// <summary> 物理攻击信息列表 </summary>
    public List<RoleAttackInfo> PhyAttackInfoList;
    /// <summary> 技能攻击信息列表 </summary>
    public List<RoleAttackInfo> SkillAttackInfoList;

    /// <summary> 角色攻击状态 </summary>  
    private RoleStateAttack m_RoleStateAttack;

    /// <summary> 锁定的敌人列表 </summary>
    private List<RoleCtrl> m_EnemyList;

    /// <summary> 查找到的列表 </summary>
    private List<Collider> m_SearchList;

    private int m_NextSkillId;
    public int NextSkillId { get { return m_NextSkillId; } }
    public RoleAttack(RoleFSMMgr fsm)
    {
        m_CurrRoleFSMMgr = fsm;
        m_CurrRoleCtrl = m_CurrRoleFSMMgr.CurrRoleCtrl;
        m_EnemyList = new List<RoleCtrl>();
        m_SearchList = new List<Collider>();
    }
    private RoleAttackInfo GetRoleAttackInfo(RoleAttackType type, int skillId)
    {
        if (type == RoleAttackType.PhyAttack)
        {
            for (int i = 0; i < PhyAttackInfoList.Count; i++)
            {
                if (PhyAttackInfoList[i].SkillId == skillId)
                {
                    return PhyAttackInfoList[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < SkillAttackInfoList.Count; i++)
            {
                if (SkillAttackInfoList[i].SkillId == skillId)
                {
                    return SkillAttackInfoList[i];
                }
            }
        }
        return null;
    }

    public void ToAttack(RoleAttackType type,int skillId)
    {
        if (m_CurrRoleFSMMgr == null || m_CurrRoleFSMMgr.CurrRoleCtrl.IsRigidity)
        {
            if (type == RoleAttackType.SkillAttack)
            {
                m_NextSkillId = skillId;
            }
            return;
        }
        m_NextSkillId = -1;
        //1.角色类型只有玩家和怪才参与技能数值计算

        if (m_CurrRoleCtrl.CurrRoleType == RoleType.MainPlayer || m_CurrRoleCtrl.CurrRoleType == RoleType.Monster)
        {
            //2.获取技能信息
            SkillEntity skillEntity = SkillDBModel.Instance.Get(skillId);
            if (skillEntity == null) return;
            int skillLevel = m_CurrRoleCtrl.CurrRoleInfo.GetSkillLevel(skillId);
            //技能所需魔法值
            int spendMP = skillEntity.SpendMP + skillEntity.SpendMPLevelUp * (skillLevel - 1);
            //3.如果是主角
            if (m_CurrRoleCtrl.CurrRoleType == RoleType.MainPlayer && type == RoleAttackType.SkillAttack)
            {
                if (GlobalInit.Instance.SkillSlotsItem != null)
                {
                    if (spendMP > GlobalInit.Instance.PlayerInfo.CurrMP)
                    {
                        TipsUtil.ShowTextTips(1000504);
                        return;
                    }
                    GlobalInit.Instance.PlayerInfo.CurrMP -=spendMP;
                    GlobalInit.Instance.PlayerInfo.SetSkillCDEndTime(skillId);
                    if (m_CurrRoleCtrl.OnMPChange != null)
                    {
                        m_CurrRoleCtrl.OnMPChange(ValueChnageType.Reduce);
                    }
                    GlobalInit.Instance.SkillSlotsItem.AddTimer();
                    GlobalInit.Instance.SkillSlotsItem = null;
                }
            }
            m_EnemyList.Clear();
            //4.找敌人 如果是主角才找敌人 怪找敌人使用AI
            if (m_CurrRoleCtrl.CurrRoleType == RoleType.MainPlayer)
            {
                int attackTargetCount = skillEntity.AttackTargetCount;
                if (attackTargetCount == 1)
                {
                    #region ------ 单体攻击 ------
                    //单体攻击 必须有锁定敌人
                    if (m_CurrRoleCtrl.LockEnemy != null)
                    {
                        m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
                    }
                    else
                    {
                        m_SearchList.Clear();
                        //没有锁定敌人
                        //发射射线去找 离当前攻击者最近的 就是锁定敌人
                        Collider[] searchList = Physics.OverlapSphere(m_CurrRoleCtrl.transform.position, skillEntity.AreaAttackRadius, 1 << LayerMask.NameToLayer("Role"));
                        if (searchList != null && searchList.Length > 0)
                        {
                            for (int i = 0; i < searchList.Length; i++)
                            {
                                if (searchList[i].GetComponent<RoleCtrl>().CurrRoleType != RoleType.MainPlayer)
                                {
                                    m_SearchList.Add(searchList[i]);
                                }
                            }
                        }
                        m_SearchList.Sort((c1, c2) =>
                        {
                            int ret = 0;
                            if (Vector3.Distance(c1.transform.position, m_CurrRoleCtrl.transform.position) <
                                Vector3.Distance(c2.transform.position, m_CurrRoleCtrl.transform.position))
                            {
                                ret = -1;
                            }
                            else
                            {
                                ret = 1;
                            }
                            return ret;
                        });
                        if (m_SearchList.Count > 0)
                        {
                            m_CurrRoleCtrl.LockEnemy = m_SearchList[0].GetComponent<RoleCtrl>();
                            m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
                        }
                    }
                    #endregion
                }
                else
                {
                    #region ------ 群体攻击 ------

                    int needAttackCount = attackTargetCount;
                    m_SearchList.Clear();
                    //没有锁定敌人
                    //发射射线去找 离当前攻击者最近的 就是锁定敌人
                    Collider[] searchList = Physics.OverlapSphere(m_CurrRoleCtrl.transform.position, skillEntity.AreaAttackRadius, 1 << LayerMask.NameToLayer("Role"));
                    if (searchList != null && searchList.Length > 0)
                    {
                        for (int i = 0; i < searchList.Length; i++)
                        {
                            if (searchList[i].GetComponent<RoleCtrl>().CurrRoleType != RoleType.MainPlayer)
                            {
                                m_SearchList.Add(searchList[i]);
                            }
                        }
                    }
                    m_SearchList.Sort((c1, c2) =>
                    {
                        int ret = 0;
                        if (Vector3.Distance(c1.transform.position, m_CurrRoleCtrl.transform.position) <
                            Vector3.Distance(c2.transform.position, m_CurrRoleCtrl.transform.position))
                        {
                            ret = -1;
                        }
                        else
                        {
                            ret = 1;
                        }
                        return ret;
                    });

                    //群体攻击 如果有锁定敌人 锁定敌人必须是攻击目标之一
                    if (m_CurrRoleCtrl.LockEnemy != null)
                    {
                        m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
                        needAttackCount--;
                        for (int i = 0; i < m_SearchList.Count; i++)
                        {
                            RoleCtrl ctrl = m_SearchList[i].GetComponent<RoleCtrl>();
                            if (ctrl.CurrRoleInfo.RoleId != m_CurrRoleCtrl.CurrRoleInfo.RoleId)
                            {
                                if ((i + 1) > needAttackCount)
                                    break;
                                m_EnemyList.Add(m_SearchList[i].GetComponent<RoleCtrl>());
                            }
                        }
                    }
                    else
                    {
                        if (m_SearchList.Count > 0)
                        {
                            m_CurrRoleCtrl.LockEnemy = m_SearchList[0].GetComponent<RoleCtrl>();
                            for (int i = 0; i < m_SearchList.Count; i++)
                            {
                                RoleCtrl ctrl = m_SearchList[i].GetComponent<RoleCtrl>();
                                if ((i + 1) > needAttackCount)
                                    break;
                                m_EnemyList.Add(ctrl);
                            }
                        }
                    }


                    #endregion
                }
            }
            else if(m_CurrRoleCtrl.CurrRoleType == RoleType.Monster)
            {
                if (m_CurrRoleCtrl.LockEnemy != null)
                {
                    m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
                }
            }
            //Debug.Log(m_EnemyList.Count);
            //5.让敌人受伤
            for (int i = 0; i < m_EnemyList.Count; i++)
            {
                m_EnemyList[i].ToHurt(CalculateHurtValue(m_EnemyList[i],skillEntity));
            }

        }

        #region ------ 动画特效相关 ------
        RoleAttackInfo info = GetRoleAttackInfo(type, skillId);
        if (info == null) return;

        GlobalInit.Instance.StartCoroutine(PlayerEffect(info));
        //EffectManager.Instance.DestroyEffect(trans, info.EffectLifeTime);
        //if (info.IsDoCameraShake && CameraCtrl.Instance != null)
        //{
        //    CameraCtrl.Instance.CameraShake(info.CameraShakeDelay);
        //}

        if (m_RoleStateAttack == null)
            m_RoleStateAttack = m_CurrRoleFSMMgr.GetRoleState(RoleState.Attack) as RoleStateAttack;

        m_RoleStateAttack.AnimatorCondition = type == RoleAttackType.PhyAttack ? "ToPhyAttack" : "ToSkill";
        m_RoleStateAttack.AnimatorConditionValue = info.Index;
        m_RoleStateAttack.AnimatorCurrState = GameUtil.GetRoleAnimatorState(type, info.Index);

        #endregion

        m_CurrRoleFSMMgr.ChangeState(RoleState.Attack);
    }

    private IEnumerator PlayerEffect(RoleAttackInfo info)
    {
        yield return new WaitForSeconds(info.ShowEffectDelay);
        Transform trans = EffectManager.Instance.PlayEffect(info.EffectName);
        //TODO 设置特效位置
        trans.position = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.position;
        trans.rotation = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation;
        EffectManager.Instance.DestroyEffect(trans, info.EffectLifeTime);
        if (info.IsDoCameraShake && CameraCtrl.Instance != null)
        {
            CameraCtrl.Instance.CameraShake(info.CameraShakeDelay);
        }
    }

    /// <summary> 计算攻击信息 </summary>
    /// <param name="enemy"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    private RoleTransferAttackInfo CalculateHurtValue(RoleCtrl enemy,SkillEntity entity)
    {
        if (enemy == null || entity == null) return null;
        int skillLevel = m_CurrRoleCtrl.CurrRoleInfo.GetSkillLevel(entity.Id);
        RoleTransferAttackInfo attackInfo = new RoleTransferAttackInfo();
        attackInfo.AttackRoleId = m_CurrRoleCtrl.CurrRoleInfo.RoleId;
        attackInfo.AttackRolePos = m_CurrRoleCtrl.transform.position;
        attackInfo.BeAttackRoleId = enemy.CurrRoleInfo.RoleId;
        attackInfo.SkillId = entity.Id;
        attackInfo.SkillLevel = skillLevel;
        attackInfo.IsAbnormal = entity.AbnormalState == 1;

        //1.攻击数值 = 攻击者综合战斗力 * (技能伤害倍率 * 0.001f)
        float attackValue = (int)(m_CurrRoleCtrl.CurrRoleInfo.Fighting * (entity.HurtValueRate + entity.HurtValueRateUp * (skillLevel - 1)) * 0.001f);
        //2.基础伤害 = 攻击数值 * 攻击数值/(攻击数值+被攻击者的防御)
        float baseHurt = attackValue * attackValue / (attackValue + enemy.CurrRoleInfo.Defense);
        //3.暴击率 = 攻击方暴击/(被攻击方暴击*5);
        float criValue = m_CurrRoleCtrl.CurrRoleInfo.GetFinalCri() / (float)enemy.CurrRoleInfo.GetFinalCri();
        //暴击率的最大值为50%
        criValue = criValue > 0.5f ? 0.5f : criValue;
        //4.是否暴击
        bool isCri = UnityEngine.Random.Range(0f, 1f) <= criValue;
        //5.暴击伤害倍率 = 暴击？1.5f:1f
        float criHurt = isCri ? 1.5f : 1f;
        //6.随机数 = 0.9f-1.1f
        float random = UnityEngine.Random.Range(0.9f, 1.1f);
        //7.最终伤害 = 基础伤害 * 暴击伤害倍率 * 随机数
        int finalHurt = Mathf.RoundToInt(baseHurt * criHurt * random);
        finalHurt = finalHurt < 1 ? 1 : finalHurt;
        attackInfo.HurtValue = finalHurt;
        attackInfo.IsCri = isCri;

        return attackInfo;
    }
}
