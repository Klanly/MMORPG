using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Menu  {
    
    /// <summary>
    /// 自定义宏设置
    /// </summary>
    [MenuItem("Tools/全局设置")]
    public static void Settings()
    {
        SettingsWindow win = EditorWindow.GetWindow(typeof(SettingsWindow)) as SettingsWindow;
        win.titleContent = new GUIContent("全局设置");
        win.Show();
    }

    /// <summary> 打包 </summary>
    [MenuItem("Tools/资源管理/资源打包")]
    public static void AssetBundleCreate()
    {
        AssetBundleWindow win = EditorWindow.GetWindow<AssetBundleWindow>();
        win.titleContent = new GUIContent("资源打包");
        win.Show();
    }

    /// <summary>
    /// 初始资源拷贝到StreamingAsstes
    /// </summary>
    [MenuItem("Tools/资源管理/初始资源拷贝")]
    public static void AssetBundleCopyToStreamingAsstes()
    {
        string toPath = Application.streamingAssetsPath + "/AssetBundles/";
        if (Directory.Exists(toPath))
        {
            Directory.Delete(toPath, true);
        }
        Directory.CreateDirectory(toPath);

        IOUtil.CopyDirectory(Application.persistentDataPath, toPath);
        AssetDatabase.Refresh();

        Debug.Log("拷贝完毕");
    }
}
