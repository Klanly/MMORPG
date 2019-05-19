/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-31 09:40:57 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-31 09:40:57 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class GameLevelDoorCtrl : MonoBehaviour {
    
    /// <summary> 关联的门 </summary>
    public GameLevelDoorCtrl CorrelationDoor;
    
    /// <summary> 所需区域Id </summary>
    [HideInInspector]
    public int OwnerRegionId;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (CorrelationDoor != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.transform.position, CorrelationDoor.transform.position);
        }
    }

#endif
}
