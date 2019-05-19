using UnityEngine;
using System.Collections;
using System;

/// <summary> 怪AI </summary>
public class GameLevelRoleMonsterAI : IRoleAI
{
    /// <summary> 当前角色控制器 </summary>
    public RoleCtrl CurrRole
    {
        get;
        set;
    }

    /// <summary> 下次巡逻时间 </summary>
    private float m_NextPatrolTime = 0f;

    /// <summary> 下次攻击时间 </summary>
    private float m_NextAttackTime = 0f;

    /// <summary> 怪物的信息 </summary>
    private RoleInfoMonster m_Info;

    /// <summary> 使用的技能Id </summary>
    private int m_UsedSkillId;

    private RoleAttackType m_RoleAttackType;

    /// <summary> 要移动到的目标点 </summary>
    private Vector3 m_MoveToPos;

    private RaycastHit m_HitInfo;
    private Vector3 m_RayPoint;

    /// <summary> 下次思考时间 </summary>
    private float m_NextThinkTime = 0f;

    /// <summary> 是否发呆中 </summary>
    private bool m_IsDaze = false;

    public GameLevelRoleMonsterAI(RoleCtrl roleCtrl,RoleInfoMonster info)
    {
        CurrRole = roleCtrl;
        //m_Info = CurrRole.CurrRoleInfo as RoleInfoMonster;
        m_Info = info;
    }

    public void DoAI()
    {

        //当前玩家不存在
        if (GlobalInit.Instance == null || GlobalInit.Instance.CurrPlayer == null) return;

        //怪已死亡
        if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Die || CurrRole.IsRigidity) return;

        if (CurrRole.LockEnemy == null)
        {
            //没有锁定敌人

            //如果是待机状态
            if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Idle)
            {
                if (Time.time > m_NextPatrolTime)
                {
                    m_NextPatrolTime = Time.time + UnityEngine.Random.Range(5f, 10f);
                    m_MoveToPos = new Vector3(CurrRole.BornPoint.x + UnityEngine.Random.Range(CurrRole.PatrolRadius * -1, CurrRole.PatrolRadius), CurrRole.BornPoint.y, CurrRole.BornPoint.z + UnityEngine.Random.Range(CurrRole.PatrolRadius * -1, CurrRole.PatrolRadius));

                    m_RayPoint = new Vector3(m_MoveToPos.x, m_MoveToPos.y + 50, m_MoveToPos.z);
                    if (Physics.Raycast(m_RayPoint, new Vector3(0, -100, 0), out m_HitInfo, 1000f, 1 << LayerMask.NameToLayer("RegionMask")))
                    {
                        return;
                    }

                    //进行巡逻
                    CurrRole.MoveTo(m_MoveToPos);
                }
            }
            
            //如果主角在怪的视野范围内
            if (Vector3.Distance(CurrRole.transform.position, GlobalInit.Instance.CurrPlayer.transform.position) <= CurrRole.ViewRadius)
            {
                //((RoleInfoMonster)CurrRole.CurrRoleInfo).MonsterEntity;
                CurrRole.LockEnemy = GlobalInit.Instance.CurrPlayer;
                m_NextAttackTime = Time.time + m_Info.MonsterEntity.DelaySecAttack;
            }
        }
        else
        {
            //锁定敌人已死亡
            if (CurrRole.LockEnemy.CurrRoleInfo.CurrHP <= 0)
            {
                CurrRole.LockEnemy = null;
                return;
            }

            if (Time.time > m_NextThinkTime + UnityEngine.Random.Range(3, 3.5f))
            {
                //让角色休息
                CurrRole.ToIdle(RoleIdleState.IdleFight);
                m_NextThinkTime = Time.time;
                m_IsDaze = true;
            }
            if (m_IsDaze)
            {
                //如果角色休息中
                if (Time.time > m_NextThinkTime + UnityEngine.Random.Range(1, 1.5f))
                {
                    m_IsDaze = false;
                }
                else
                {
                    return;
                }
            }

            //Debug.Log(CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum.ToString());
            if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum != RoleState.Idle) return;

            //如果有锁定敌人
            //1.如果怪和锁定敌人的距离 超过了怪的视野范围 则取消锁定
            //主角的速度比怪的速度快 超出怪的视野范围则取消锁定
            if (Vector3.Distance(CurrRole.transform.position, GlobalInit.Instance.CurrPlayer.transform.position) > CurrRole.ViewRadius)
            {
                CurrRole.LockEnemy = null;
                return;
            }
            //有锁定敌人 并且在可攻击范围之内
            if (m_Info.MonsterEntity.PhyAttackPro >= UnityEngine.Random.Range(0,100))
            {
                //使用物理攻击
                m_UsedSkillId = m_Info.MonsterEntity.PhyAttackIdArray[UnityEngine.Random.Range(0, m_Info.MonsterEntity.PhyAttackIdArray.Length)];
                m_RoleAttackType = RoleAttackType.PhyAttack;
            }
            else
            {
                //使用技能攻击
                m_UsedSkillId = m_Info.MonsterEntity.SkillAttackIdArray[UnityEngine.Random.Range(0, m_Info.MonsterEntity.SkillAttackIdArray.Length)];
                m_RoleAttackType = RoleAttackType.SkillAttack;
            }

            SkillEntity entity = SkillDBModel.Instance.Get(m_UsedSkillId);
            if (entity == null) return;

            //2.判断敌人是否在此技能的攻击范围内
            if (Vector3.Distance(CurrRole.transform.position, GlobalInit.Instance.CurrPlayer.transform.position) <= entity.AttackRange)
            {
                //让怪朝向锁定敌人
                CurrRole.transform.LookAt(new Vector3(CurrRole.LockEnemy.transform.position.x,CurrRole.transform.position.y,CurrRole.LockEnemy.transform.position.z));
                //攻击
                if (Time.time > m_NextAttackTime && CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum != RoleState.Attack)
                {
                    m_NextAttackTime = Time.time + UnityEngine.Random.Range(0f,1f)+ m_Info.MonsterEntity.DelaySecAttack;
                    CurrRole.ToAttack(m_RoleAttackType, m_UsedSkillId);
                }
            }
            else
            {
                //追逐
                if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Idle)
                {
                    m_MoveToPos = GameUtil.GetRandomPos(CurrRole.transform.position,CurrRole.LockEnemy.transform.position, entity.AttackRange);
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