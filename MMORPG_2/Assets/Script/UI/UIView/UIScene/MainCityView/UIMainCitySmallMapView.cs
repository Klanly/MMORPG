/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-06 01:45:38 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-06 01:45:38 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>  </summary>
public class UIMainCitySmallMapView : MonoBehaviour {

    public static UIMainCitySmallMapView Instance;

    public Image SmallMap;
    public Image PlayerRoleArrow;
	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
