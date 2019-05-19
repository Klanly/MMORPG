using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoleHeadBarView : MonoBehaviour
{
    /// <summary> 昵称 </summary>
    private Text m_NickName;
    /// <summary> 昵称 </summary>
    private Slider m_SliderHP;

    /// <summary>
    /// 飘血显示
    /// </summary>
    //private HUDText hudText;

    /// <summary> 对齐的目标点 </summary>
    private Transform m_Target;
    private Transform m_Trans;
    private RectTransform m_RectTrans;
    private void Awake()
    {
        m_Trans = transform;
        m_RectTrans = GetComponent<RectTransform>();
        m_NickName = m_Trans.Find("NickName").GetComponent<Text>();
        m_SliderHP = m_Trans.Find("SliderHP").GetComponent<Slider>();
    }

    void Update()
    {
        if (Camera.main == null ||  m_Target == null|| m_RectTrans == null) return;

        //世界左边点 转换成视口坐标
        Vector3 screenPos = Camera.main.WorldToScreenPoint(m_Target.position);
        //转换成UI摄像机的世界坐标
        Vector3 pos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RectTrans,screenPos,UI_Camera.Instance.Camera,out pos))
        {
            transform.position = pos;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="nickName"></param>
    /// <param name="isShowHPBar">是否显示血条</param>
    /// <param name="sliderValue"> slider值 </param>
    public void Init(Transform target, string nickName, bool isShowHPBar = false,float sliderValue = 1f)
    {
        m_Target = target;
        m_NickName.text = StringUtil.GetRichString(nickName, isShowHPBar?"yellow":"green");
        m_SliderHP.gameObject.SetActive(isShowHPBar);
        m_SliderHP.value = sliderValue;
    }

    public void SetSliderHP(float sliderValue = 1f)
    {
        m_SliderHP.value = sliderValue;
    }

    /// <summary>
    /// 上弹伤害文字
    /// </summary>
    /// <param name="hurtValue"></param>
    public void Hurt(int hurtValue, float pbHPValue = 0)
    {
        //hudText.Add(string.Format("-{0}", hurtValue), Color.red, 0.1f);
    }
}