using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary> AssetBundle管理类 </summary>
public class AssetBundleManager :Singleton<AssetBundleManager> {

    /// <summary> 资源包名单,依赖文件配置 </summary>
    private AssetBundleManifest m_Manifest;

    //private AssetBundleLoader abLoader;

    /// <summary> 所有加载的Asset资源镜像 </summary>
    private Dictionary<string, UnityEngine.Object> m_AssetDic = new Dictionary<string, UnityEngine.Object>();

    /// <summary> 依赖项的列表 </summary>
    private Dictionary<string, AssetBundleLoader> m_AssetBundleDic = new Dictionary<string, AssetBundleLoader>();

    /// <summary> 加载依赖文件配置 </summary>
    private void LoadManifestBundle()
    {
        if (m_Manifest != null)
        {
            return;
        }
        string assetName = string.Empty;
#if UNITY_STANDALONE_WIN
        assetName = "Windows";
#elif UNITY_ANDROID
        assetName = "Android";
#elif UNITY_IPHONE
        assetName = "iOS";
#endif
        using (AssetBundleLoader loader = new AssetBundleLoader(assetName))
        {
            //abLoader = loader;
            m_Manifest = loader.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        Debuger.Log("加载依赖文件配置 完毕");
    }
    /// <summary> 加载图集中的图片 </summary>
    /// <param name="atlasName">图集名</param>
    /// <param name="spriteName">图片名</param>
    /// <returns></returns>
    public Sprite LoadSprite(string atlasName, string spriteName)
    {
        return Resources.Load<Sprite>("UI/UISources/" + atlasName + "/" + spriteName);
    }

    /// <summary> 加载背景图片 </summary>
    /// <param name="folderName">背景所在文件夹名</param>
    /// <param name="spriteName">背景图片名</param>
    /// <returns></returns>
    public Sprite LoadBg(string folderName, string spriteName)
    {
        return Resources.Load<Sprite>("UI/UITexture/" + folderName + "/" + spriteName);
    }

    /// <summary> 加载或下载资源 </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="path">段路径</param>
    /// <param name="name">加载对象名</param>
    /// <param name="onComplete">C#加载完成回调</param>
    /// <param name="type">0=prefab 1=png</param>
    public void LoadOrDownload<T>(string path, string name, Action<T> onComplete, byte type = 0) where T : UnityEngine.Object
    {
        lock (this)
        {
#if DISABLE_ASSETBUNDLE
            string newPath = string.Empty;
            switch (type)
            {
                case 0:
                    newPath = string.Format("Assets/{0}", path.Replace("assetbundle","prefab"));
                    break;
                case 1:
                    newPath = string.Format("Assets/{0}", path.Replace("assetbundle", "png"));
                    break;
            }
            if (onComplete!=null)
            {
                UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(newPath);
                onComplete(obj as T);
            }
#else
            //1、加载依赖文件配置
            LoadManifestBundle();
            //2、开始加载依赖项
            string[] arrDps = m_Manifest.GetAllDependencies(path);
            //3、检查依赖项是否已经下载 若没下载则下载
            CheckDps(0,arrDps,()=>
            {
                string fullPath = (LocalFileManager.Instance.LocalFilePath + path).ToLower();
                #region ------ 下载或者加载主资源 ------
                
                if (!File.Exists(fullPath))
                {
                    //文件不存在 需要下载
                    DownloadDataEntity entity = DownloadManager.Instance.GetServerData(path);
                    if (entity != null)
                    {
                        AssetBundleDownload.Instance.StartCoroutine(AssetBundleDownload.Instance.DownloadData(entity, 
                            (bool isSuccess) => 
                            {
                                if (isSuccess)
                                {
                                    //下载成功
                                    if (m_AssetDic.ContainsKey(fullPath))
                                    {
                                        //文件已存在镜像中
                                        if (onComplete != null)
                                        {
                                            onComplete(m_AssetDic[fullPath] as T);
                                        }
                                        return;
                                    }
                                    //先加载依赖项
                                    for (int i = 0; i < arrDps.Length; i++)
                                    {
                                        if (!m_AssetDic.ContainsKey((LocalFileManager.Instance.LocalFilePath + arrDps[i]).ToLower()))
                                        {
                                            AssetBundleLoader loader = new AssetBundleLoader(arrDps[i]);
                                            UnityEngine.Object obj = loader.LoadAsset(GameUtil.GetFileName(arrDps[i]));
                                            m_AssetBundleDic[(LocalFileManager.Instance.LocalFilePath + arrDps[i]).ToLower()] = loader;
                                            m_AssetDic[(LocalFileManager.Instance.LocalFilePath + arrDps[i]).ToLower()] = obj;
                                        }
                                    }
                                    //直接加载
                                    using (AssetBundleLoader loader = new AssetBundleLoader(fullPath,isFullPath:true))
                                    {
                                        if (onComplete != null)
                                        {
                                            UnityEngine.Object obj = loader.LoadAsset<T>(name);
                                            m_AssetDic[fullPath] = obj;
                                            onComplete(obj as T);
                                        }
                                        //TODO lua回调
                                    }
                                }
                            }));
                    }
                    else
                    {
                        Debuger.LogError("The fullPath is error:" + fullPath);
                    }
                }
                else
                {
                    if (m_AssetDic.ContainsKey(fullPath))
                    {
                        if (onComplete != null)
                        {
                            onComplete(m_AssetDic[fullPath] as T);
                        }
                        //TODO lua回调

                        return;
                    }

                    for (int i = 0; i < arrDps.Length; i++)
                    {
                        if (!m_AssetDic.ContainsKey((LocalFileManager.Instance.LocalFilePath + arrDps[i]).ToLower()))
                        {
                            AssetBundleLoader loader = new AssetBundleLoader(arrDps[i]);
                            UnityEngine.Object obj = loader.LoadAsset(GameUtil.GetFileName(arrDps[i]));
                            m_AssetBundleDic[(LocalFileManager.Instance.LocalFilePath + arrDps[i]).ToLower()] = loader;
                            m_AssetDic[(LocalFileManager.Instance.LocalFilePath + arrDps[i]).ToLower()] = obj;
                        }
                    }
                    //直接加载
                    using (AssetBundleLoader loader = new AssetBundleLoader(fullPath, isFullPath: true))
                    {
                        UnityEngine.Object obj = loader.LoadAsset<T>(name);
                        m_AssetDic[fullPath] = obj;
                        if (onComplete != null)
                        {
                            //进行回调
                            onComplete(obj as T);
                        }
                        //TODO 进行xlua的回调
                    }
                }

                #endregion
            });
#endif
        }
    }

    /// <summary> 检查依赖项 </summary>
    /// <param name="index">索引</param>
    /// <param name="arrDps">所有依赖</param>
    /// <param name="onComplete">完成回调</param>
    private void CheckDps(int index, string[] arrDps, Action onComplete)
    {
        lock (this)
        {
            if (arrDps == null || arrDps.Length == 0)
            {
                if (onComplete != null) onComplete();
                return;
            }
            string fullPath = LocalFileManager.Instance.LocalFilePath + arrDps[index];
            if (!File.Exists(fullPath))
            {
                //如果文件不存在需要下载
                DownloadDataEntity entity = DownloadManager.Instance.GetServerData(arrDps[index]);
                if (entity != null)
                {
                    AssetBundleDownload.Instance.StartCoroutine(AssetBundleDownload.Instance.DownloadData(entity,
                        (bool isSuccess) => 
                        {
                            index++;
                            if (index == arrDps.Length)
                            {
                                if (onComplete != null)
                                {
                                    onComplete();
                                    return;
                                }
                                CheckDps(index, arrDps, onComplete);
                            }
                        }));
                }
            }
            else
            {
                index++;
                if (index == arrDps.Length)
                {
                    if (onComplete != null) onComplete();
                    return;
                }
                CheckDps(index, arrDps, onComplete);
            }
        }
    }

    /// <summary> 加载镜像 </summary>
    /// <param name="path">资源路径</param>
    /// <param name="name">资源名称</param>
    /// <returns></returns>
    public GameObject Load(string path, string name)
    {
#if DISABLE_ASSETBUNDLE
        return UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(string.Format("Assets/{0}", path.Replace("assetbundle", "prefab")));
#else
        using (AssetBundleLoader loader = new AssetBundleLoader(path))
        {
            return loader.LoadAsset<GameObject>(name);
        }
#endif
    }

    /// <summary> 同步加载 </summary>
    /// <param name="path">资源路径</param>
    /// <param name="name">资源名称</param>
    /// <returns></returns>
    public GameObject LoadClone(string path, string name)
    {
#if DISABLE_ASSETBUNDLE
        GameObject obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(string.Format("Assets/{0}", path.Replace("assetbundle", "prefab")));
        return UnityEngine.Object.Instantiate(obj);
#else
        using (AssetBundleLoader loader = new AssetBundleLoader(path))
        {
            GameObject obj = loader.LoadAsset<GameObject>(name);
            return UnityEngine.Object.Instantiate(obj);
        }
#endif
    }

    /// <summary> 异步加载 </summary>
    /// <param name="path">资源路径</param>
    /// <param name="name">资源名称</param>
    /// <returns></returns>
    public AssetBundleLoaderAsync LoadAsync(string path, string name)
    {
        //实例化一个游戏对象
        GameObject obj = new GameObject("AssetBundleLoadAsync");
        //如果obj 没有这个脚本  就添加这个脚本
        AssetBundleLoaderAsync async = obj.GetOrAddComponent<AssetBundleLoaderAsync>();
        //初始化路径和名称
        async.Init(path, name);

        return async;
    }

    /// <summary> 卸载依赖项</summary>
    public void UnloadDpsAssetBundle()
    {
        foreach (var item in m_AssetBundleDic)
        {
            item.Value.Dispose();
        }
        m_AssetBundleDic.Clear();
        m_AssetDic.Clear();
    }
}
