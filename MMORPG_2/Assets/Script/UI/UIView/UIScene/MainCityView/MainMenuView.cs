/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-05 18:21:48 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-05 18:21:48 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class MainMenuView : MonoBehaviour {

    private Transform m_Trans;
    private void Awake()
    {
        m_Trans = transform;
    }

    public void SetMainMenu(List<MainMenuIconItem> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.SetParent(m_Trans);
            list[i].transform.localScale = Vector3.one;
            list[i].transform.localPosition = new Vector2((list[i].GetComponent<RectTransform>().sizeDelta.x + 15)*(1-list[i].Index),32-(list[i].Row-1)*80);
        }
    }
}
