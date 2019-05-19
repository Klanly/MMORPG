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
public class LevelLimitItem : MonoBehaviour {

    private Text m_Text;
    [HideInInspector]
    public int Level;
    public void Init()
    {
        m_Text = transform.Find("TextName").GetComponent<Text>();
    }

    public void SetData(string value)
    {
        m_Text.text = value;
    }
}
