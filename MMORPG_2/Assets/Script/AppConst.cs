using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppConst {

    public const string IPServerUrl = "http://127.0.0.1:9091/";//服务器地址(DownloadManager)
    public const string SocketIP = "127.0.0.1";
    //public const string IPServerUrl = "http://118.24.215.142:9091/";//服务器地址(DownloadManager)
    //public const string SocketIP = "118.24.215.142";
    public const ushort IPPort = 9092;

#if !DEBUG_LOG
    public const bool DebugMode = false;
#else
    public const bool DebugMode = true;
#endif

    /// <summary> 配置表Data数据路径 </summary>
    public const string DataPath = @"E:\\Project\Unity3D 5.6.2f1\MMORPG_2\Data\{0}";
    /// <summary> 版本文件名称(DownloadManager) </summary>
    public const string VersionFileName = "VersionFile.txt";
    /// <summary> 下载器的数量(DownloadManager) </summary>  
    public const int DownloadRountineNum = 5;
    /// <summary> 超时时间(DownloadManager) </summary>
    public const int DownloadTimeOut = 5;
    /// <summary> 初始化Text提示数量(TextTipsUtil) </summary>
    public const int TextTipsCount = 5;
    /// <summary> Text提示Y轴动画持续时间(TextTips) </summary>
    public const float TextTipsYDuration = 1f;
    /// <summary> Text提示Alpha动画持续时间(TextTips) </summary>
    public const float TextTipsAlphaDuration = 0.5f;
    /// <summary> 怪物死亡多久销毁 </summary>
    public const float MonsterDieDestroyTime = 6f;
}
