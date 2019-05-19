using UnityEngine;
using System.Collections;

/// <summary> 待机状态 </summary>
public class RoleStateIdle : RoleStateAbstract
{
    /// <summary> 切换状态时间间隔 </summary>
    private float m_ChangeStep = 15;
    /// <summary> 下次改变休闲状态的时间 </summary>
    private float m_NextChanteTime = 0;

    /// <summary> 是否进入了选中播放Pose的动画(选择角色界面会播放该动画)此处只是为了看着不是单一播放休闲动画 </summary>
    private bool m_IsSelectPose;

    /// <summary> 此状态的运行时间 </summary>
    private float m_RuningTime;

    /// <summary> 构造函数 </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateIdle(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary> 实现基类 进入状态 </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        if (CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleType == RoleType.MainPlayer)
        {
            //主角
            if (CurrRoleFSMMgr.CurrIdleState == RoleIdleState.IdleNormal)
            {
                m_NextChanteTime = Time.time + m_ChangeStep;
                m_IsSelectPose = false;
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToIdleNormal.ToString(), true);
            }
            else
            {
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToIdleFight.ToString(), true);
            }
            m_RuningTime = 0;
        }
        else
        {
            //怪
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToIdleFight.ToString(), true);
        }
    }

    /// <summary> 实现基类 执行状态 </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleType == RoleType.MainPlayer)
        {
            if (!IsChangeOver)
            {
                if (CurrRoleFSMMgr.CurrIdleState == RoleIdleState.IdleNormal)
                {
                    //普通休闲
                    CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
                    if (!m_IsSelectPose)
                    {
                        //在状态机激活的状态名字是否与Idle匹配
                        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Idle.ToString()))
                        {
                            //设置当前动画状态条件的目的是 防止频繁进入相同的动画状态
                            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.Idle);
                            m_RuningTime += Time.deltaTime;
                            if (m_RuningTime > 0.1f)
                                IsChangeOver = true;
                        }
                        else
                        {
                            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), 0);
                        }
                    }
                    else
                    {
                        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Marry.ToString()))
                        {
                            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.Marry);
                            IsChangeOver = true;
                        }
                    }
                }
                else
                {
                    //战斗休闲
                    CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
                    if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Idle_Fight.ToString()))
                    {
                        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.Idle_Fight);
                        m_RuningTime += Time.deltaTime;
                        if (m_RuningTime > 0.1f)
                            IsChangeOver = true;
                    }
                    else
                    {
                        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), 0);
                    }
                }
            }
            if (CurrRoleFSMMgr.CurrIdleState == RoleIdleState.IdleNormal && SceneMgr.Instance.CurrentSceneName != ConstDefine.Scene_SelectRole)
            {
                if (Time.time > m_NextChanteTime)
                {
                    m_NextChanteTime = Time.time + m_ChangeStep;
                    m_IsSelectPose = true;
                    IsChangeOver = false;
                    CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToIdleNormal.ToString(), false);
                    CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.ToPose.ToString(), 4);
                }
                if (m_IsSelectPose)
                {
                    CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
                    if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Marry.ToString()) && CurrRoleAnimatorStateInfo.normalizedTime > 1)
                    {
                        m_IsSelectPose = false;
                        IsChangeOver = false;
                        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.ToPose.ToString(), 0);
                        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToIdleNormal.ToString(), true);
                    }
                }
            }
        }
        else
        {
            //如果是怪
            CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
            if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Idle.ToString()) || CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Idle_Fight.ToString()))
            {
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.Idle_Fight);

                m_RuningTime += Time.deltaTime;
                if (m_RuningTime > 0.1f)
                {
                    IsChangeOver = true;
                }
            }
            else
            {
                //防止怪原地跑
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), 0);
            }
        }
    }
    /// <summary> 实现基类 离开状态 </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        if (CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleType == RoleType.MainPlayer)
        {
            if (CurrRoleFSMMgr.CurrIdleState == RoleIdleState.IdleNormal)
            {
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToIdleNormal.ToString(), false);
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.ToPose.ToString(), 0);
            }
            else
            {
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToIdleFight.ToString(), false);
            }
        }
        else
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToIdleFight.ToString(), false);
        }
    }
}