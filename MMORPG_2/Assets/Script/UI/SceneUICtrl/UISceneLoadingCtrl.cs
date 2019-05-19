using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

/// <summary>
/// Loading场景UI控制器
/// </summary>
public class UISceneLoadingCtrl : MonoBehaviour
{
    /// <summary>
    /// UI场景控制器
    /// </summary>
    [SerializeField]
    private UISceneLoadingView m_UILoadingView;

    private AsyncOperation m_Async = null;

    /// <summary>
    /// 当前的进度
    /// </summary>
    private int m_CurrProgress = 0;

    /// <summary> 资源包异步创建请求 </summary>
    private AssetBundleCreateRequest request;

    /// <summary> 资源包 </summary>
    private AssetBundle bundle;

    void Start()
    {
        AssetBundleManager.Instance.UnloadDpsAssetBundle();
        GC.Collect();
        DelegateDefine.Instance.OnSceneLoadOk += OnSceneLoadOk;
        LayerManager.Instance.Reset();
        StartCoroutine(LoadingScene());
        WindowUtil.Instance.CloseAllWinwow();
        Resources.UnloadUnusedAssets();
    }

    private void OnSceneLoadOk()
    {
        if (m_UILoadingView != null)
        {
            Destroy(m_UILoadingView.gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator LoadingScene()
    {
        string strSceneName = SceneMgr.Instance.CurrentSceneName;
        if (string.IsNullOrEmpty(strSceneName))
        {
            yield break;
        }

        if (strSceneName.Equals(ConstDefine.Scene_LogOn) || strSceneName.Equals("Scene_Init")|| strSceneName.Equals(ConstDefine.Scene_Loading))
        {
            m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
            m_Async.allowSceneActivation = false;
            yield break;
        }

#if DISABLE_ASSETBUNDLE
        //加载
        m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
        //设置允许场景激活为false
        m_Async.allowSceneActivation = false;
        yield return m_Async;
#else
        StartCoroutine(Load(string.Format("Download/Scene/{0}.unity3d", strSceneName), strSceneName));
#endif
    }

    private IEnumerator Load(string path,string strSceneName)
    {
#if DISABLE_ASSETBUNDLE
        yield return null;
        path = path.Replace(".unity3d", "");
        m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
        m_Async.allowSceneActivation = false;
#else
        //获取资源完整路径
        string fullPath = LocalFileManager.Instance.LocalFilePath + path;
        //如果路径不存在 则进行下载
        if (!File.Exists(fullPath))
        {
            DownloadDataEntity entity = DownloadManager.Instance.GetServerData(path);
            if (entity!=null)
            {
                StartCoroutine(AssetBundleDownload.Instance.DownloadData(entity,(bool isSuccess) =>
                {
                    if (isSuccess)
                    {
                        StartCoroutine(LoadScene(fullPath,strSceneName));
                    }
                }));
            }
        }
        else
        {
            StartCoroutine(LoadScene(fullPath, strSceneName));
        }
        yield return null;
#endif
    }

    private IEnumerator LoadScene(string fullPath,string sceneName)
    {
        request = AssetBundle.LoadFromMemoryAsync(LocalFileManager.Instance.GetBuffer(fullPath));
        yield return request;
        bundle = request.assetBundle;
        m_Async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        m_Async.allowSceneActivation = false;
    }

    void Update()
    {
        if (m_Async == null)
        {
            return;
        }
        int toProgress = 0;
        if (m_Async.progress < 0.9f)
        {
            toProgress = Mathf.Clamp((int)m_Async.progress * 100, 1, 100);
        }
        else
        {
            toProgress = 100;
        }

        if (m_CurrProgress < toProgress)
        {
            m_CurrProgress++;
        }
        else
        {
            m_Async.allowSceneActivation = true;
        }

        m_UILoadingView.SetProgressValue(m_CurrProgress * 0.01f);
    }

    private void OnDestroy()
    {
        DelegateDefine.Instance.OnSceneLoadOk -= OnSceneLoadOk;
        if (bundle != null)
            bundle.Unload(false);

        request = null;
        bundle = null;
    }
}