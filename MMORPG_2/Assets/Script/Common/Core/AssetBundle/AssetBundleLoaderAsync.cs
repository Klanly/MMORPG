/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-11-14 22:09:08 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-11-14 22:09:08 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class AssetBundleLoaderAsync : MonoBehaviour {

    /// <summary> 资源完整路径 </summary>
    private string m_FullPath;

    /// <summary> 资源名称 </summary>
    private string m_Name;

    /// <summary> 资源包异步创建请求 </summary>
    private AssetBundleCreateRequest request;

    /// <summary> 资源包 </summary>
    private AssetBundle bundle;

    /// <summary> 资源加载完成回调 </summary>
    public Action<UnityEngine.Object> OnLoadComplete;

    /// <summary> 异步加载场景完成回调 </summary>
    public Action<AsyncOperation> OnSceneLoadComplete;
    private void Start()
    {
        StartCoroutine(Load());
    }

    public void Init(string path,string name)
    {
        m_FullPath = LocalFileManager.Instance.LocalFilePath + path;
        m_Name = name;
    }

    /// <summary> 异步加载资源 </summary>
    /// <returns></returns>
    private IEnumerator Load()
    {
        request = AssetBundle.LoadFromMemoryAsync(LocalFileManager.Instance.GetBuffer(m_FullPath));
        yield return request;
        bundle = request.assetBundle;
        if (bundle.isStreamedSceneAssetBundle)
        {
            //加载的是场景
            if (OnSceneLoadComplete != null)
            {
                AsyncOperation m_Async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(m_Name, UnityEngine.SceneManagement.LoadSceneMode.Additive);
                OnSceneLoadComplete(m_Async);
                DestroyImmediate(gameObject);
            }
        }
        else
        {
            if (OnLoadComplete != null)
            {
                OnLoadComplete(bundle.LoadAsset(m_Name));
                DestroyImmediate(gameObject);
                Debuger.Log(string.Format("异步加载 {0} 完成", m_Name));
            }
        }
    }

    /// <summary> 销毁 </summary>
    private void OnDestroy()
    {
        //写在所有包含在bundle中的对象
        if (bundle != null) bundle.Unload(false);
        OnLoadComplete = null;
        OnSceneLoadComplete = null;
        m_FullPath = null;
        m_Name = null;
    }
}
