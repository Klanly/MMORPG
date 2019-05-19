/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-24 15:37:47 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-24 15:37:47 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>  </summary>
public class UIGameLevelItem : MonoBehaviour {

    private Transform m_Trans;
    private Text m_Name;
    private Transform m_StarContainer;
    private Image m_IsBoss;
    private GameObject m_Select;

    private int m_Index = 0;
    public int Index { get { return m_Index; } }
    private Action<int> m_CallBack;
    public void Init()
    {
        if (m_Trans == null)
        {
            m_Trans = transform;
            m_Name = m_Trans.Find("Name").GetComponent<Text>();
            m_StarContainer = m_Trans.Find("StarContainer");
            m_IsBoss = m_Trans.Find("IsBoss").GetComponent<Image>();
            m_Select = m_Trans.Find("Select").gameObject;
            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        if (m_CallBack != null)
            m_CallBack(Index);
    }

    public void SetData(TransferData data)
    {
        m_Index = data.GetValue<int>(ConstDefine.Index);
        m_Name.text = data.GetValue<string>(ConstDefine.Name);
        GameUtil.LoadStar(m_StarContainer, data.GetValue<int>(ConstDefine.CurrStar), data.GetValue<int>(ConstDefine.MaxStar), data.GetValue<int>(ConstDefine.RowStarCount));
        m_IsBoss.SetSprite("Icon", data.GetValue<bool>(ConstDefine.IsBoss)? "Button_Fightauto1" : "Button_Fightauto");
        m_CallBack = data.ActionOneIntCallBack;
    }
    public void IsSelect(bool value)
    {
        m_Select.SetActive(value);
    }
}
