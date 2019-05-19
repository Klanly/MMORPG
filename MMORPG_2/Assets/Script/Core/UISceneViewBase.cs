using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneViewBase : UIViewBase {

    /// <summary>
    /// 容器_居中
    /// </summary>
    [SerializeField]
    public Transform Container_Center;

    /// <summary> 界面加载完成回调 </summary>
    public Action OnLoadComplete;

    public bl_HUDText HUDText;
}
