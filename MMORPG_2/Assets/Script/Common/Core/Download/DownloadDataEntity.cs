using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 下载资源数据的实体 </summary>
public class DownloadDataEntity  {

    /// <summary> 名称 </summary>
	public string FullName { get; set; }
    /// <summary> MD5值 </summary>
    public string MD5 { get; set; }
    /// <summary> 大小 </summary>
    public int Size { get; set; }
    /// <summary> 是否初始化资源数据 </summary>
    public bool IsFirstData { get; set; }
}
