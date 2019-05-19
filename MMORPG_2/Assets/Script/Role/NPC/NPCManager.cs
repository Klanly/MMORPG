/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-08 10:44:53 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-08 10:44:53 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

/// <summary> NPC管理器 </summary>
public class NPCManager : Singleton<NPCManager> {
    
    /// <summary> 获取指定世界地图中的所有NPC列表 </summary>
    /// <param name="npcExcelName">npc表</param>
    /// <returns></returns>
    public List<NPCDataEntity> GetWorldMapEntityList(string npcExcelName)
    {
        npcExcelName = npcExcelName + "DBModel";
        Type type = Type.GetType(npcExcelName);
        if (type == null)
        {
            Debug.Log("class <color=yellow>" + npcExcelName + "</color> is null");
            return null;
        }
        var instanceObj = type.Assembly.CreateInstance(npcExcelName);
        MethodInfo info = type.GetMethod("GetList");
        object obj = info.Invoke(instanceObj, null);
        List<NPCDataEntity> result = obj as List<NPCDataEntity>;
        return result;
    }

    /// <summary> 获取指定世界地图中的所有NPC列表 </summary>
    /// <param name="id">世界地图WorldMap表中的id</param>
    /// <returns></returns>
    public List<NPCDataEntity> GetWorldMapEntityList(int id)
    {
        WorldMapEntity entity = WorldMapDBModel.Instance.Get(id);
        if (entity == null)
        {
            Debuger.Log("该场景没有NPC 场景在WorldMap表中的id = "+id);
            return null;
        }
        else
        {
            return GetWorldMapEntityList(entity.NPCExcel);
        }
    }
}
