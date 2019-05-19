/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-14 13:22:00 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-14 13:22:00 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class UIGameLevelMapWindow : UIWindowViewBase
{
    public Transform Container;

    public Action<GameObject> BtnCallBack;
    protected override void OnAwake()
    {
        base.OnAwake();
    }
    protected override void OnBtnClick(GameObject go)
    {
        if (BtnCallBack != null)
        {
            BtnCallBack(go);
        }
    }
}
