/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-11 14:49:52 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-11 14:49:52 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class TestSceneCtrl : SystemCtrlBase<TestSceneCtrl>,ISystemCtrl {
    public void OpenWindow(string winName)
    {
        WindowUtil.Instance.OpenWindow(winName);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
