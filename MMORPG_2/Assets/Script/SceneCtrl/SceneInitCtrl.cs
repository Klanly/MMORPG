using UnityEngine;
using System.Collections;
using System;

public class SceneInitCtrl : MonoBehaviour 
{
    private UISceneInitView m_InitView;
    public static SceneInitCtrl Instance;
    private void Awake ()
	{
        //连接服务器
        Instance = this;
        m_InitView = GameObject.Find("UI_Root_Init/Canvas").GetComponent<UISceneInitView>();
    }


    private void Start()
    {
        NetWorkSocket.Instance.ConnectByIP(AppConst.SocketIP, AppConst.IPPort, OnConnectCallBack);
    }
    /// <summary>
    /// 设置进度条的值
    /// </summary>
    /// <param name="text"></param>
    /// <param name="value"></param>
    public void SetProgress(string text, float value)
    {
        m_InitView.TextLoad.text = text;
        m_InitView.SliderLoad.value = value;
        m_InitView.SliderText.text = (value * 100).ToString("0.00") + "%";
        if (value == 1)
            m_InitView.SliderText.text = "100%";
    }
    
    private void OnConnectCallBack()
    {
        DownloadManager.Instance.InitStreamingAssets(OnInitComplete);
    }

    private void OnInitComplete()
    {
        StartCoroutine(LoadLogOn());
    }
    private IEnumerator LoadLogOn()
    {
        yield return new WaitForSeconds(1f);
        SceneMgr.Instance.LoadToLogOn();
    }
}