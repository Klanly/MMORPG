using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 数值变化委托 </summary>
/// <param name="type"></param>
/// <param name="value"></param>
public delegate void OnValueChangeHandle(ValueChnageType type);

public class DelegateDefine:Singleton<DelegateDefine>  {

    public Action OnSceneLoadOk;

    
}
