using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
    public string CurrSceneType { get; private set; }

    /// <summary> 当前场景类型 </summary>
    public string CurrentSceneName { get; private set; }

    private int m_CurrWorldMapId;
    public int CurrentWorldMapId { get { return m_CurrWorldMapId; } }

    /// <summary> 去登录选区场景 </summary>
    public void LoadToLogOn()
    {
        CurrentSceneName = ConstDefine.Scene_LogOn;
        SceneManager.LoadScene(ConstDefine.Scene_Loading);
    }

    /// <summary> 去选人场景 </summary>
    public void LoadToSelectRole()
    {
        CurrentSceneName = ConstDefine.Scene_SelectRole;
        SceneManager.LoadScene(ConstDefine.Scene_Loading);
    }

    /// <summary> 去新手村 </summary>
    public void LoadToVillage()
    {
        CurrentSceneName = ConstDefine.Scene_CunZhuang;
        SceneManager.LoadScene(ConstDefine.Scene_Loading);
    }

    public void LoadScene(string sceneName)
    {
        CurrentSceneName = sceneName;
        SceneManager.LoadScene(ConstDefine.Scene_Loading);
    }

    /// <summary> 去世界地图场景(主城 + 野外场景) </summary>
    /// <param name="worldMapId"> 场景编号(世界地图.xls/WorldMap表) </param>
    public void LoadToWorldMap(int worldMapId)
    {
        m_CurrWorldMapId = worldMapId;
        WorldMapEntity entity = WorldMapDBModel.Instance.Get(worldMapId);
        CurrentSceneName = entity.SceneName;
        CurrSceneType = entity.SceneType;
        SceneManager.LoadScene("Scene_Loading");
    }
}