using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UIWindowViewBase : UIViewBase {

    /// <summary> 挂点类型 </summary>
    [SerializeField]
    public WindowUIContainerType containerType = WindowUIContainerType.Center;

    /// <summary> 打开方式 </summary>
    [SerializeField]
    public WindowShowStyle showStyle = WindowShowStyle.Normal;

    /// <summary> 动画曲线 </summary>
    [SerializeField]
    public Ease TweenEase = Ease.Linear;
    /// <summary> 打开或关闭动画效果持续时间 </summary>
    [SerializeField]
    public float duration = 0.2f;

    /// <summary> 当前窗口类型 </summary>
    [HideInInspector]
    public string CurrentUIType;

    private string m_NextOpenWinName;

    public Tweener Tweener { get; set; }
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        if (go.name.Equals("Btn_Close", System.StringComparison.CurrentCultureIgnoreCase))
        {
            Close();
        }
    }
    /// <summary> 关闭界面 </summary>
    public virtual void Close()
    {
        WindowUtil.Instance.CloseWindow(CurrentUIType);
    }

    /// <summary> 关闭并打开下一个界面 </summary>
    /// <param name="nextWinName">下一个界面类型</param>
    public virtual void CloseAndOpenNext(string nextWinName)
    {
        this.Close();
        m_NextOpenWinName = nextWinName;
    }

    /// <summary>
    /// 销毁之前执行
    /// </summary>
    protected override void BeforeOnDestroy()
    {
        LayerManager.Instance.CheckOpenWindow();
        if (!string.IsNullOrEmpty(m_NextOpenWinName))
        {
            WindowManager.Instance.OpenWindow(m_NextOpenWinName);
        }
    }
}
