/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-12 14:49:18 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-12 14:49:18 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class UIRoleInfoWindow : UIWindowViewBase
{
    [HideInInspector]
    public Transform Container;
    protected override void OnAwake()
    {
        base.OnAwake();
        Container = transform.Find("Container");
    }
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
    }
}
