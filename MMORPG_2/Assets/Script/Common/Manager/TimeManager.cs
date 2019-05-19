/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-19 21:00:12 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-19 21:00:12 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 时间管理器 </summary>
public class TimeManager : SingletonMono<TimeManager>
{
    /// <summary> 是否播放中 </summary>
    private bool m_IsTimeScale;

    /// <summary> 时间缩放结束时间 </summary>
    private float m_TimeScaleEndTIme = 0f;

    private Action m_CallBack;

    /// <summary> 修改时间缩放 </summary>
    /// <param name="toTimeScale">缩放的值</param>
    /// <param name="durationTime">持续时间</param>
    public void ChangeTimeScale(float toTimeScale,float durationTime,Action callBack = null)
    {
        m_IsTimeScale = true;
        Time.timeScale = toTimeScale;
        m_CallBack = callBack;
        m_TimeScaleEndTIme = Time.realtimeSinceStartup + durationTime;
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (m_IsTimeScale)
        {
            if (Time.realtimeSinceStartup > m_TimeScaleEndTIme)
            {
                Time.timeScale = 1;
                m_IsTimeScale = false;
                if (m_CallBack != null)
                {
                    m_CallBack();
                    m_CallBack = null;
                }
            }
        }
    }
}
