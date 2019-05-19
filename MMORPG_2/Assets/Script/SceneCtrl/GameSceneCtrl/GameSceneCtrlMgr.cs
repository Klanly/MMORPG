/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-03 08:39:15 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-03 08:39:15 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class GameSceneCtrlMgr : MonoBehaviour {

    /// <summary> 游戏关卡主控制器 </summary>
    [SerializeField]
    private GameLevelSceneCtrl m_GameLevelSceneCtrl;

    /// <summary> 世界地图场景主控制器 </summary>
    //[SerializeField]
    //private WorldMapSceneCtrl m_WorldMapSceneCtrl;

    private Dictionary<string, GameObject> m_Dic = new Dictionary<string, GameObject>();

    [SerializeField]
    private Transform m_Ground;

    private void Awake()
    {
        if (m_GameLevelSceneCtrl != null)
        {
            m_Dic[ConstDefine.Scene_ShanGu] = m_GameLevelSceneCtrl.gameObject;
        }
        //if (m_WorldMapSceneCtrl != null)
        //{
        //    m_Dic[ConstDefine.Scene_ShanGu] = m_WorldMapSceneCtrl.gameObject;
        //}
        GameObject obj = m_Dic[SceneMgr.Instance.CurrentSceneName];
        if (obj!= null)
        {
            obj.SetActive(true);
        }
        foreach (var item in m_Dic)
        {
            if (item.Key != SceneMgr.Instance.CurrentSceneName)
            {
                Destroy(item.Value);
            }
        }

        Renderer[] groundRenders = m_Ground.GetComponentsInChildren<Renderer>();
        if (groundRenders!=null && groundRenders.Length > 0 )
        {
            for (int i = 0; i < groundRenders.Length; i++)
            {
                groundRenders[i].enabled = false;
            }
        }
    }
}
