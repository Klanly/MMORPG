/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-25 09:45:39 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-25 09:45:39 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class StarItem : MonoBehaviour {

    private GameObject m_StarLight;
    private void Awake()
    {
        m_StarLight = transform.Find("StarLight").gameObject;
    }

    public void SetLight(bool isActive)
    {
        m_StarLight.SetActive(isActive);
    }
}
