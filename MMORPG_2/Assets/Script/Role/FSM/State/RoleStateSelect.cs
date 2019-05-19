/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-06 11:03:08 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-06 11:03:08 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class RoleStateSelect : RoleStateAbstract {

    /// <summary> 构造函数 </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateSelect(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary> 实现基类 进入状态 </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.ToPose.ToString(), 2);
    }

    /// <summary> 实现基类 执行状态 </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Dali_Selected.ToString()))
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.Dali_Selected);
            //如果动画执行了一遍 就切换待机
            if (CurrRoleAnimatorStateInfo.normalizedTime > 1)
            {
                CurrRoleFSMMgr.CurrRoleCtrl.ToIdle();
            }
        }
    }

    /// <summary> 实现基类 离开状态 </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.ToPose.ToString(), 0);
    }
}
