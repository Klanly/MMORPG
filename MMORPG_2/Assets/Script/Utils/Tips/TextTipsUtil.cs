using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTipsUtil : Singleton<TextTipsUtil> {

    private Queue<TextTipsInfo> m_TipsQueue = new Queue<TextTipsInfo>();

    private List<TextTips> m_TextTipsList = new List<TextTips>();

    public TextTipsUtil()
    {
        m_TipsQueue.Clear();
        m_TextTipsList.Clear();
        if (m_TextTipsList == null || m_TextTipsList.Count == 0)
        {
            GameObject tipsCanvas = GameObject.Instantiate((GameObject)Resources.Load("UIPrefab/TextTips/TextTipsCanvas"));
            Object.DontDestroyOnLoad(tipsCanvas);
            tipsCanvas.name = "TextTipsCanvas";
            GameObject go = (GameObject)Resources.Load("UIPrefab/TextTips/TextTips");
            for (int i = 0; i < AppConst.TextTipsCount; i++)
            {
                GameObject InstGo = GameObject.Instantiate(go, tipsCanvas.transform);
                m_TextTipsList.Add(InstGo.GetComponent<TextTips>());
                InstGo.SetActive(false);
            }
        }
        FrameTimerManager.Instance.Add("ShowTextTipsTimer", 0.3f, Show,-1);
    }
    public void ShowTips(string textTips,TextTipsType textTipsType = TextTipsType.TextTips)
    {
        TextTipsInfo info = new TextTipsInfo();
        info.TipsType = textTipsType;
        info.TipsValue = textTips;
        m_TipsQueue.Enqueue(info);
    }

    void Show()
    {
        if (m_TipsQueue.Count>0)
        {
            TextTipsInfo tipsInfo = m_TipsQueue.Dequeue();
            for (int i = 0; i < m_TextTipsList.Count; i++)
            {
                if (!m_TextTipsList[i].IsRun)
                {
                    m_TextTipsList[i].transform.SetAsLastSibling();
                    m_TextTipsList[i].Show(tipsInfo.TipsValue,tipsInfo.TipsType);
                    break;
                }
            }
        }
    }
}

public class TextTipsInfo
{
    public TextTipsType TipsType { get; set; }

    public string TipsValue { get; set; }
}

