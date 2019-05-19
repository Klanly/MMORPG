/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-05 12:26:03 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-05 12:26:03 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class UI_Camera : MonoBehaviour {

    [HideInInspector]
    public Camera Camera;
    public static UI_Camera Instance;
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    private void Start ()
    {
        Camera = GetComponent<Camera>();	
	}
}
