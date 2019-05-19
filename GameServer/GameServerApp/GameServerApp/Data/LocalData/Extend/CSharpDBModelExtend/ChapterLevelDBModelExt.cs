/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-26 11:27:23 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-26 11:27:23 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>  </summary>
public partial class ChapterLevelDBModel {

    private Dictionary<int, List<ChapterLevelEntity>> m_EntityDic = new Dictionary<int, List<ChapterLevelEntity>>();

    public Dictionary<int, List<ChapterLevelEntity>> EntityDic{ get { return m_EntityDic; } }
    public List<ChapterLevelEntity> GetList(int chapter)
    {
        if (m_EntityDic.Count == 0)
        {
            List<ChapterLevelEntity> entityList = GetList();
            for (int i = 0; i < entityList.Count; i++)
            {
                if (!m_EntityDic.ContainsKey(entityList[i].Chapter))
                    m_EntityDic[entityList[i].Chapter] = new List<ChapterLevelEntity>();
                m_EntityDic[entityList[i].Chapter].Add(entityList[i]);
            }
        }
        if (m_EntityDic.ContainsKey(chapter))
            return m_EntityDic[chapter];
        else
        {
            Console.WriteLine("dont hava index:"+chapter);
            return null;

        }
    }
}
