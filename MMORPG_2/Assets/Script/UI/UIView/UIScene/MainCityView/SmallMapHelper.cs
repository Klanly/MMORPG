/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-06 01:28:52 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-06 01:28:52 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class SmallMapHelper : MonoBehaviour {

    public static SmallMapHelper Instance;
	// Use this for initialization
	void Awake () {
        Instance = this;
	}
}
