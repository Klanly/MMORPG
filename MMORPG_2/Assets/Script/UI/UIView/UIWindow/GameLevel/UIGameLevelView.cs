/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-26 10:31:11 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-26 10:31:11 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>  </summary>
public class UIGameLevelView : MonoBehaviour {

    private Transform m_Trans;
    private Text m_ChapterName;
    private Transform m_Right;
    private Image m_LevelImage;
    private Text m_Desc;
    private Transform m_StarContainer;
    private Text m_LevelName;
    private Text m_FightValue;
    private Text m_CountValue;
    private Text m_VitalityValue;
    private Text m_ThreeStarValue;
    private Button m_BtnChallenge;

    private StarContainer starContainer;
    public Action BtnChallengeClickCallBack;
    private int m_Chapter = -1;
    public int Chapter { get { return m_Chapter; } }
    public void Init()
    {
        m_Trans = transform;
        m_ChapterName = m_Trans.Find("ChapterNameBg/ChapterName").GetComponent<Text>();
        m_Right = m_Trans.Find("Right");
        m_LevelImage = m_Right.Find("LevelImage").GetComponent<Image>();
        m_Desc = m_Right.Find("Desc").GetComponent<Text>();
        m_StarContainer = m_Right.Find("StarContainer");
        m_LevelName = m_Right.Find("LevelName").GetComponent<Text>();
        m_FightValue = m_Right.Find("BottomBg/FightValue").GetComponent<Text>();
        m_CountValue = m_Right.Find("BottomBg/CountValue").GetComponent<Text>();
        m_VitalityValue = m_Right.Find("BottomBg/VitalityValue").GetComponent<Text>();
        m_ThreeStarValue = m_Right.Find("BottomBg/ThreeStarValue").GetComponent<Text>();
        m_BtnChallenge = m_Right.Find("BtnChallenge").GetComponent<Button>();
        EventTriggerListener.Get(m_BtnChallenge.gameObject).onClick = OnBtnClick;

    }

    private void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "BtnChallenge":
                if (BtnChallengeClickCallBack != null)
                    BtnChallengeClickCallBack();
                break;
        }
    }

    public void SetChapterName(int chapter)
    {
        m_Chapter = chapter;
        m_ChapterName.text = string.Format(StringUtil.GetStringById(1000401), chapter);
    }

    public void SetData(TransferData data)
    {
        m_LevelImage.SetBg(data.GetValue<string>(ConstDefine.FolderName),data.GetValue<string>(ConstDefine.IconName));
        m_Desc.text = data.GetValue<string>(ConstDefine.Desc);
        GameUtil.LoadStar(m_StarContainer, data.GetValue<int>(ConstDefine.CurrStar), data.GetValue<int>(ConstDefine.MaxStar), data.GetValue<int>(ConstDefine.RowStarCount));
        //m_Desc.text = data.GetValue<string>(ConstDefine.Desc);
        m_Desc.SetText(data.GetValue<string>(ConstDefine.Desc));
        m_LevelName.text = data.GetValue<string>(ConstDefine.Name);
        m_FightValue.text = data.GetValue<int>(ConstDefine.RecommendFight).ToString()+string.Format("(<color=#93B61EFF>({0})</color>)", GlobalInit.Instance.PlayerInfo.Fighting);
        m_CountValue.text = data.GetValue<int>(ConstDefine.MaxCount).ToString();
        m_VitalityValue.text = data.GetValue<int>(ConstDefine.NeedVitality).ToString();
        m_ThreeStarValue.text = data.GetValue<int>(ConstDefine.FirstThreeStar).ToString();
    }
}
