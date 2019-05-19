using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTipsUtil :Singleton<WindowTipsUtil> {

    private GameObject m_Obj;
    private WindowTips m_WindowTips;
    public void Show(string message, string title = "", int btnCount = 2, Action confirmCallBack = null, Action cancelCallBack = null)
    {
        if (m_Obj == null)
        {
            m_Obj = ResourcesManager.Instance.Load(ResourcesManager.ResourceType.UIWindow, "Pan_Tips", cache: true);
        }
        m_Obj.transform.SetParent(UISceneCtrl.Instance.CurrentUIScene.Container_Center);
        m_Obj.transform.localPosition = Vector3.zero;
        m_Obj.transform.localScale = Vector3.one;
        if (m_WindowTips==null)
        {
            m_WindowTips = m_Obj.GetComponent<WindowTips>();
        }
        m_WindowTips.Show(message, title, btnCount, confirmCallBack, cancelCallBack);
    }
}
