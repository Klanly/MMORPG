/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-05 21:59:50 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-05 21:59:50 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>  </summary>
public class MainMenuAndMapView : MonoBehaviour {

    private Transform m_Trans;
    private Button m_ShowOrHideBtn;
    private GameObject m_Show;
    private GameObject m_Hide;
    private Action m_ShowOrHideCallBack;
    [HideInInspector]
    public MainMenuView MainMenuIconView;
    public static MainMenuAndMapView Instance;
    private void Awake()
    {
        Instance = this;
        m_Trans = transform;
        m_ShowOrHideBtn = m_Trans.Find("ShowOrHide").GetComponent<Button>();
        m_Show = m_ShowOrHideBtn.transform.Find("Show").gameObject;
        m_Hide = m_ShowOrHideBtn.transform.Find("Hide").gameObject;
        MainMenuIconView = transform.Find("MainMenuView").GetComponent<MainMenuView>();
    }
    // Use this for initialization
    void Start ()
    {
        m_ShowOrHideBtn.onClick.AddListener(delegate 
        {
            if (m_ShowOrHideCallBack != null)
                m_ShowOrHideCallBack();
        });
	}

    public void SetMainMenu(List<MainMenuIconItem> itemList,Action callBack)
    {
        MainMenuIconView.SetMainMenu(itemList);
        m_ShowOrHideCallBack = callBack;
    }

    public void IsShow(bool isShow)
    {
        m_Show.SetActive(isShow);
        m_Hide.SetActive(!isShow);
    }
}
