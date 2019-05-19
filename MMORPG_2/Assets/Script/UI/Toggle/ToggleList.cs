/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-11-21 19:23:26 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-11-21 19:23:26 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class ToggleList<T> where T : ToggleItem {

    private List<T> m_ToggleList = new List<T>();
    private List<GameObject> m_DependList=null;
    public int LastSelect = -1;
    public int NowSelect = -1;
    public Action<int> CallBack;
    public int Count { get { return m_ToggleList.Count; } }
    
    public ToggleList()
    {
        
    }
    public ToggleList(List<T> list)
    {
        m_ToggleList = list;
    }
    public ToggleList(List<T> list,List<GameObject> dependList)
    {
        m_ToggleList = list;
        m_DependList = dependList;
    }
    /// <summary> 添加Item </summary>
    /// <param name="t"></param>
    public void Add(T t)
    {
        m_ToggleList.Add(t);
    }
    /// <summary> 获取Item </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T GetValue(int index)
    {
        return m_ToggleList[index];
    }

    /// <summary> 添加关联对象 </summary>
    /// <param name="t"></param>
    public void AddDepend(GameObject go)
    {
        if (m_DependList == null)
            m_DependList = new List<GameObject>();
        m_DependList.Add(go);
    }
    /// <summary> 获取关联对象 </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public GameObject GetDependValue(int index)
    {
        return m_DependList[index];
    }
    /// <summary> 选中Item </summary>
    /// <param name="index"></param>
    public void Select(int index)
    {
        if (NowSelect == index)
            return;
        if (NowSelect != -1)
        {
            LastSelect = NowSelect;
            m_ToggleList[LastSelect].IsSelect(false);
            if (m_DependList != null)
            {
                m_DependList[LastSelect].SetActive(false);
            }
        }
        NowSelect = index;
        m_ToggleList[NowSelect].IsSelect(true);
        if (m_DependList != null)
        {
            m_DependList[NowSelect].SetActive(true);
        }
        if (CallBack != null)
        {
            CallBack(index);
        }
    }

    public void Disponse()
    {
        if (m_ToggleList != null)
        {
            for (int i = 0; i < m_ToggleList.Count; i++)
            {
                m_ToggleList[i] = null;
            }
            m_ToggleList = null;
        }
        if (m_DependList != null)
        {
            for (int i = 0; i < m_DependList.Count; i++)
            {
                m_DependList[i] = null;
            }
            m_DependList = null;
        }
        CallBack = null;
    }
}
