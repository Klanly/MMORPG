/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-09 15:14:48 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-09 15:14:48 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

/// <summary> 特效管理器 </summary>
public class EffectManager : Singleton<EffectManager> {
     
    /// <summary> 特效缓存池 </summary>
    private SpawnPool m_EffectPool;

    private Dictionary<string, Transform> m_EffectDic = new Dictionary<string, Transform>();

    /// <summary> 加载特效 </summary>
    /// <param name="effectName">特效名</param>
    /// <param name="effectFolder">特效所在文件夹默认为Effect主文件夹</param>
    /// <returns></returns>
    private GameObject LoadEffect(string effectName,string effectFolder = null)
    {
        GameObject obj = null;
        if (string.IsNullOrEmpty(effectFolder))
            obj = AssetBundleManager.Instance.Load(string.Format("Download/Effect/{0}.assetbundle", effectName), effectName);
        else
            obj = AssetBundleManager.Instance.Load(string.Format("Download/Effect/{0}/{1}.assetbundle",effectFolder,effectName), effectName);
        //return Object.Instantiate(obj);
        return obj;
    }

    /// <summary> 初始化 </summary>
    public void Init()
    {
        m_EffectPool = PoolManager.Pools.Create("Effect");
    }

    /// <summary> 播放特效 </summary>
    /// <param name="effectName">特效名</param>
    /// <param name="effectFolder">特效所在文件夹默认为Effect主文件夹</param>
    /// <returns></returns>
    public Transform PlayEffect(string effectName, string effectFolder = null)
    {
        if (m_EffectPool == null)
            Init();
        if (!m_EffectDic.ContainsKey(effectName))
        {
            //没有播放过
            m_EffectDic[effectName] = LoadEffect(effectName, effectFolder).transform;
            PrefabPool prefabPool = new PrefabPool(m_EffectDic[effectName]);
            prefabPool.preloadAmount = 0;//预加载数量
            prefabPool.cullDespawned = true;//是否开启缓存吃自动清理模式
            prefabPool.cullAbove = 5;//缓存池自动清理 但是始终保持几个对象不清理
            prefabPool.cullDelay = 2;//多长时间清理一次 时间是秒
            prefabPool.cullMaxPerPass = 2;//每次清理几个
            m_EffectPool.CreatePrefabPool(prefabPool);//创建子池
        }
        return m_EffectPool.Spawn(m_EffectDic[effectName]);
    }

    public void DestroyEffect(Transform effect,float delay)
    {
        m_EffectPool.Despawn(effect,delay);
        
    }

    /// <summary> 清空 </summary>
    public void Clear()
    {
        m_EffectDic.Clear();
        m_EffectPool = null;
    }
}
