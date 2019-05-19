using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftGameServerItem : MonoBehaviour {


    private int m_PageIndex = 0;
    private Text m_ServerName;
    private Button m_Btn;
    private GameObject m_SelectBg;
    private Action<int> m_CallBack;
    private void Init()
    {
        m_ServerName = transform.Find("ServerName").GetComponent<Text>();
        m_Btn = transform.Find("ServerBg").GetComponent<Button>();
        m_SelectBg = transform.Find("SelectBg").gameObject;
        EventTriggerListener.Get(m_Btn.gameObject).onClick = OnBtnClick;
    }

    private void OnBtnClick(GameObject go)
    {
        if (m_CallBack != null)
            m_CallBack(m_PageIndex);
    }

    public void SetData(int pageIndex,string serverName,Action<int> callBack)
    {
        if (m_ServerName == null)
            Init();
        m_PageIndex = pageIndex;
        m_ServerName.text = serverName;
        m_CallBack = callBack;
    }

    public void IsSelect(bool value)
    {
        m_SelectBg.SetActive(value);
    }
}
