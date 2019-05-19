/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-20 11:37:04 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-20 11:37:04 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary> 战斗结算界面 </summary>
public class UIFightResultWindow : UIWindowViewBase
{
    private Transform m_Trans;
    private Image m_ImgResult;
    private RawImage m_BgRole;
    private Text m_TextPassTime;
    private Text m_TextExp;
    private Text m_TextCoin;
    private Transform m_StarParent;
    private GameObject m_BtnResurgence;

    public Action OnRoleResurgenceCallBack;
    protected override void OnAwake()
    {
        base.OnAwake();
        m_Trans = transform;
        m_ImgResult = m_Trans.Find("ImgResult").GetComponent<Image>();
        m_BgRole = m_Trans.Find("BgRole").GetComponent<RawImage>();
        m_TextPassTime = m_Trans.Find("PassTimeValue").GetComponent<Text>();
        m_TextExp = m_Trans.Find("ImgExp/ExpValue").GetComponent<Text>();
        m_TextCoin = m_Trans.Find("ImgCoin/CoinValue").GetComponent<Text>();
        m_StarParent = m_Trans.Find("StarParent");
        m_BtnResurgence = m_Trans.Find("BtnResurgence").gameObject;
    }

    protected override void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "BtnReturn":
                SceneMgr.Instance.LoadToWorldMap(PlayerCtrl.Instance.LastInWorldMapId);
                GameUtil.EnterToCity();
                break;
            case "BtnResurgence":
                if (OnRoleResurgenceCallBack != null) OnRoleResurgenceCallBack();
                break;
        }
    }

    public void SetData(TransferData data)
    {
        int result = data.GetValue<int>(ConstDefine.FightValue);
        if (result == 0)
        {
            m_ImgResult.SetSprite("GameLevel", "FailTitle");
            m_BgRole.SetGray();
        }
        m_BtnResurgence.SetActive(result == 0);
        m_TextPassTime.SetText((data.GetValue<float>(ConstDefine.UseTime)).ToString("f0")+"秒", true, 0.2f, DG.Tweening.ScrambleMode.Numerals);
        m_TextExp.SetText("+" + data.GetValue<int>(ConstDefine.ExpValue), true, 0.2f, DG.Tweening.ScrambleMode.Numerals);
        m_TextCoin.SetText("+" + data.GetValue<int>(ConstDefine.CoinValue), true, 0.2f, DG.Tweening.ScrambleMode.Numerals);
        GameUtil.LoadStar(m_StarParent, data.GetValue<int>(ConstDefine.CurrStar), data.GetValue<int>(ConstDefine.MaxStar), 3, 0.3f);
    }
}
