using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary> 主角主城AI </summary>
public class RoleMainPlayerCityAI : IRoleAI
{
    public RoleCtrl CurrRole
    {
        get;
        set;
    }

    public RoleMainPlayerCityAI(RoleCtrl roleCtrl)
    {
        CurrRole = roleCtrl;
        m_SearchList = new List<Collider>();
    }

    private int m_PhyIndex = 0;

    /// <summary> 要移动到的目标点 </summary>
    private Vector3 m_MoveToPos;

    private RaycastHit m_HitInfo;
    private Vector3 m_RayPoint;

    //private bool IsFightFinish = false;

    /// <summary> 查找到的列表 </summary>
    private List<Collider> m_SearchList;
    public void DoAI()
    {
        //执行AI
        if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Die) return;
        if (GlobalInit.Instance.IsAutoFight)
        {
            AutoFightState();
        }
        else
        {
            NormalFightState();
        }
    }

    private void AutoFightState()
    {
        //如果当前区域已经没有怪了
        if (!GameLevelSceneCtrl.Instance.IsCurrRegionHaveMosnter)
        {
            if (GameLevelSceneCtrl.Instance.IsCurrRegionIsLast)
            {
                //如果已经是最后一个区域了
                CurrRole.LockEnemy = null;
                GlobalInit.Instance.IsAutoFight = false;
                return;
            }
            CurrRole.MoveTo(GameLevelSceneCtrl.Instance.NextRegionPlayerBornPos);
            //进入下一区域
        }
        else
        {
            //如果没有锁定敌人
            if (CurrRole.LockEnemy == null)
            {
                m_SearchList.Clear();
                //没有锁定敌人
                //发射射线去找 离当前攻击者最近的 就是锁定敌人
                Collider[] searchList = Physics.OverlapSphere(CurrRole.transform.position,1000f, 1 << LayerMask.NameToLayer("Role"));
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
                    if (Vector3.Distance(c1.transform.position, CurrRole.transform.position) <
                        Vector3.Distance(c2.transform.position, CurrRole.transform.position))
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
                    CurrRole.LockEnemy = m_SearchList[0].GetComponent<RoleCtrl>();
                    //m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
                }

                //根据视野范围搜索附近的怪
                //找最近的怪当锁定敌人
            }
            else
            {
                

                //有锁定敌人

                //锁定敌人已死亡
                if (CurrRole.LockEnemy.CurrRoleInfo.CurrHP <= 0)
                {
                    CurrRole.LockEnemy = null;
                    return;
                }
                if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum != RoleState.Idle)
                {
                    return;
                }

                int skillid = CurrRole.CurrRoleInfo.GetCanUseSkillId();
                RoleAttackType type;
                //定义要使用的技能id和类型
                if (skillid > 0)
                {
                    type = RoleAttackType.SkillAttack;
                    //技能攻击
                    
                }
                else
                {
                    type = RoleAttackType.PhyAttack;
                    skillid = CurrRole.CurrRoleInfo.PhySkillDic[m_PhyIndex].SkillId;
                    //物理攻击
                }

                SkillEntity entity = SkillDBModel.Instance.Get(skillid);
                if (entity == null) return;

                //.判断敌人是否在此技能的攻击范围内
                if (Vector3.Distance(CurrRole.LockEnemy.transform.position, GlobalInit.Instance.CurrPlayer.transform.position) <= entity.AttackRange)
                {
                    //发起攻击
                    //CurrRole.ToAttack(type, skillid);
                    if (type == RoleAttackType.SkillAttack)
                    {
                        GlobalInit.Instance.OnSkillClick(skillid);
                    }
                    else
                    {
                        CurrRole.ToAttack(RoleAttackType.PhyAttack, skillid);
                    }
                }
                else
                {
                    //追逐
                    if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Idle)
                    {
                        m_MoveToPos = GameUtil.GetRandomPos(CurrRole.transform.position, CurrRole.LockEnemy.transform.position, entity.AttackRange);
                        m_RayPoint = new Vector3(m_MoveToPos.x, m_MoveToPos.y + 50, m_MoveToPos.z);
                        if (Physics.Raycast(m_RayPoint, new Vector3(0, -100, 0), out m_HitInfo, 1000f, 1 << LayerMask.NameToLayer("RegionMask")))
                        {
                            return;
                        }
                        CurrRole.MoveTo(m_MoveToPos);
                    }
                }

            }
        }

        
    }

    private void NormalFightState()
    {
        if (CurrRole.LastFightTime != 0)
        {
            if (Time.time > CurrRole.LastFightTime + 30f)
            {
                CurrRole.ToIdle();
                CurrRole.LastFightTime = 0;
            }
        }

        //1.如果有锁定敌人 就行攻击
        if (CurrRole.LockEnemy != null)
        {
            if (CurrRole.LockEnemy.CurrRoleInfo.CurrHP <= 0)
            {
                CurrRole.LockEnemy = null;
                return;
            }
            if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Idle)
            {
                if (CurrRole.Attack.NextSkillId > 0)
                {
                    CurrRole.ToAttack(RoleAttackType.SkillAttack, CurrRole.Attack.NextSkillId);
                }
                else
                {
                    CurrRole.ToAttack(RoleAttackType.PhyAttack, CurrRole.CurrRoleInfo.PhySkillDic[m_PhyIndex].SkillId);
                    m_PhyIndex++;
                    if (m_PhyIndex >= CurrRole.CurrRoleInfo.PhySkillDic.Count)
                    {
                        m_PhyIndex = 0;
                    }
                }
            }
        }
    }
}