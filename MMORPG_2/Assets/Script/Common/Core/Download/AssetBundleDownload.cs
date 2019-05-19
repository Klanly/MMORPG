using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetBundleDownload : SingletonMono<AssetBundleDownload> {

    /// <summary> 版本信息资源地址 </summary>
    private string m_VersionUrl;
    /// <summary> 初始化服务器版本信息委托 </summary>
    private Action<List<DownloadDataEntity>> m_OnInitVersion;

    /// <summary> 下载器数组 </summary>
    private AssetBundleDownloadRoutine[] m_RoutineArr = new AssetBundleDownloadRoutine[AppConst.DownloadRountineNum];
    /// <summary> 下载器索引 </summary>
    private int m_RoutineIndex = 0;

    /// <summary> 是否下载完成 </summary>
    private bool m_IsDownloadOver = false;

    /// <summary> 总数量 </summary>
    public int TotalCount
    {
        get;private set;
    }
    /// <summary> 总大小 </summary>
    public int TotalSize
    {
        get; private set;
    }
    /// <summary> 正在下载的文件大小 </summary>
    public int DownloadSize
    {
        get
        {
            int downloadSize = 0;
            for (int i = 0; i < m_RoutineArr.Length; i++)
            {
                downloadSize += m_RoutineArr[i].DownloadSize;
            }
            return downloadSize;
        }
    }
    //已经下载的时间
    private float m_AlreadyTime = 0f;
    //剩余的时间
    private float m_NeedTime = 0f;
    //下载速度
    private float m_Speed = 0f;
    protected override void OnStart()
    {
        base.OnStart();
        //真正的运行
        StartCoroutine(DownloadVersion(m_VersionUrl));
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        //如果需要下载的数量大于0 并且还没有下载完成
        if (TotalCount > 0 && !m_IsDownloadOver)
        {
            //当前下载数量
            int totalCompleteCount = CurrCompleteTotalCount();
            totalCompleteCount = totalCompleteCount == 0 ? 1 : totalCompleteCount;
            //当前下载的大小
            int totalCompleteSize = CurrCompleteTotalSize();
            //已经下载的时间
            m_AlreadyTime += Time.deltaTime;
            m_Speed = totalCompleteSize / m_AlreadyTime;
            ////计算下载剩余时间 = (总大小 - 已下载大小)/速度
            if (m_Speed > 0)
            {
                m_NeedTime = (TotalSize - totalCompleteSize) / m_Speed;
            }
            string str = string.Format("正在下载资源{0}/{1}    {2}Mb/{3}Mb  下载速度{4}Kb/s  预计剩余时间{5}秒", totalCompleteCount, TotalCount, (DownloadSize/1024f).ToString("0.00"), (TotalSize/1024f).ToString("0.00"),m_Speed.ToString("0.00"), m_NeedTime.ToString("0.00"));
            SceneInitCtrl.Instance.SetProgress(str, totalCompleteSize / (float)TotalSize);
            if (totalCompleteCount == TotalCount)
            {
                m_IsDownloadOver = true;
                SceneInitCtrl.Instance.SetProgress("资源更新完毕", 1);
                if (DownloadManager.Instance.OnInitComplete != null)
                {
                    DownloadManager.Instance.OnInitComplete();
                }
            }
        }
    }

    /// <summary> 初始化服务器版本信息 </summary>
    /// <param name="url">资源地址</param>
    /// <param name="onInitVersion">初始化服务器版本信息委托</param>
    public void InitServerVersion(string url,Action<List<DownloadDataEntity>> onInitVersion)
    {
        m_VersionUrl = url;
        m_OnInitVersion = onInitVersion;
    }

    /// <summary> 加载版本信息文件 </summary>
    /// <param name="url">版本信息文件url</param>
    /// <returns></returns>
    private IEnumerator DownloadVersion(string url)
    {
        WWW www = new WWW(url);
        float timeOut = Time.time;
        float progress = www.progress;
        while (www!=null && !www.isDone)
        {
            if (progress < www.progress)
            {
                timeOut = Time.time;
                progress = www.progress;
            }
            if ((Time.time - timeOut) > AppConst.DownloadTimeOut)
            {
                Debuger.LogError("下载超时 url:"+url);
                yield break;
            }
        }
        yield return www;
        if (www != null && www.error == null)
        {
            string content = www.text;
            if (m_OnInitVersion != null)
                m_OnInitVersion(DownloadManager.Instance.PackDownloadData(content));
        }
        else
            Debug.LogError("下载失败 原因:" + www.error +"   url:"+url);
    }

    /// <summary> 下载文件 </summary>
    /// <param name="downloadList">要下载的文件信息列表</param>
    public void DownloadFiles(List<DownloadDataEntity> downloadList)
    {
        TotalSize = 0;
        TotalCount = 0;
        //初始化下载器
        for (int i = 0; i < m_RoutineArr.Length; i++)
        {
            if (m_RoutineArr[i] == null)
            {
                m_RoutineArr[i] = gameObject.AddComponent<AssetBundleDownloadRoutine>();
            }
        }
        //循环的给下载器分配下载任务
        for (int i = 0; i < downloadList.Count; i++)
        {
            m_RoutineIndex = m_RoutineIndex % m_RoutineArr.Length;
            m_RoutineArr[m_RoutineIndex].AddDownload(downloadList[i]);
            m_RoutineIndex++;
            TotalSize += downloadList[i].Size;
            TotalCount++;
        }
        for (int i = 0; i < m_RoutineArr.Length; i++)
        {
            if (m_RoutineArr[i] == null) continue;
            m_RoutineArr[i].StartDownload(i + 1);
        }
    }

    /// <summary> 当前已经下载的文件总数量 </summary>
    /// <returns></returns>
    public int CurrCompleteTotalCount()
    {
        int count = 0;
        for (int i = 0; i < m_RoutineArr.Length; i++)
        {
            if (m_RoutineArr[i] == null) continue;
            count += m_RoutineArr[i].CompleteCount;
        }
        return count;
    }
    /// <summary> 当前已经下载的文件总大小 </summary>
    /// <returns></returns>
    public int CurrCompleteTotalSize()
    {
        int size = 0;
        for (int i = 0; i < m_RoutineArr.Length; i++)
        {
            if (m_RoutineArr[i] == null) continue;
            size += m_RoutineArr[i].DownloadSize;
        }
        return size;
    }

    /// <summary> 动态下载更新 </summary>
    /// <param name="currDownloadData">当前需要下载的文件</param>
    /// <param name="onComplete">下载完成回调 bool 下载成功或失败</param>
    /// <returns></returns>
    public IEnumerator DownloadData(DownloadDataEntity currDownloadData,Action<bool> onComplete)
    {
        string dataUrl = DownloadManager.Instance.DownLoadUrl + currDownloadData.FullName;
        //段路径 用于创建文件夹
        string path = currDownloadData.FullName.Substring(0, currDownloadData.FullName.LastIndexOf('\\'));
        //得到本地路径  用\\移动端创建不出来文件夹 path.Replace('\\','/')
        string localFilePath = DownloadManager.Instance.LocalFilePath + path.Replace('\\','/');
        IOUtil.CheckDirectoryPath(localFilePath);
        WWW www = new WWW(dataUrl);
        float timeOut = Time.time;
        float progress = www.progress;
        while (www != null && !www.isDone)
        {
            if (progress < www.progress)
            {
                timeOut = Time.time;
                progress = www.progress;
            }
            if ((Time.time - timeOut) > AppConst.DownloadTimeOut)
            {
                Debug.Log("下载失败 超时 Path:" + dataUrl);
                if (onComplete != null)
                    onComplete(false);
                yield break;
            }
            yield return null;
        }
        if (www != null && www.error == null)
        {
            using (FileStream fs = new FileStream(DownloadManager.Instance.LocalFilePath + currDownloadData.FullName,FileMode.Open,FileAccess.ReadWrite))
            {
                fs.Write(www.bytes, 0, www.bytes.Length);
            }
        }
        //写入本地文件
        DownloadManager.Instance.ModifyLocalData(currDownloadData);
        if (onComplete!=null)
        {
            onComplete(true);
        }
    }
}
