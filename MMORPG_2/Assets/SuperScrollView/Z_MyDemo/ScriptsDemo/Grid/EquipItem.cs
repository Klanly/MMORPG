/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：#CreateTime# 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：#CreateTime# 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>  </summary>
public class EquipItem : MonoBehaviour {

    private Transform m_Trans;
    private Image m_Quality;
    private Image m_Icon;
    private GameObject m_Binding;
    private Text m_Level;
    private List<Image> m_StarList;
    [HideInInspector]
    public int Index;
    [HideInInspector]
    public string IconName;
    public void Init()
    {
        m_Trans = transform;
        m_Quality = m_Trans.Find("Quality").GetComponent<Image>();
        m_Icon = m_Trans.Find("Equip/Icon").GetComponent<Image>();
        m_Binding = m_Trans.Find("Equip/Binding").gameObject;
        m_Level = m_Trans.Find("Equip/Level/Text").GetComponent<Text>();
        m_StarList = new List<Image>(m_Trans.Find("Equip/LightStarContainer").GetComponentsInChildren<Image>());
    }

    public void SetData(int index,int quality,string iconName,bool isBinding,int level,int star)
    {
        Index = index;
        IconName = "UISources/Equip_QingYun/" + iconName;
        m_Icon.sprite = Resources.Load<Sprite>(IconName);
        m_Binding.SetActive(isBinding);
        m_Level.text = level.ToString();
        SetStar(star);
        SetQuality(quality);
    }

    private void SetQuality(int quality)
    {
        switch (quality)
        {
            case 1:
                m_Quality.color = Color.white;
                break;
            case 2:
                m_Quality.color = Color.green;
                break;
            case 3:
                m_Quality.color = Color.blue;
                break;
            case 4:
                m_Quality.color = new Color(255, 192, 203);
                break;
            case 5:
                m_Quality.color = Color.red;
                break;
        }
    }
    private void SetStar(int star)
    {
        for (int i = 0; i < m_StarList.Count; i++)
        {
            m_StarList[i].gameObject.SetActive(star > i + 1);
        }

    }
}
