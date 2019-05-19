/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-11-15 20:18:57 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-11-15 20:18:57 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>  </summary>
public class UISceneLoadingView : UISceneBase {

    [SerializeField]
    private Image m_Progress;
    [SerializeField]
    private Text m_ProgressText;

    public void SetProgressValue(float value)
    {
        if (m_Progress == null || m_ProgressText == null) return;
        m_Progress.fillAmount = value;
        m_ProgressText.text = string.Format("{0}%", (int)(value * 100));
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        m_Progress = null;
        m_ProgressText = null;
    }
}
