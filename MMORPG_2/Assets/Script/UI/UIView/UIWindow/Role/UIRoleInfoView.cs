/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-13 13:03:06 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-13 13:03:06 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary> 角色信息视图类 </summary>
public class UIRoleInfoView : MonoBehaviour {

    private Transform m_BasicBg;
    private Text m_Finghting;
    private Text m_NickName;
    private Text m_VipLevel;
    private Text m_PapalName;
    private Text m_Attack;
    private Text m_Defence;
    private Text m_Hit;
    private Text m_Dodge;
    private Text m_Cri;
    private Text m_Res;
    private Text m_HP;
    private Text m_MP;
    private Text m_Exp;
    private Image m_HPBar;
    private Image m_MPBar;
    private Image m_ExpBar;
    private void Awake()
    {
        Transform m_BasicBg = transform.Find("BasicBg");
        m_Finghting = m_BasicBg.Find("FightRedBg/FightValue").GetComponent<Text>();
        m_NickName = m_BasicBg.Find("PlayerName/Name").GetComponent<Text>();
        m_VipLevel = m_BasicBg.Find("VIP/VIPValue").GetComponent<Text>();
        m_PapalName = m_BasicBg.Find("PapalName/Name").GetComponent<Text>();
        m_Attack = m_BasicBg.Find("AttackValue").GetComponent<Text>();
        m_Defence = m_BasicBg.Find("DefenceValue").GetComponent<Text>();
        m_Hit = m_BasicBg.Find("HitValue").GetComponent<Text>();
        m_Dodge = m_BasicBg.Find("DodgeValue").GetComponent<Text>();
        m_Cri = m_BasicBg.Find("CriValue").GetComponent<Text>();
        m_Res = m_BasicBg.Find("ResValue").GetComponent<Text>();
        m_HP = m_BasicBg.Find("HPBar/Value").GetComponent<Text>();
        m_MP = m_BasicBg.Find("MPBar/Value").GetComponent<Text>();
        m_Exp = m_BasicBg.Find("ExpBar/Value").GetComponent<Text>();
        m_HPBar = m_BasicBg.Find("HPBar/HPBarValue").GetComponent<Image>();
        m_MPBar = m_BasicBg.Find("MPBar/MPBarValue").GetComponent<Image>();
        m_ExpBar = m_BasicBg.Find("ExpBar/ExpBarValue").GetComponent<Image>();
    }

    public void SetData(TransferData data)
    {
        m_Finghting.text = data.GetValue<int>(ConstDefine.FinalFighting).ToString();
        m_NickName.text = data.GetValue<string>(ConstDefine.NickName);
        m_VipLevel.text = data.GetValue<int>(ConstDefine.VIPLevel).ToString();
        m_PapalName.text = data.GetValue<string>(ConstDefine.PapalName);
        m_Attack.text = data.GetValue<int>(ConstDefine.FinalAttack).ToString();
        m_Defence.text = data.GetValue<int>(ConstDefine.FinalDefense).ToString();
        m_Hit.text = data.GetValue<int>(ConstDefine.FinalHit).ToString();
        m_Dodge.text = data.GetValue<int>(ConstDefine.FinalDodge).ToString();
        m_Cri.text = data.GetValue<int>(ConstDefine.FinalCri).ToString();
        m_Res.text = data.GetValue<int>(ConstDefine.FinalRes).ToString();
        m_HP.text = string.Format("{0}/{1}", data.GetValue<int>(ConstDefine.CurrHP), data.GetValue<int>(ConstDefine.MaxHP));
        m_MP.text = string.Format("{0}/{1}", data.GetValue<int>(ConstDefine.CurrMP), data.GetValue<int>(ConstDefine.MaxMP));
        m_Exp.text = string.Format("{0}/{1}", data.GetValue<int>(ConstDefine.CurrExp), data.GetValue<int>(ConstDefine.MaxExp));
        m_HPBar.fillAmount = data.GetValue<int>(ConstDefine.CurrHP) / (float)data.GetValue<int>(ConstDefine.MaxHP);
        m_MPBar.fillAmount = data.GetValue<int>(ConstDefine.CurrMP) / (float)data.GetValue<int>(ConstDefine.MaxMP);
        m_ExpBar.fillAmount = data.GetValue<int>(ConstDefine.CurrExp)  / (float)data.GetValue<int>(ConstDefine.MaxExp);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class UIRoleInfo
{
    public int Finghting { get; set; }
    public string NickName { get; set; }
    public int VipLevel { get; set; }
    public string PapalName { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }
    public int Hit { get; set; }
    public int Dodge { get; set; }
    public int Cri { get; set; }
    public int Res { get; set; }
    public int CurrHP { get; set; }
    public int MaxHP { get; set; }
    public int CurrMP { get; set; }
    public int MaxMP { get; set; }
    public int CurrExp { get; set; }
    public int MaxExp { get; set; }
}
