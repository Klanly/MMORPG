/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-05 18:29:52 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-05 18:29:52 
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

/// <summary> 主界面功能Icon </summary>
public class MainMenuIconItem : MonoBehaviour {

    private Transform m_Trans;
    [HideInInspector]
    public int Row;
    [HideInInspector]
    public int Index;
    private Image m_Icon;
    private Image m_Lock;
    private Image m_RedDot;
    private Text m_Text;
    private string m_OpenWinName;
    private Action<string> m_OnIconClickCallBack;
    [HideInInspector]
    public bool IsPlaying;//动画是否正在播放中
    private void Awake()
    {
        m_Trans = transform;
        m_Icon = m_Trans.Find("Icon").GetComponent<Image>();
        m_Lock = m_Trans.Find("Lock").GetComponent<Image>();
        m_RedDot = m_Trans.Find("RedDot").GetComponent<Image>();
        m_Text = m_Trans.Find("Text").GetComponent<Text>();
        GetComponent<Button>().onClick.AddListener(delegate 
        {
            if (m_OnIconClickCallBack != null)
                m_OnIconClickCallBack(m_OpenWinName);
        });
    }

    public void SetData(MainMenuIconItemInfo info,Action<string> callBack)
    {
        Row = info.Row;
        Index = info.Index;
        m_Icon.SetSprite(info.AtlasName, info.IconName);
        m_Lock.gameObject.SetActive(info.IsLockActive);
        m_RedDot.gameObject.SetActive(info.IsRedDotActive);
        m_Text.text = info.ActivityName;
        m_OpenWinName = info.OpenWinName;
        m_OnIconClickCallBack = callBack;
        m_Trans.DOLocalMove(new Vector3(81, 35, 0), 0.1f * Index).SetAutoKill(false).Pause().OnComplete(()=> 
        {
            gameObject.SetActive(false);
            IsPlaying = false;
        }).OnRewind(()=>
        {
            IsPlaying = false;
        });
    }

    public void DoPlayerForward()
    {
        IsPlaying = true;
        m_Trans.DOPlayForward();
    }
    public void DOPlayBackwards()
    {
        IsPlaying = true;
        gameObject.SetActive(true);
        m_Trans.DOPlayBackwards();
    }
}

public class MainMenuIconItemInfo
{
    public int Row { get; set; }
    public int Index { get; set; }
    public string AtlasName { get; set; }
    public string IconName { get; set; }
    public bool IsLockActive { get; set; }
    public bool IsRedDotActive { get; set; }
    public string ActivityName { get; set; }
    public string OpenWinName { get; set; }
}
