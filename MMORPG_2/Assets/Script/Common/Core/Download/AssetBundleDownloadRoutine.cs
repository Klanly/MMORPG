/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-11-13 23:51:13 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-11-13 23:51:13 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary> 下载器 </summary>
public class AssetBundleDownloadRoutine : MonoBehaviour {

    /// <summary> 这个下载器需要下载的文件列表 </summary>
    private List<DownloadDataEntity> m_List = new List<DownloadDataEntity>();

    /// <summary> 当前正在下载的数据 </summary>
    private DownloadDataEntity m_CurrDownloadData;

    private int m_RoutineIndex = -1;

    /// <summary> 需要下载的数量 </summary>
    public int NeedDownloadCount
    {
        get;private set;
    }

    /// <summary> 已经下载完成的数量 </summary>
    public int CompleteCount
    {
        get; private set;
    }

    /// <summary> 已经下载好的文件总大小 </summary>
    private int m_DownloadSize;

    /// <summary> 当前下载的文件大小 </summary>
    private int m_CurrDownloadSize;

    /// <summary> 这个下载器已经下载的大小 </summary>
    public int DownloadSize
    {
        get { return m_DownloadSize + m_CurrDownloadSize; }
    }

    /// <summary> 是否开始下载 </summary>
    public bool IsStartDownload
    {
        get; private set;
    }

    /// <summary> 添加下载对象 </summary>
    /// <param name="entity">要添加的数据实体</param>
    public void AddDownload(DownloadDataEntity entity)
    {
        m_List.Add(entity);
    }

    /// <summary> 开始下载 </summary>
    /// <param name="routineIndex">下载器索引</param>
    public void StartDownload(int routineIndex)
    {
        m_RoutineIndex = routineIndex;
        IsStartDownload = true;
        NeedDownloadCount = m_List.Count;
    }

    private void Update()
    {
        if (IsStartDownload)
        {
            IsStartDownload = false;
            StartCoroutine(DownloadData());
        }
    }

    private IEnumerator DownloadData()
    {
        if (NeedDownloadCount == 0) yield break;
        m_CurrDownloadData = m_List[0];
        //资源下载路径
        string dataUrl = DownloadManager.Instance.DownLoadUrl + m_CurrDownloadData.FullName;
        int lastIndex = m_CurrDownloadData.FullName.LastIndexOf("\\"); //大于-1 说明路径包含了文件夹
        if (lastIndex > -1)
        {
            //文件夹段路径 用于创建文件夹
            string path = m_CurrDownloadData.FullName.Substring(0, lastIndex);
            //得到本地路径
            string localFilePath = DownloadManager.Instance.LocalFilePath + path;
            if (!Directory.Exists(localFilePath))
            {
                Directory.CreateDirectory(localFilePath);
            }
        }
        WWW www = new WWW(dataUrl);
        float timeOut = Time.time;
        float progress = www.progress;
        while (www!=null && !www.isDone)
        {
            if (progress < www.progress)
            {
                timeOut = Time.time;
                progress = www.progress;
                m_CurrDownloadSize = (int)(m_CurrDownloadData.Size*progress);
            }
            if ((Time.time - timeOut)>AppConst.DownloadTimeOut)
            {
                Debuger.LogError("下载失败 超时 path:"+dataUrl);
                yield break;
            }
            yield return null;
        }
        yield return www;
        if (www!=null && www.error == null)
        {
            using (FileStream fs = new FileStream(DownloadManager.Instance.LocalFilePath + m_CurrDownloadData.FullName, FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(www.bytes, 0, www.bytes.Length);
                Debuger.Log("下载器"+m_RoutineIndex + "   " + m_CurrDownloadData.FullName + "   下载完成");
            }
        }
        //下载成功
        m_CurrDownloadSize = 0;
        m_DownloadSize += m_CurrDownloadData.Size;
        //写入本地文件
        DownloadManager.Instance.ModifyLocalData(m_CurrDownloadData);
        m_List.RemoveAt(0);
        CompleteCount++;
        if (m_List.Count == 0)
        {
            m_List.Clear();
        }
        else
        {
            IsStartDownload = true;
        }
    }
}
