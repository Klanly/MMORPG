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

/// <summary>  </summary>
public class LevelLimitCtrl : LevelLimitCtrlBase {

	// Use this for initialization
	void Start () {
        List<GameObject> list = new List<GameObject>();
        list.Add(Resources.Load<GameObject>("UIPrefab/LevelLimit/LevelLimitItem"));
        //list.Add(Resources.Load<GameObject>("UIPrefab/LevelLimit/LevelLimitItem"));
        Init(list);
	}



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(LevelMin +"   "+LevelMax);
        }
    }
}
