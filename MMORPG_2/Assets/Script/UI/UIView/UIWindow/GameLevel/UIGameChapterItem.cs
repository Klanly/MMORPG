/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-14 15:21:05 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-14 15:21:05 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>  </summary>
public class UIGameChapterItem : MonoBehaviour
{
    private Transform m_Trans;
    private Image m_Bg;
    private Text m_ChapterName;
    private Text m_LevelProgress;
    private Text m_StarProgeress;
    private GameObject m_Lock;

    private int m_Index = 0;

    public int Index { get { return m_Index; } }

    private Action<int> m_CallBack;
    public void Init()
    {
        m_Trans = transform;
        m_Bg = GetComponent<Image>();
        m_ChapterName = m_Trans.Find("ChapterName").GetComponent<Text>();
        m_LevelProgress = m_Trans.Find("LevelProgress").GetComponent<Text>();
        m_StarProgeress = m_Trans.Find("StarProgers").GetComponent<Text>();
        m_Lock = m_Trans.Find("Lock").gameObject;
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (m_CallBack != null)
        {
            m_CallBack(Index);
        }
    }

    public void SetData(TransferData data)
    {
        m_Index = data.GetValue<int>(ConstDefine.Index);
        m_Bg.SetBg(data.GetValue<string>(ConstDefine.FolderName), data.GetValue<string>(ConstDefine.IconName));
        m_Bg.SetGray(!data.GetValue<bool>(ConstDefine.IsOpen));
        m_ChapterName.text = data.GetValue<string>(ConstDefine.Name);
        m_StarProgeress.text = string.Format("{0}/{1}", data.GetValue<int>(ConstDefine.CurrStar), data.GetValue<int>(ConstDefine.MaxStar));
        m_LevelProgress.text = string.Format("{0}/{1}",data.GetValue<int>(ConstDefine.CurrProgeress),data.GetValue<int>(ConstDefine.MaxProgeress));
        m_Lock.SetActive(!data.GetValue<bool>(ConstDefine.IsOpen));
        m_CallBack = data.ActionOneIntCallBack;
    }
}
