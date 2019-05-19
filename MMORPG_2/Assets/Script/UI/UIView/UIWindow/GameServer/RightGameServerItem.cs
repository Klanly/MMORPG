using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightGameServerItem : MonoBehaviour {
    
    private Text m_ServerName;
    private int m_ServerId;
    private int m_Index = -1;
    private int m_State = 0;
    private bool m_IsNew = false;
    private string m_Ip = "";
    private int m_Port = 0;
    private Image m_ServerBg;
    private Image m_ServerState;
    private GameObject m_New;

    private Action<int> m_CallBack;

    public bool IsNew
    {
        get
        {
            return m_IsNew;
        }
    }
    public int ServerState
    {
        get
        {
            return m_State;
        }
    }
    public string ServerName
    {
        get
        {
            return m_ServerName.text;
        }
    }
    public int ServerId
    {
        get
        {
            return m_ServerId;
        }
    }
    public string Ip
    {
        get
        {
            return m_Ip;
        }
    }
    public int Port
    {
        get
        {
            return m_Port;
        }
    }

    private void Init()
    {
        m_ServerName = transform.Find("ServerName").GetComponent<Text>();
        m_ServerBg = transform.Find("ServerBg").GetComponent<Image>();
        m_ServerState = transform.Find("ServerState").GetComponent<Image>();
        m_New = transform.Find("New").gameObject;
        EventTriggerListener.Get(m_ServerBg.gameObject).onClick = OnBtnClick;
    }

    private void OnBtnClick(GameObject go)
    {
        if (m_CallBack != null)
            m_CallBack(m_Index);
    }

    public void SetData(int index,GameServerOnePageResponseProto.GameServerOnePageItem item,Action<int> callBack)
    {
        if (m_ServerName == null)
            Init();
        m_Index = index;
        m_ServerId = item.ServerId;
        m_ServerName.text = item.Name;
        m_State = item.Status;
        m_IsNew = item.IsNew;
        m_Ip = item.Ip;
        m_Port = item.Port;
        m_CallBack = callBack;
        m_New.SetActive(m_IsNew);
        //switc
        m_ServerState.color = ColorUtil.GetServerColor(item.Status);
    }
}
