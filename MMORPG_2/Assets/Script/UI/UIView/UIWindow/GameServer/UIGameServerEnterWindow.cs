using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGameServerEnterWindow : UIWindowViewBase
{
    private Transform m_Trans;
    [HideInInspector] public Transform NowServerWin;
    [HideInInspector] public Text NowServerFont;
    [HideInInspector] public Text NowServerText;
    [HideInInspector] public Image Status;
    [HideInInspector] public Text ChangeText;
    [HideInInspector] public Button BtnChange;
    [HideInInspector] public Button ButEnter;
    [HideInInspector] public GameObject Logo;

    [HideInInspector] public Transform ServerSelectWin;
    [HideInInspector] public Transform LeftGrid;
    [HideInInspector] public Transform RightGrid;
    [HideInInspector] public Image NowSelectServerState;
    [HideInInspector] public Text NowSelectServerName;

    
    protected override void OnAwake()
    {
        base.OnAwake();
        m_Trans = transform;
        NowServerWin = m_Trans.Find("NowServerWin");
        NowServerFont = NowServerWin.Find("NowServerBg/NowServerFont").GetComponent<Text>();
        NowServerText = NowServerWin.Find("NowServerBg/NowServerText").GetComponent<Text>();
        Status = NowServerWin.Find("NowServerBg/Status").GetComponent<Image>();
        ChangeText = NowServerWin.Find("NowServerBg/ChangeText").GetComponent<Text>();
        BtnChange = NowServerWin.Find("NowServerBg/ChangeText/BtnChange").GetComponent<Button>();
        ButEnter = NowServerWin.Find("BtnEnter").GetComponent<Button>();
        Logo = NowServerWin.Find("Logo").gameObject;

        ServerSelectWin = m_Trans.Find("ServerSelectWin");
        LeftGrid = ServerSelectWin.Find("LeftBg/LeftView/Viewport/Grid");
        RightGrid = ServerSelectWin.Find("RightBg/OnePageBg/Grid");
        NowSelectServerState = ServerSelectWin.Find("RightBg/ChoseBg/StateBg").GetComponent<Image>();
        NowSelectServerName = ServerSelectWin.Find("RightBg/ChoseBg/Text").GetComponent<Text>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        ServerSelectWin.DOLocalMoveY(0, 0.2f).SetAutoKill(false).Pause();
    }

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "BtnChange":
                UIDispatcher.Instance.Dispatcher(ConstDefine.UIGameServerEnterWindow_BtnChange);
                break;
            case "BtnEnter":
                UIDispatcher.Instance.Dispatcher(ConstDefine.UIGameServerEnterWindow_BtnEnter);
                break;
        }
    }
}
