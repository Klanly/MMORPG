/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-11-21 19:27:34 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-11-21 19:27:34 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class ToggleItem : MonoBehaviour {

    [HideInInspector]
    public int Index;

    /// <summary> 是否选中 </summary>
    /// <param name="isSelect"></param>
    public virtual void IsSelect(bool isSelect)
    {

    }
}
