/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-05 00:26:01 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-05 00:26:01 
/// </summary>

using UnityEngine;
using UnityEngine.UI;
/// <summary>  </summary>
public class UIMainCityRoleInfoView : MonoBehaviour {

    public static UIMainCityRoleInfoView Instance;

    private Transform m_Trans;
    private Image m_PlayerIcon;
    private Text m_Level;
    private Text m_NickName;
    private Text m_Vip;
    private Image m_HP;
    private Text m_HPRatio;
    private Image m_MP;
    private Text m_MPRatio;
    private Text m_Fighting;
    private void Awake()
    {
        m_Trans = transform;
        m_PlayerIcon = m_Trans.Find("PlyaerHeadAll/Icon").GetComponent<Image>();
        m_Level = m_Trans.Find("PlyaerHeadAll/PlayerHeadInfo/Level").GetComponent<Text>();
        m_NickName = m_Trans.Find("PlyaerHeadAll/PlayerHeadInfo/NickName").GetComponent<Text>();
        m_Vip = m_Trans.Find("Vip/Value").GetComponent<Text>();
        m_HP = m_Trans.Find("PlyaerHeadAll/HP/Value").GetComponent<Image>();
        m_HPRatio = m_Trans.Find("PlyaerHeadAll/HP/Text").GetComponent<Text>();
        m_MP = m_Trans.Find("PlyaerHeadAll/MP/Value").GetComponent<Image>();
        m_MPRatio = m_Trans.Find("PlyaerHeadAll/MP/Text").GetComponent<Text>();
        m_Fighting = m_Trans.Find("PlyaerHeadAll/FightingBg/Text").GetComponent<Text>();
        Instance = this;
    }

    /// <summary> 设置属性 </summary>
    public void SetUI(TransferData data)
    {
        Debug.LogError("SetUI Level = " + data.GetValue<int>(ConstDefine.Level).ToString());
        m_PlayerIcon.SetSprite("RoleIcon", data.GetValue<string>(ConstDefine.RoleIcon));
        m_Level.text = data.GetValue<int>(ConstDefine.Level).ToString();
        m_NickName.text = data.GetValue<string>(ConstDefine.NickName);
        m_Vip.text = data.GetValue<int>(ConstDefine.VIPLevel).ToString();
        m_HP.fillAmount = (float)data.GetValue<int>(ConstDefine.CurrHP) / data.GetValue<int>(ConstDefine.MaxHP);
        m_HPRatio.text = (m_HP.fillAmount * 100).ToString("0.00") + "%";
        m_MP.fillAmount = (float)data.GetValue<int>(ConstDefine.CurrMP) / data.GetValue<int>(ConstDefine.MaxMP);
        m_MPRatio.text = (m_MP.fillAmount * 100).ToString("0.00") + "%";
        m_Fighting.text = data.GetValue<int>(ConstDefine.FinalFighting).ToString();
    }

    public void SetHP(int currHP,int maxHP)
    {
        m_HP.fillAmount = currHP / (float)maxHP;
        m_HPRatio.text = (m_HP.fillAmount * 100).ToString("0.00") + "%";
    }

    public void SetMP(int currMP,int maxMP)
    {
        m_MP.fillAmount = currMP/ (float)maxMP;
        m_MPRatio.text = (m_MP.fillAmount * 100).ToString("0.00") + "%";
    }
}
