using UnityEngine;
using System.Collections;

/// <summary>
/// UI层级管理器
/// </summary>
public class LayerManager : Singleton<LayerManager> 
{
    /// <summary>
    /// UIPanel层级深度
    /// </summary>
    private int m_UIViewLayer = 50;

    /// <summary>
    /// 重置
    /// </summary>
    public void Reset()
    {
        m_UIViewLayer = 50;
    }

    /// <summary>
    /// 检查窗口数量 如果没有打开的窗口 重置
    /// </summary>
    public void CheckOpenWindow()
    {
        m_UIViewLayer--;
        if (WindowUtil.Instance.OpenWindowCount == 0)
        {
            Reset();
        }
    }

    /// <summary>
    /// 设置层级
    /// </summary>
    public void SetLayer(GameObject obj)
    {
        m_UIViewLayer++;
        Canvas canvas = obj.GetComponent<Canvas>();
        canvas.sortingOrder = m_UIViewLayer;
    }
}