/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-13 15:36:01 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-13 15:36:01 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 数据传输类 </summary>
public class TransferData
{
    private Dictionary<string, object> m_PutValues;

    /// <summary> 一个int参数的委托 </summary>
    public Action<int> ActionOneIntCallBack = null;

    public object[] ObjArray = null;
    public TransferData()
    {
        m_PutValues = new Dictionary<string, object>();
    }

    public Dictionary<string, object> PutValues
    {
        get
        {
            return m_PutValues;
        }
    }

    /// <summary> 设置数据 </summary>
    /// <typeparam name="TM"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetValue<TM>(string key, TM value)
    {
        PutValues[key] = value;
    }

    /// <summary> 
    /// 获取值 如果报错 
    /// InvalidCastException: Cannot cast from source type to destination type 
    /// 是因为GetValue的类型 与SetValue的类型不同
    /// </summary>
    /// <typeparam name="TM"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public TM GetValue<TM>(string key)
    {
        if (PutValues.ContainsKey(key))
        {
            return (TM)PutValues[key];
        } 
        return default(TM);
    }
}
