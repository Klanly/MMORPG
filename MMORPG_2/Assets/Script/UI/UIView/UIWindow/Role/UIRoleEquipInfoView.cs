/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-12 16:48:24 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-12 16:48:24 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class UIRoleEquipInfoView : MonoBehaviour {

    private Transform m_Trans;
    private Transform m_RoleModel;
    private void Awake()
    {
        m_Trans = transform;
        m_RoleModel = m_Trans.Find("RoleModel");
    }
    // Use this for initialization
    void Start ()
    {
        LoadRole();
	}

    private void LoadRole()
    {
        GameObject obj = RoleManager.Instance.LoadPlayer();
        obj.SetLayer("UI");
        obj.SetParent(m_RoleModel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
