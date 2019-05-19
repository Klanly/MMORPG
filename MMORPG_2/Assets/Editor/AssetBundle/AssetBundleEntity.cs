using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> AssetBundle实体 </summary>
public class AssetBundleEntity {

    /// <summary> 用于打包时选定,唯一的Key </summary>
    public string Key;

    /// <summary> 资源名 </summary>
    public string Name;

    /// <summary> 资源类型 </summary>
    public string Tag;

    /// <summary> 是否是文件夹 </summary>
    public bool IsFolder;

    /// <summary> 是否初始数据资源 </summary>
    public bool IsFirstData;

    /// <summary> 打包保存路径 </summary>
    public string ToPath;

    /// <summary> 是否被选中 </summary>
    public bool IsChecked;

    /// <summary> 路径集合 </summary>
    private List<string> m_PathList = new List<string>();
    /// <summary> 路径集合 </summary>
    public List<string> PathList { get { return m_PathList; } }
}
