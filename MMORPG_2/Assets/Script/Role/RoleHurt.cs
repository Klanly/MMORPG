/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-06 13:38:21 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-06 13:38:21 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 角色受伤 </summary>
public class RoleHurt
{
    private RoleFSMMgr m_CurrRoleFSMMgr = null;
    public Action OnRoleHurt = null;
    public RoleHurt(RoleFSMMgr fsm)
    {
        m_CurrRoleFSMMgr = fsm;
    }

    /// <summary> 角色受伤 </summary>
    /// <param name="attackInfo"></param>
    public IEnumerator ToHurt(RoleTransferAttackInfo attackInfo)
    {
        if (m_CurrRoleFSMMgr == null) yield break;
        //如果角色已经死亡了 直接返回
        if (m_CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Die) yield break;
        SkillEntity skillEntity = SkillDBModel.Instance.Get(attackInfo.SkillId);
        if (skillEntity == null) yield break;
        
        yield return new WaitForSeconds(skillEntity.ShowHurtEffectDelaySecond);

        //1.减血

        m_CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleInfo.CurrHP -= attackInfo.HurtValue;
        int fontSize = 15;
        Color color = Color.red;
        if (attackInfo.IsCri)
        {
            fontSize = 30;
            color = Color.yellow;
        }
        UISceneCtrl.Instance.CurrentUIScene.HUDText.NewText("- " + attackInfo.HurtValue, m_CurrRoleFSMMgr.CurrRoleCtrl.transform, color, fontSize, 15f, -1f, 2.2f, UnityEngine.Random.Range(0,2)==1? bl_Guidance.RightDown:bl_Guidance.LeftDown);
        if (OnRoleHurt != null)
            OnRoleHurt();
        if (m_CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleInfo.CurrHP <= 0)
        {
            //角色死亡
            m_CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleInfo.CurrHP = 0;
            m_CurrRoleFSMMgr.CurrRoleCtrl.ToDie();

            yield break;
        }
        //2.播放受伤特效
        Transform hurtTrans = EffectManager.Instance.PlayEffect("Effect_Hurt","Common");
        //TODO 设置特效位置
        hurtTrans.position = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.position;
        hurtTrans.rotation = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation;
        EffectManager.Instance.DestroyEffect(hurtTrans, 2f);


        //3.弹出受伤数字 HUDText(包括暴击效果)

        //4.屏幕泛红
        
        //不是僵直状态才播放受伤动画
        if (!m_CurrRoleFSMMgr.CurrRoleCtrl.IsRigidity)
        {
            m_CurrRoleFSMMgr.ChangeState(RoleState.Hurt);
        }
    }
}
