using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkHttp : SingletonMono<NetWorkHttp> {

    /// <summary> Web请求回调 </summary>
    private Action<CallBackArgs> m_CallBack;
    /// <summary> Web请求回调数据 </summary>
    private CallBackArgs m_CallBackArgs;
    public bool IsBusy { get; private set; }
    protected override void OnStart()
    {
        m_CallBackArgs = new CallBackArgs();
    }

    #region SendData 发送Web数据
    /// <summary>
    /// 发送Web数据
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callBack"></param>
    /// <param name="isPost"></param>
    /// <param name="json"></param>
    public void SendData(string url, Action<CallBackArgs> callBack, bool isPost = false, string json = "")
    {
        if (IsBusy) return;
        IsBusy = true;
        m_CallBack = callBack;
        if (isPost==false)
        {
            GetUrl(url);
        }
        else
        {
            PostUrl(url, json);
        }
    }
    #endregion

    #region GetUrl Get请求
    private void GetUrl(string url)
    {
        WWW data = new WWW(url);
        StartCoroutine(Request(data));
    }
    
    #endregion

    #region PostUrl Post请求
    private void PostUrl(string url, string json)
    {
        //定义一个表单
        WWWForm form = new WWWForm();

        form.AddField("", json);
        WWW data = new WWW(url,form);
        StartCoroutine(Request(data));
    }
    #endregion

    #region Request 请求服务器
    private IEnumerator Request(WWW data)
    {
        yield return data;
        IsBusy = false;
        if (string.IsNullOrEmpty(data.error))
        {
            if (data.text == "null")
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.HasError = true;
                    m_CallBackArgs.ErrorMsg = "未请求到数据";
                    m_CallBack(m_CallBackArgs);
                }
            }
            else
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.HasError = false;
                    m_CallBackArgs.Json = data.text;
                    m_CallBack(m_CallBackArgs);
                }
            }
        }
        else
        {
            Debug.LogError("data.error = " + data.error);
            if (m_CallBack != null)
            {
                m_CallBackArgs.HasError = true;
                m_CallBackArgs.ErrorMsg = data.error;
                m_CallBack(m_CallBackArgs);
            }
        }
    }
    #endregion

    #region CallBackArgs Web请求回调
    /// <summary> Web请求回调 </summary>
    public class CallBackArgs : EventArgs
    {
        /// <summary> 是否有错 </summary>
        public bool HasError;

        /// <summary> 错误信息 </summary>
        public string ErrorMsg;

        /// <summary> 返回值 </summary>
        public string Json;
    }
    #endregion
}
