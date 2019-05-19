/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-08 16:03:12 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-08 16:03:12 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class NPCCtrl : MonoBehaviour {

    /// <summary> 昵称挂点 </summary>
    private Transform m_HeadBarTrans;
    /// <summary> 头顶UI条 </summary>
    private GameObject m_HeadBar;

    private NPCDataEntity m_CurrNPCDataEntity;
    void Start ()
    {
        m_HeadBarTrans = new GameObject("HeadBarTrans").transform;
        m_HeadBarTrans.SetParent(transform);
        m_HeadBarTrans.transform.localPosition = Vector3.zero + new Vector3(0, GetComponent<CapsuleCollider>().height + 0.2f, 0);
        InitHeadBar();

        transform.position = new Vector3(m_CurrNPCDataEntity.RoleBirthPos[0], m_CurrNPCDataEntity.RoleBirthPos[1], m_CurrNPCDataEntity.RoleBirthPos[2]);
        transform.eulerAngles = new Vector3(0, m_CurrNPCDataEntity.RoleBirthPos[3], 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary> 初始化 </summary>
    /// <param name="currNPCDataEntity">NPC数据实体</param>
    public void Init(NPCDataEntity currNPCDataEntity)
    {
        m_CurrNPCDataEntity = currNPCDataEntity;
    }
    /// <summary>
    /// 初始化头顶UI条
    /// </summary>
    private void InitHeadBar()
    {
        if (RoleHeadBarRoot.Instance == null) return;
        //克隆预设
        m_HeadBar = ResourcesManager.Instance.Load(ResourcesManager.ResourceType.UIOther, "NPCHeadBar");
        m_HeadBar.transform.SetParent(RoleHeadBarRoot.Instance.gameObject.transform);
        m_HeadBar.transform.localScale = Vector3.one;


        NPCHeadBarView npcHeadBarView = m_HeadBar.GetComponent<NPCHeadBarView>();

        //给预设赋值
        npcHeadBarView.Init(m_HeadBarTrans, m_CurrNPCDataEntity);
    }
}
