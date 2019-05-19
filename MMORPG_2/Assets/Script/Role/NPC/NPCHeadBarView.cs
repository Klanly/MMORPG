/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-08 16:05:59 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-08 16:05:59 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>  </summary>
public class NPCHeadBarView : MonoBehaviour
{

    /// <summary> 昵称 </summary>
    private Text m_NickName;
    /// <summary> 对齐的目标点 </summary>
    private Transform m_Target;
    private Transform m_Trans;
    private RectTransform m_RectTrans;

    private GameObject m_TalkBg;
    private Text m_TalkText;
    /// <summary> NPC自言自语 </summary>
    private string m_Talk;
    private NPCDataEntity m_NPCDataEntity;
    private Tweener tweener;
    private void Awake()
    {
        m_Trans = transform;
        m_RectTrans = GetComponent<RectTransform>();
        m_NickName = m_Trans.Find("NickName").GetComponent<Text>();
        m_TalkBg = m_Trans.Find("TalkBg").gameObject;
        m_TalkText = m_Trans.Find("TalkBg/Text").GetComponent<Text>();
        //m_TalkBg.SetActive(false);
        m_TalkBg.transform.localScale = Vector3.zero;
    }

    /// <summary> 初始化 </summary>
    /// <param name="target"></param>
    /// <param name="nickName"></param>
    public void Init(Transform target, NPCDataEntity entity)
    {
        m_Target = target;
        m_NPCDataEntity = entity;
        m_NickName.text = m_NPCDataEntity.Name;
        m_Talk = m_NPCDataEntity.Talk;
        DOPlayerAnimation();
    }

    private void DOPlayerAnimation()
    {
        if (string.IsNullOrEmpty(m_Talk))
            return;
        FrameTimerManager.Instance.Add("ShowNPCShowTalkTimer"+SceneMgr.Instance.CurrentWorldMapId.ToString()+m_NPCDataEntity.Id, 13f, ShowNPCTalk, -1);
        tweener = m_TalkBg.transform.DOScale(Vector3.one,1f)
               .SetAutoKill(false).Pause()
               .SetEase(Ease.InOutQuint)
               .OnComplete(() =>
               {
                   m_TalkText.DOText(m_NPCDataEntity.Talk, 2f);
                   StartCoroutine(Hide());
               });
    }
    IEnumerator Hide()
    {
        yield return new WaitForSeconds(5f);
        tweener.Rewind();
    }

    private void ShowNPCTalk()
    {
        //m_TalkBg.SetActive(true);
        //m_TalkBg.transform.localScale = Vector3.zero;
        m_TalkText.text = "";
        tweener.Restart();
    }

    void Update()
    {
        if (Camera.main == null || m_Target == null) return;

        //世界左边点 转换成视口坐标
        Vector3 screenPos = Camera.main.WorldToScreenPoint(m_Target.position);
        //转换成UI摄像机的世界坐标
        Vector3 pos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RectTrans, screenPos, UI_Camera.Instance.Camera, out pos))
        {
            transform.position = pos;
        }
    }

    private void OnDisable()
    {
        m_NickName = null;
        m_Target = null;
        m_Trans = null;
        m_RectTrans = null;
        m_TalkBg = null;
        m_TalkText = null;
        m_Talk = null;
        m_NPCDataEntity = null;
        tweener = null;
        FrameTimerManager.Instance.RemoveCallback(ShowNPCTalk);
    }
}
