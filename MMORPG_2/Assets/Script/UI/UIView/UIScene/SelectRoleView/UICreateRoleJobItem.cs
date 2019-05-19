/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-11-21 19:12:50 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-11-21 19:12:50 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>  </summary>
public class UICreateRoleJobItem : ToggleItem {

    private Button m_Btn;
    private GameObject m_IsSelect;
    private Action<int> m_OnJobCallBack;

    [HideInInspector]
    public Text DescText;
    private void Init()
    {
        m_Btn = GetComponent<Button>();
        m_IsSelect = transform.Find("IsSelect").gameObject;
        m_Btn.onClick.AddListener(delegate 
        {
            if (m_OnJobCallBack != null)
            {
                m_OnJobCallBack(Index);
            }
        });
    }

    public void SetData(int jobId,Text text, Action<int> callBack)
    {
        if (m_Btn == null)
            Init();
        Index = jobId;
        DescText = text;
        m_OnJobCallBack = callBack;
    }

    public override void IsSelect(bool isSelect)
    {
        base.IsSelect(isSelect);
        m_IsSelect.SetActive(isSelect);
    }
}
