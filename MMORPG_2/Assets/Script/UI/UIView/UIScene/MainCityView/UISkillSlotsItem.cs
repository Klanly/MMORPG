/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-10 16:59:24 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-10 16:59:24 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary> 角色技能槽 </summary>
public class UISkillSlotsItem : MonoBehaviour {

    [SerializeField]
    /// <summary> 技能槽编号 </summary>
    private int m_SlotsNode;
    /// <summary> 技能等级 </summary>
    private int m_SkillLevel;
    /// <summary> 技能Id </summary>
    private int m_SkillId;
    /// <summary> 技能冷却时间 </summary>
    private float m_SkillCD;

    /// <summary> 技能是否处于激活状态 </summary>
    private bool m_IsActive;

    public bool IsActive { get { return m_IsActive; } }

    public int SkillId { get { return m_SkillId; } }

    [SerializeField]
    private Image m_Icon;
    [SerializeField]
    private Image m_CD;
    [SerializeField]
    private Text m_LeftTimeText;

    private float m_LeftTime;

    public float LeftTime { get { return m_LeftTime; } }

    public void SetData(TransferData data)
    {
        m_SkillId = data.GetValue<int>(ConstDefine.SkillId);
        m_Icon.SetSprite(data.GetValue<string>(ConstDefine.FolderName), data.GetValue<string>(ConstDefine.IconName));
        m_SkillId = data.GetValue<int>(ConstDefine.SkillId);
        m_SkillCD = data.GetValue<float>(ConstDefine.SkillCD);
        m_IsActive = data.GetValue<bool>(ConstDefine.IsActive);
    }

    public void SetGray(bool isGray = false)
    {
        if (m_Icon == null)
            return;
        m_Icon.SetGray(isGray);
        if(m_CD != null)
            m_CD.gameObject.SetActive(false);
        if (m_LeftTimeText!= null)
            m_LeftTimeText.gameObject.SetActive(false);
    }

    public void AddTimer()
    {
        m_LeftTime = m_SkillCD;
        if (m_CD != null)
        {
            m_CD.gameObject.SetActive(true);
            m_CD.fillAmount = 1;
        }
        if (m_LeftTimeText != null)
        {
            m_LeftTimeText.gameObject.SetActive(true);
            m_LeftTimeText.text = ((int)m_LeftTime).ToString();
        }
        int count = (int)m_LeftTime;
        FrameTimerManager.Instance.Add("SkillLeftTime" + SkillId, 1, OnTimer, count);
    }

    private void OnTimer()
    {
        m_LeftTime -= 1;
        m_CD.fillAmount = m_LeftTime / m_SkillCD;
        if (m_LeftTime<=0)
        {
            m_LeftTime=0;
            if (m_CD != null)
                m_CD.gameObject.SetActive(false);
            if (m_CD != null)
                m_LeftTimeText.gameObject.SetActive(false);
        }
        if (m_CD != null)
            m_LeftTimeText.text = ((int)m_LeftTime).ToString();
    }

    private void OnDestroy()
    {
        FrameTimerManager.Instance.RemoveCallback(OnTimer);
    }
}
