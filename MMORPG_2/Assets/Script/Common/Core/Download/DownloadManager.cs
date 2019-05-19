using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DownloadManager : Singleton<DownloadManager> {


#if UNITY_ANDROID
    /// <summary>
    /// 资源下载路径
    /// </summary>
    public string DownLoadUrl = AppConst.ServerUrl + "Android/";
#elif UNITY_IPHONE
    /// <summary>
    /// 资源下载路径
    /// </summary>
    public string DownLoadUrl = AppConst.ServerUrl + "iOS/";
#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
    /// <summary>
    /// 资源下载路径
    /// </summary>
    public string DownLoadUrl = AppConst.IPServerUrl + "Windows/";
#endif

    public string LocalFilePath = Application.persistentDataPath + "/";

    /// <summary> 需要下载的资源数据列表 </summary>
    private List<DownloadDataEntity> m_NeedDownloadDataList = new List<DownloadDataEntity>();

    /// <summary> 本地资源数据列表 </summary>
    private List<DownloadDataEntity> m_LocalDataList = new List<DownloadDataEntity>();

    /// <summary> 服务器资源数据列表 </summary>
    private List<DownloadDataEntity> m_ServerDataList;

    /// <summary> 本地的版本文件 </summary>
    private string m_LocalVersionPath;

    /// <summary> StreamingAssets路径 </summary>
    private string m_StreamingAssetsPath;

    /// <summary> 初始化完成的委托 </summary>
    public Action OnInitComplete;

    /// <summary> 第一步:初始化资源 </summary>
    /// <param name="onInitComplete"></param>
    public void InitStreamingAssets(Action onInitComplete)
    {
        OnInitComplete = onInitComplete;
        m_LocalVersionPath = LocalFilePath + AppConst.VersionFileName;
        //判断本地是否已经有资源
        if (File.Exists(m_LocalVersionPath))
        {
            //有资源 则检查更新
            InitCheckVersion();
        }
        else
        {
            //没有资源 执行初始化,然后在检查更新
            m_StreamingAssetsPath = "file:///" + Application.streamingAssetsPath + "/AssetBundles";
#if UNITY_ANDROID && !UNITY_EDITOR
            m_StreamingAssetsPath = Application.streamingAssetsPath + "/AssetBundles/";
#endif
            string versionFileUrl = m_StreamingAssetsPath + AppConst.VersionFileName;
            GlobalInit.Instance.StartCoroutine(ReadStreamingAssetVersionFile(versionFileUrl, OnReadStreamingAssetOver));
        }
    }

    /// <summary> 读取初始资源目录的版本文件 </summary>
    /// <param name="fileUrl"></param>
    /// <param name="onReadStreaminAssetOver"></param>
    /// <returns></returns>
    private IEnumerator ReadStreamingAssetVersionFile(string fileUrl, Action<string> onReadStreamingAssetOver)
    {
        SceneInitCtrl.Instance.SetProgress("正在准备进行资源初始化", 0);
        using (WWW www = new WWW(fileUrl))
        {
            yield return www;
            if (www.error == null)
            {
                if (onReadStreamingAssetOver != null)
                {
                    onReadStreamingAssetOver(Encoding.UTF8.GetString(www.bytes));
                }
            }
            else
            {
                //Debuger.Log("www is error FileUrl:" + fileUrl);
                //StreamingAssets文件夹中不包含 fileUrl 文件传空字符串
                onReadStreamingAssetOver("");
            }
        }
    }
    /// <summary>
    /// 读取版本文件完毕
    /// </summary>
    /// <param name="obj"></param>
    private void OnReadStreamingAssetOver(string content)
    {
        GlobalInit.Instance.StartCoroutine(InitStreamingAssetList(content));
    }

    /// <summary>
    /// 初始化资源清单
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private IEnumerator InitStreamingAssetList(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            //本地没有版本文件
            InitCheckVersion();
            yield break;
        }
        string[] arr = content.Split('\n');
        //循环解压
        for (int i = 0; i < arr.Length; i++)
        {
            string[] arrInfo = arr[i].Split(' ');
            string filUrl = arrInfo[0];//短路径
            yield return GlobalInit.Instance.StartCoroutine(AssetLoadToLocal(m_StreamingAssetsPath + filUrl,LocalFilePath + filUrl));
            float value = (i + 1) / (float)arr.Length;
            SceneInitCtrl.Instance.SetProgress(string.Format("初始化资源不消耗流量{0}/{1}", i + 1, arr.Length), value);
        }
        //解压版本文件
        yield return GlobalInit.Instance.StartCoroutine(AssetLoadToLocal(m_StreamingAssetsPath + AppConst.VersionFileName,
                LocalFilePath + AppConst.VersionFileName
                ));
        //检查更新
        InitCheckVersion();
    }
    /// <summary> 解压某个文件到本地 </summary>
    /// <param name="fileUrl">文件Url</param>
    /// <param name="toPath">本地路径</param>
    /// <returns></returns>
    private IEnumerator AssetLoadToLocal(string fileUrl,string toPath)
    {
        using (WWW www = new WWW(fileUrl))
        {
            yield return www;
            if (www.error == null)
            {
                int lastIndexOf = toPath.LastIndexOf('\\');
                if (lastIndexOf != -1)
                {
                    //出去文件名以外的路径
                    string localPath = toPath.Substring(0, lastIndexOf);
                    if (!Directory.Exists(localPath))
                    {
                        Directory.CreateDirectory(localPath);
                    }
                }
                using (FileStream fs = File.Create(toPath,www.bytes.Length))
                {
                    fs.Write(www.bytes, 0, www.bytes.Length);
                    fs.Close();
                }
            }
        }
    }
    private void InitCheckVersion()
    {
        SceneInitCtrl.Instance.SetProgress("正在检查版本更新", 0);
        //资源版本文件路径
        string strVersionUrl = DownLoadUrl + AppConst.VersionFileName;
        //读取版本文件
        AssetBundleDownload.Instance.InitServerVersion(strVersionUrl, OnInitVersionCallBack);
    }

    /// <summary> 初始化版本文件回调 </summary>
    /// <param name="obj"></param>
    private void OnInitVersionCallBack(List<DownloadDataEntity> serverDownloadData)
    {
        //得到服务端数据列表
        m_ServerDataList = serverDownloadData;
        if (File.Exists(m_LocalVersionPath))
        {
            //如果本地存在版本文件 则和服务器端的进行对比
            //服务器端的版本文件字典
            Dictionary<string, string> serverDic = PackDownloadDataDic(serverDownloadData);
            //获取客户端的版本文件字典
            string content = IOUtil.GetFileText(m_LocalVersionPath);
            Dictionary<string, string> clientDic = PackDownloadDataDic(content);
            m_LocalDataList = PackDownloadData(content);
            //1、查找新加的初始资源
            for (int i = 0; i < serverDownloadData.Count; i++)
            {
                if (serverDownloadData[i].IsFirstData && !clientDic.ContainsKey(serverDownloadData[i].FullName))
                {
                    //加入下载列表
                    m_NeedDownloadDataList.Add(serverDownloadData[i]);
                }
            }
            //2、对比已经下载过的 但是有更新的资源
            foreach (var item in clientDic)
            {
                //如果md5不一致
                if (serverDic.ContainsKey(item.Key) && serverDic[item.Key] != item.Value)
                {
                    DownloadDataEntity entity = GetDownloadData(item.Key, serverDownloadData);
                    if (entity != null)
                    {
                        m_NeedDownloadDataList.Add(entity);
                    }
                }
            }
        }
        else
        {
            //如果不存在 则初始资源就是需要下载的文件
            for (int i = 0; i < serverDownloadData.Count; i++)
            {
                if (serverDownloadData[i].IsFirstData)
                {
                    m_NeedDownloadDataList.Add(serverDownloadData[i]);
                }
            }
        }
        if (m_NeedDownloadDataList.Count == 0)
        {
            SceneInitCtrl.Instance.SetProgress("资源更新完毕", 1);
            if (OnInitComplete!=null)
            {
                OnInitComplete();
            }
            return;
        }
        AssetBundleDownload.Instance.DownloadFiles(m_NeedDownloadDataList);
    }

    /// <summary> 根据资源名称获取资源实体 </summary>
    /// <param name="fullName"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    private DownloadDataEntity GetDownloadData(string fullName,List<DownloadDataEntity> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].FullName.Equals(fullName,StringComparison.CurrentCultureIgnoreCase))
            {
                return list[i];
            }
        }
        return null;
    }

    /// <summary> 封装字典 </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private Dictionary<string, string> PackDownloadDataDic(List<DownloadDataEntity> list)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        for (int i = 0; i < list.Count; i++)
        {
            dic[list[i].FullName] = list[i].MD5;
        }
        return dic;
    }
    /// <summary> 封装字典 </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private Dictionary<string, string> PackDownloadDataDic(string content)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        string[] arrLines = content.Split('\n');
        for (int i = 0; i < arrLines.Length; i++)
        {
            string[] arrData = arrLines[i].Split(' ');
            if (arrData.Length == 4)
            {
                dic[arrData[0]] = arrData[1];
            }
        }
        return dic;
    }

    /// <summary> 根据文本内容 封装下载数据列表 </summary>
    /// <param name="content">文本内容</param>
    /// <returns></returns>
    public List<DownloadDataEntity> PackDownloadData(string content)
    {
        List<DownloadDataEntity> list = new List<DownloadDataEntity>();
        string[] arrLines = content.Split('\n');
        for (int i = 0; i < arrLines.Length; i++)
        {
            string[] arrData = arrLines[i].Split(' ');
            if (arrData.Length == 4)
            {
                DownloadDataEntity entity = new DownloadDataEntity();
                entity.FullName = arrData[0];
                entity.MD5 = arrData[1];
                entity.Size = arrData[2].ToInt();
                entity.IsFirstData = arrData[3].ToInt() == 1;
                list.Add(entity);
            }
        }
        return list;
    }

    /// <summary> 修改本地文件 </summary>
    /// <param name="entity"></param>
    public void ModifyLocalData(DownloadDataEntity entity)
    {
        if (m_LocalDataList == null) return;
        bool isExists = false;
        for (int i = 0; i < m_LocalDataList.Count; i++)
        {
            if (m_LocalDataList[i].FullName.Equals(entity.FullName,StringComparison.CurrentCultureIgnoreCase))
            {
                m_LocalDataList[i].MD5 = entity.MD5;
                m_LocalDataList[i].Size = entity.Size;
                m_LocalDataList[i].IsFirstData = entity.IsFirstData;
                isExists = true;
                break;
            }
        }
        if (!isExists)
            m_LocalDataList.Add(entity);
        SaveLocalVersion();
    }

    /// <summary> 保存本地版本文件 </summary>
    private void SaveLocalVersion()
    {
        StringBuilder sbContent = new StringBuilder();
        for (int i = 0; i < m_LocalDataList.Count; i++)
        {
            sbContent.AppendLine(string.Format("{0} {1} {2} {3}", m_LocalDataList[i].FullName, m_LocalDataList[i].MD5, m_LocalDataList[i].Size, m_LocalDataList[i].IsFirstData ? 1 : 0));
        }
        IOUtil.CreateTextFile(m_LocalVersionPath, sbContent.ToString());
    }

    public DownloadDataEntity GetServerData(string path)
    {
        if (m_ServerDataList == null)
        {
            Debuger.Log("Error: m_ServerDataList == null Path:" + path);
            return null;
        }
        for (int i = 0; i < m_ServerDataList.Count; i++)
        {
            if (path.Replace("/", "\\").Equals(m_ServerDataList[i].FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                return m_ServerDataList[i];
            }
        }
        Debuger.Log("Error: Path:" + path);
        return null;
    }
}
