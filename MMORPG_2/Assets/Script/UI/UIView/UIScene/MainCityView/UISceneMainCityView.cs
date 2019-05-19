/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-04 00:36:19 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-04 00:36:19 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class UISceneMainCityView : UISceneViewBase {

    private Transform m_Trans;
    /// <summary> 1-6技能槽父对象 </summary>
    [HideInInspector]
    public Transform SkillListTrans;
    public Action<GameObject> BtnCallBack;

    /// <summary> 物理攻击技能槽 </summary>
    private UISkillSlotsItem m_PhySlotsItem;

    /// <summary> 角色技能槽列表(0号索引为物理攻击技能槽  7号为被动技能槽  1-6为主动技能槽) </summary>  
    public List<UISkillSlotsItem> SkillSlotsItemList;

    private Transform m_AutoFightTrans;
    private GameObject m_NormalFight;
    private GameObject m_AutoFight;

    private bool m_IsAutoFight = false; 
    protected override void OnAwake()
    {
        base.OnAwake();
        m_Trans = transform;
        SkillListTrans = m_Trans.Find("Canvas/Container_RightBottom/ActiveSkills/SkillList");
        m_AutoFightTrans = m_Trans.Find("Canvas/Container_RightBottom/AutoFight");
        m_NormalFight = m_Trans.Find("Canvas/Container_RightBottom/AutoFight/NormalFight").gameObject;
        m_AutoFight = m_Trans.Find("Canvas/Container_RightBottom/AutoFight/AutoFight").gameObject;
    }

    protected override void OnStart()
    {
        base.OnStart();
        if (OnLoadComplete != null)
        {
            OnLoadComplete();
        }
        m_AutoFightTrans.gameObject.SetActive(SceneMgr.Instance.CurrSceneType == ConstDefine.GameLevel);
        m_IsAutoFight = false;
    }
    protected override void OnBtnClick(GameObject go)
    {
        if (BtnCallBack != null)
        {
            BtnCallBack(go);
        }
    }

    public bool AutoFight()
    {
        m_IsAutoFight = !m_IsAutoFight;
        m_NormalFight.SetActive(!m_IsAutoFight);
        m_AutoFight.SetActive(m_IsAutoFight);
        return m_IsAutoFight;
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
    }
}
