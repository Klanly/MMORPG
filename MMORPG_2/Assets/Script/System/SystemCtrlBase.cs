using System.Collections;
using UnityEngine;
using System;
public class SystemCtrlBase<T> : IDisposable where T : new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    public virtual void Dispose()
    {

    }

    /// <summary> 添加按钮监听 </summary>
    /// <param name="key"></param>
    /// <param name="handler"></param>
    protected void AddEventListener(string key,DispatcherBase<UIDispatcher,object[],string>.OnActionHandler handler)
    {
        UIDispatcher.Instance.AddEventListener(key, handler);
    }
    /// <summary> 移除按钮监听 </summary>
    /// <param name="key"></param>
    /// <param name="handler"></param>
    protected void RemoveEventListener(string key, DispatcherBase<UIDispatcher, object[], string>.OnActionHandler handler)
    {
        UIDispatcher.Instance.RemoveEventListener(key, handler);
    }
}
