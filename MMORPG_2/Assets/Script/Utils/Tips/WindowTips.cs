using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary> 窗口提示 </summary>
public class WindowTips : UIWindowViewBase {

    private Transform m_Trans;
    private Text m_Title;
    private Text m_Message;
    private Button m_Confirm;
    private Button m_Cancel;

    private Action m_ConfirmCallBack;
    private Action m_CancelCallBack;
    private void Awake()
    {
        m_Trans = transform;
        m_Title = m_Trans.Find("Title").GetComponent<Text>();
        m_Message = m_Trans.Find("Message").GetComponent<Text>();
        m_Confirm = m_Trans.Find("BtnContainer/Confirm").GetComponent<Button>();
        m_Cancel = m_Trans.Find("BtnContainer/Cancel").GetComponent<Button>();
    }

    private void Start()
    {
        EventTriggerListener.Get(m_Confirm.gameObject).onClick = OnConfirmClick;
        EventTriggerListener.Get(m_Cancel.gameObject).onClick = OnCancelClick;
    }
    
    private void OnConfirmClick(GameObject go)
    {
        if (m_ConfirmCallBack != null) m_ConfirmCallBack();
        Close();
    }
    private void OnCancelClick(GameObject go)
    {
        if (m_CancelCallBack != null) m_CancelCallBack();
        Close();
    }

    private new void Close()
    {
        m_Trans.localPosition = new Vector2(0, 5000);
    }

    /// <summary> 显示提示窗口 </summary>
    /// <param name="message">提示信息</param>
    /// <param name="title">标题</param>
    /// <param name="btnCount">按钮个数(1.只有确定按钮，2.确定和取消)</param>
    /// <param name="confirmCallBack">确定回调</param>
    /// <param name="cancelCallBack">取消回调</param>
    public void Show(string message,string title = "",int btnCount = 2,Action confirmCallBack = null,Action cancelCallBack = null)
    {
        switch (btnCount)
        {
            case 1:
                m_Cancel.gameObject.SetActive(false);
                m_Confirm.transform.localPosition = Vector2.zero;
                break;
            default:
                m_Cancel.gameObject.SetActive(true);
                m_Confirm.transform.localPosition = new Vector2(-165, 0);
                break;
        }
        m_Trans.localPosition = Vector2.zero;
        m_Title.text = title;
        m_Message.text = message;
        m_ConfirmCallBack = confirmCallBack;
        m_CancelCallBack = cancelCallBack;
    }
}
