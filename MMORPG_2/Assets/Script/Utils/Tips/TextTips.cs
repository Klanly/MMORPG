using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TextTips : MonoBehaviour {

    private Text m_Text;
    private CanvasGroup m_CanvasGroup;

    private Transform m_Trans;
    private Image m_Icon;
    [HideInInspector]
    public bool IsRun = false;
    //public Ease Ease = Ease.Linear;
    private float m_StartY = 0;
    private float m_EndY = 0;

    private void Awake()
    {
        m_Trans = transform;
        m_EndY = Screen.height / 2 - 100;
        m_StartY = m_EndY - m_Trans.GetComponent<RectTransform>().sizeDelta.y*AppConst.TextTipsCount;
        m_Text = m_Trans.Find("Text").GetComponent<Text>();
        m_Icon = m_Trans.Find("Icon").GetComponent<Image>();
        m_CanvasGroup = m_Trans.GetComponent<CanvasGroup>();
        m_Trans.localPosition = new Vector2(m_Trans.localPosition.x, m_StartY);
        m_Trans.DOLocalMoveY(m_EndY, AppConst.TextTipsYDuration).SetAutoKill(false).Pause().SetEase(Ease.OutCubic).OnComplete(()=> 
        {
            m_CanvasGroup.DORestart();
        });
        m_CanvasGroup.DOFade(0, AppConst.TextTipsAlphaDuration).SetAutoKill(false).Pause().OnComplete(() =>
        {
            IsRun = false;
            m_Trans.localPosition = new Vector2(m_Trans.localPosition.x, m_StartY);
            m_Trans.gameObject.SetActive(false);
        });
    }

    public void Show(string tipsStr,TextTipsType tipsType = TextTipsType.TextTips)
    {
        m_Trans.gameObject.SetActive(true);
        IsRun = true;
        m_Text.text = tipsStr;
        m_Icon.gameObject.SetActive(true);
        switch (tipsType)
        {
            case TextTipsType.TextTips:
                m_Icon.gameObject.SetActive(false);
                break;
            case TextTipsType.ExpTips:
                m_Icon.SetSprite("Icon", "ICON_I_Currency_05");
                break;
             case TextTipsType.CoinTips:
                m_Icon.SetSprite("Icon", "ICON_I_Currency_01");
                break;
            case TextTipsType.GoldBindingTips:
                m_Icon.SetSprite("Icon", "ICON_I_Currency_03");
                break;
            case TextTipsType.GoldTips:
                m_Icon.SetSprite("Icon", "ICON_I_Currency_02");
                break;
        }
        m_CanvasGroup.alpha = 1;
        m_Trans.DORestart();
    }
}
