/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-11-22 15:59:29 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-11-22 15:59:29 
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

/// <summary>  </summary>
public class UISelectRoleItem : ToggleItem {

    private Transform m_Trans;
    private Image m_Head;
    private Text m_RoleName;
    private Text m_Level;
    private Text m_Job;
    private Button m_Btn;
    private Action<int> m_CallBack;
    private int m_RoleId;

    public int RoleId
    {
        get
        {
            return m_RoleId;
        }
    }

    private void Init()
    {
        m_Trans = transform;
        m_Head = m_Trans.Find("DescBg/Head").GetComponent<Image>();
        m_RoleName = m_Trans.Find("DescBg/RoleName").GetComponent<Text>();
        m_Level = m_Trans.Find("DescBg/Level").GetComponent<Text>();
        m_Job = m_Trans.Find("DescBg/Job").GetComponent<Text>();
        m_Btn = GetComponent<Button>();
        m_Btn.onClick.AddListener(delegate 
        {
            if (m_CallBack != null)
            {
                m_CallBack(Index);
            }
        });
        m_Trans.DOLocalMoveX(-50, 0.3f).SetAutoKill(false).Pause();
    }

    public void DoPlayForward()
    {
        m_Trans.DOPlayForward();
    }
    public void DOPlayBackwards()
    {
        m_Trans.DOPlayBackwards();
    }

    public void SetData(UISelectRoleItemInfo info,Action<int> callback)
    {
        if (m_Trans == null)
            Init();
        Index = info.Index;
        m_Head.SetSprite(info.AtalsName, info.SpriteName);
        m_RoleId = info.RoleId;
        m_RoleName.text = info.RoleName;
        m_Level.text = info.Level.ToString();
        m_Job.text = info.JobName;
        m_CallBack = callback;
    }
}

public class UISelectRoleItemInfo
{
    public int Index { get; set; }
    public string AtalsName { get; set; }
    public string SpriteName { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public int Level { get; set; }
    public string JobName { get; set; }
}
