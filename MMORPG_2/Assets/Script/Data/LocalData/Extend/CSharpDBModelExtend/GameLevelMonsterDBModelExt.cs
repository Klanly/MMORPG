/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-03 21:01:25 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-03 21:01:25 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public partial class GameLevelMonsterDBModel {

    private Dictionary<int, Dictionary<int,List<GameLevelMonsterEntity>>> m_MonsterDic = new Dictionary<int, Dictionary<int, List<GameLevelMonsterEntity>>>();
    public Dictionary<int, Dictionary<int, List<GameLevelMonsterEntity>>> MonsterDic
    {
        get
        {
            if (m_MonsterDic.Count == 0)
            {
                List<GameLevelMonsterEntity> list = GetList();
                for (int i = 0; i < list.Count; i++)
                {
                    if (!m_MonsterDic.ContainsKey(list[i].Chapter))
                        m_MonsterDic[list[i].Chapter] = new Dictionary<int, List<GameLevelMonsterEntity>>();
                    if (!m_MonsterDic[list[i].Chapter].ContainsKey(list[i].GameLevel))
                        m_MonsterDic[list[i].Chapter][list[i].GameLevel] = new List<GameLevelMonsterEntity>();
                    m_MonsterDic[list[i].Chapter][list[i].GameLevel].Add(list[i]);
                }
            }
            return m_MonsterDic;
        }
    }

    private List<GameLevelMonsterEntity> m_RegionMonsterList = new List<GameLevelMonsterEntity>();

    /// <summary> 获取关卡怪物数量 </summary>
    /// <param name="chapter">章节</param>
    /// <param name="gameLevelId">关卡</param>
    /// <returns></returns>
    public int GetGameLevelMonsterCount(int chapter,int gameLevelId)
    {
        return MonsterDic[chapter][gameLevelId].Count;
    }

    /// <summary> 获取关卡怪物Id列表 </summary>
    /// <param name="chapter">章节</param>
    /// <param name="gameLevelId">关卡</param>
    /// <returns></returns>
    public int[] GetGameLevelMonsterId(int chapter, int gameLevelId)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < MonsterDic[chapter][gameLevelId].Count; i++)
        {
            if (!list.Contains(MonsterDic[chapter][gameLevelId][i].MonsterId))
            {
                list.Add(MonsterDic[chapter][gameLevelId][i].MonsterId);
            }
        }
        return list.ToArray();
    }

    /// <summary> 获取区域内怪物id及数量 </summary>
    /// <param name="chapter"></param>
    /// <param name="gameLevelId"></param>
    /// <param name="regionId"></param>
    /// <returns></returns>
    public List<GameLevelMonsterEntity> GetGamelLevelRegionMonster(int chapter, int gameLevelId,int regionId)
    {
        m_RegionMonsterList.Clear();
        for (int i = 0; i < MonsterDic[chapter][gameLevelId].Count; i++)
        {
            if (MonsterDic[chapter][gameLevelId][i].RegionId == regionId && !m_RegionMonsterList.Contains(MonsterDic[chapter][gameLevelId][i]))
            {
                m_RegionMonsterList.Add(MonsterDic[chapter][gameLevelId][i]);
            }
        }
        return m_RegionMonsterList;
    }

    public GameLevelMonsterEntity GetGameLevelMonsterEntity(int chapter,int gameLevel,int regionId,int monsterId)
    {
        for (int i = 0; i < m_RegionMonsterList.Count; i++)
        {
            if (m_RegionMonsterList[i].MonsterId == monsterId)
            {
                return m_RegionMonsterList[i];
            }
        }
        return null;
    }
}
