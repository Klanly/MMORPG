using UnityEngine;
using System.Collections;
using System;

/// <summary> 死亡状态 </summary>
public class RoleStateDie : RoleStateAbstract
{

    /// <summary> 角色死亡委托 </summary>
    public Action OnDie;
    /// <summary> 角色销毁委托 </summary>
    public Action OnDestroy;

    private float m_BeginDieTime = 0f;

    /// <summary> 构造函数 </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateDie(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToDie.ToString(), true);
        //播放死亡特效特效
        Transform dieTrans = EffectManager.Instance.PlayEffect("Effect_PenXue", "Common");
        //TODO 设置特效位置
        dieTrans.position = CurrRoleFSMMgr.CurrRoleCtrl.transform.position;
        dieTrans.rotation = CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation;
        EffectManager.Instance.DestroyEffect(dieTrans, 3f);
        if (OnDie != null)
            OnDie();
        m_BeginDieTime = 0;
    }

    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (OnDestroy != null)
        {
            m_BeginDieTime += Time.deltaTime;

            if (m_BeginDieTime > AppConst.MonsterDieDestroyTime)
            {
                OnDestroy();
            }
        }


        if (!IsChangeOver)
        {
            CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
            if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Die.ToString()))
            {
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.Die);

                //如果动画执行了一遍 就切换待机
                //if (CurrRoleAnimatorStateInfo.normalizedTime > 1 && CurrRoleFSMMgr.CurrRoleCtrl.OnRoleDie != null)
                //{
                //    CurrRoleFSMMgr.CurrRoleCtrl.OnRoleDie(CurrRoleFSMMgr.CurrRoleCtrl);
                //}
                IsChangeOver = true;
            }
        }
    }

    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToDie.ToString(), false);
    }
}