using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
/// <summary> AssetBundle窗口管理 </summary>
public class AssetBundleWindow : EditorWindow {

    /// <summary> AssetBundle 数据读取类 </summary>
    private AssetBundleDAL dal;
    /// <summary> AssetBundle数据实体 </summary>
    private List<AssetBundleEntity> m_List;
    /// <summary> 是否选中 </summary>
    private Dictionary<string, bool> m_Dic;
    /// <summary> 资源标签 </summary>
    private string[] arrTag = { "All", "Scene", "Role", "Effect", "Audio", "UI", "XLuaCode", "Prefab", "Config", "None" };

    /// <summary> 标记的索引 </summary>
    private int tagIndex = 0;
    /// <summary> 选中的标记索引 </summary>
    private int selectTagIndex = -1;
    /// <summary> 平台 </summary>
    private string[] arrBuilTarget = { "Windows", "Android", "iOS" };
    
    /// <summary> 选中的打包平台索引 </summary>
    private int selectBuildTargetIndex = -1;

    private Vector2 pos;

#if UNITY_STANDALONE_WIN

    /// <summary> 平台 </summary>
    private BuildTarget target = BuildTarget.StandaloneWindows;
    /// <summary> 索引 </summary>
    private int buildTargetIndex = 0;

#elif UNITY_ANDROID

    /// <summary> 平台 </summary>
    private BuildTarget target = BuildTarget.Android;
    /// <summary> 索引 </summary>
    private int buildTargetIndex = 1;

#elif UNITY_IPHONE

    /// <summary> 平台 </summary>
    private BuildTarget target = BuildTarget.iOS;
    /// <summary> 索引 </summary>
    private int buildTargetIndex = 2;

#endif
    public void OnEnable()
    {
        string xmlPath = Application.dataPath + @"/Editor/AssetBundle/AssetBundleConfig.xml";
        dal = new AssetBundleDAL(xmlPath);
        m_List = dal.GetList();
        m_Dic = new Dictionary<string, bool>();
        for (int i = 0; i < m_List.Count; i++)
        {
            m_Dic[m_List[i].Key] = true;
        }
    }

    private void OnGUI()
    {
        if (m_List == null) return;
        #region ------ 按钮行 ------
        GUILayout.BeginHorizontal("Box");
        selectTagIndex = EditorGUILayout.Popup(tagIndex, arrTag, GUILayout.Width(100));
        if (selectTagIndex!=tagIndex)
        {
            tagIndex = selectTagIndex;
            EditorApplication.delayCall = OnSelectTagCallBack;
        }
        selectBuildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, arrBuilTarget, GUILayout.Width(100));
        if (selectBuildTargetIndex != buildTargetIndex)
        {
            buildTargetIndex = selectBuildTargetIndex;
            EditorApplication.delayCall = OnSelectTargetCallBack;
        }
        if (GUILayout.Button("清空AssetBundle",GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallBack;
        }

        if (GUILayout.Button("保存设置",GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnSaveAssetBundleCallBack;
        }
        if (GUILayout.Button("打AssetBundle包", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnCreateAssetBundleCallBack;
        }
        if (GUILayout.Button("拷贝数据表", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnCopyDataTableCallBack;
        }
        if (GUILayout.Button("生成版本文件", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnCreateVersionTextCallBack;
        }
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
        #endregion

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("包名");
        GUILayout.Label("标记", GUILayout.Width(100));
        GUILayout.Label("文件夹", GUILayout.Width(200));
        GUILayout.Label("初始资源", GUILayout.Width(200));
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();

        pos = GUILayout.BeginScrollView(pos);

        for (int i = 0; i < m_List.Count; i++)
        {
            AssetBundleEntity entity = m_List[i];
            GUILayout.BeginHorizontal("box");

            m_Dic[entity.Key] = GUILayout.Toggle(m_Dic[entity.Key], "", GUILayout.Width(20));
            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.IsFolder.ToString(), GUILayout.Width(200));
            GUILayout.Label(entity.IsFirstData.ToString(), GUILayout.Width(200));
            GUILayout.EndHorizontal();

            foreach (var path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    #region ------ 回调 ------
    /// <summary> 选定Tag回调 </summary>
    private void OnSelectTagCallBack()
    {
        switch (tagIndex)
        {
            case 0:
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = true;
                }
                break;
            case 1:
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 2:
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Role", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 3:
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Effect",  StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 4:
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Audio", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 5:  //图片 UI
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("UI", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 6:  //XLua代码 XLuaCode
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("XLuaCode", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 7:  //预制体 Prefab
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Prefab", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 8:  //预制体 Prefab
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Config", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 9:  //空 None
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = false;
                }
                break;
        }
    }
    /// <summary> 选定平台回调 </summary>
    private void OnSelectTargetCallBack()
    {
        switch (buildTargetIndex)
        {
            case 0:  //PC平台
                target = BuildTarget.StandaloneWindows64;
                break;
            case 1:  //a安卓平台
                target = BuildTarget.Android;
                break;
            case 2:  //ios平台
                target = BuildTarget.iOS;
                break;
        }
    }

    /// <summary> 保存设置回调 </summary>
    private void OnSaveAssetBundleCallBack()
    {
        //需要打包的对象
        List<AssetBundleEntity> list = new List<AssetBundleEntity>();
        foreach (AssetBundleEntity entity in m_List)
        {
            if (m_Dic[entity.Key])
            {
                entity.IsChecked = true;
                list.Add(entity);
            }
            else
            {
                entity.IsChecked = false;
                list.Add(entity);
            }
        }
        //循环设置文件夹(包括子文件夹里面的项)
        for (int i = 0; i < list.Count; i++)
        {
            AssetBundleEntity entity = list[i];

            if (entity.IsFolder)
            {
                //如果这个节点配置是一个文件夹,那么需要遍历文件夹
                //需要把路径变成绝对路径
                string[] folderArr = new string[entity.PathList.Count];
                for (int k = 0; k < entity.PathList.Count; k++)
                {
                    folderArr[k] = Application.dataPath + "/" + entity.PathList[k];
                }
                SaveFolderSettings(folderArr, !entity.IsChecked);
            }
            else
            {
                //如果不是文件夹只需要设置里面的项
                string[] arrFiles = new string[entity.PathList.Count];
                for (int k = 0; k < entity.PathList.Count; k++)
                {
                    arrFiles[k] = Application.dataPath + "/" + entity.PathList[k];
                    SaveFileSetting(arrFiles[k], !entity.IsChecked);
                }
            }
        }
    }

    /// <summary> 批量设置标签(文件夹) </summary>
    /// <param name="folderArr">路径数组</param>
    /// <param name="isSetNull">是否设置为空</param>
    private void SaveFolderSettings(string[]folderArr,bool isSetNull)
    {
        foreach (string folderPath in folderArr)
        {
            //1.先获取路径下的所有文件
            string[] arrFiles = Directory.GetFiles(folderPath);
            //2.对文件进行标记
            foreach (var filePath in arrFiles)
            {
                SaveFileSetting(filePath, isSetNull);
            }
            //3.获取该路径下的所有文件夹
            string[] arrFolders = Directory.GetDirectories(folderPath);
            //4.对文件夹数组进行标记
            SaveFolderSettings(arrFolders, isSetNull);
        }
    }

    /// <summary> 设置文件标签 </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="isSetNull">是否设置为空</param>
    private void SaveFileSetting(string filePath,bool isSetNull)
    {
        if (filePath.IndexOf(".")!=-1)
        {
            #region ------ 如果包含.则说明是文件 ------
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
            {
                int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);
                //路径
                string newPath = filePath.Substring(index);
                //文件名
                string fileName = newPath.Replace("Assets/", "").Replace(fileInfo.Extension, "");
                //后缀
                string variant = fileInfo.Extension.Equals(".unity", StringComparison.CurrentCultureIgnoreCase) ? "unity3D" : "assetbundle";
                AssetImporter import = AssetImporter.GetAtPath(newPath);
                import.SetAssetBundleNameAndVariant(fileName, variant);
                if (isSetNull)
                {
                    import.SetAssetBundleNameAndVariant(null, null);
                }
                import.SaveAndReimport();
            }
            //else
            //    return;
            #endregion
        }
        else
        {
            //说明是把文件夹当成一个资源来打(UI图集)
            int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);
            //路径
            string newPath = filePath.Substring(index);
            //文件名
            string fileName = newPath.Replace("Assets/", "");
            //后缀
            string variant = "assetbundle";
            AssetImporter import = AssetImporter.GetAtPath(newPath);
            import.SetAssetBundleNameAndVariant(fileName, variant);
            if (isSetNull)
            {
                import.SetAssetBundleNameAndVariant(null, null);
            }
            import.SaveAndReimport();
        }
        
        Debug.Log("保存设置成功");
    }

    /// <summary> 打AssetBundle包回调 </summary>
    private void OnCreateAssetBundleCallBack()
    {
        //需要打包的对象
        //List<AssetBundleEntity> needBuildList = new List<AssetBundleEntity>();
        //foreach (AssetBundleEntity entity in m_List)
        //{
        //    if (m_Dic[entity.Key])
        //    {
        //        needBuildList.Add(entity);
        //    }
        //}

        //for (int i = 0; i < needBuildList.Count; i++)
        //{
        //    Debug.LogFormat("正在打包{0}/{1}", i + 1, needBuildList.Count);
        //    BuildAssetBundle(needBuildList[i]);
        //}
        //Debug.Log("打包完毕");

        //打包路径
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuilTarget[buildTargetIndex];
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }
        //打包
        BuildPipeline.BuildAssetBundles(toPath, BuildAssetBundleOptions.None, target);
        Debug.Log("打包完毕");
        AssetDatabase.Refresh();
    }
    
    /// <summary> 清空AssetBuncle </summary>
    private void OnClearAssetBundleCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuilTarget[buildTargetIndex];
        if (Directory.Exists(path))
            Directory.Delete(path, true);
        Debug.Log("清空完毕");
    }

    //private void BuildAssetBundle(AssetBundleEntity entity)
    //{
    //    AssetBundleBuild[] arrBuild = new AssetBundleBuild[1];
    //    AssetBundleBuild build = new AssetBundleBuild();
    //    //包名
    //    build.assetBundleName = string.Format("{0}.{1}", entity.Name, (entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle"));
    //    //后缀
    //    //build.assetBundleVariant = (entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle");
    //    //资源路径
    //    build.assetNames =entity.PathList.ToArray();
    //    arrBuild[0] = build;
    //    string toPath = Application.dataPath + "/../AssetBundles/" + arrBuilTarget[buildTargetIndex] + entity.ToPath;
    //    Debug.Log(toPath);
    //    if (!Directory.Exists(toPath))
    //    {
    //        Directory.CreateDirectory(toPath);
    //    }
    //    BuildPipeline.BuildAssetBundles(toPath, arrBuild, BuildAssetBundleOptions.None, target);
    //}

    /// <summary> 拷贝数据表回调 </summary>
    private void OnCopyDataTableCallBack()
    {
        string fromPath = Application.dataPath + "/Download/DataTable";
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuilTarget[buildTargetIndex] + "/Download/DataTable";
        IOUtil.CopyDirectory(fromPath, toPath);
        Debug.Log("拷贝数据表完毕");
    }


    /// <summary> 创建版本文件回调 </summary>
    private void OnCreateVersionTextCallBack()
    {
        string Path = Application.dataPath + "/../AssetBundles/" + arrBuilTarget[buildTargetIndex];
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }
        //版本文件路径
        string strVersionFilePath = Path+ "/VersionFile.txt";
        //如果文件存在 则删除
        IOUtil.DeleteFile(strVersionFilePath);
        StringBuilder sbContent = new StringBuilder();
        DirectoryInfo directoryInfo = new DirectoryInfo(Path);
        //返回文件目录包括子文件夹内的所有文件
        FileInfo[] arrFileInfos = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
        for (int i = 0; i < arrFileInfos.Length; i++)
        {
            FileInfo fileInfo = arrFileInfos[i];
            string fullName = fileInfo.FullName;//全名(包含路径和扩展名)
            //相对路径
            string name = fullName.Substring(fullName.IndexOf(arrBuilTarget[buildTargetIndex]) + arrBuilTarget[buildTargetIndex].Length + 1);
            //获取文件的md5值
            string md5 = EncryptUtil.GetFileMd5(fullName);
            if (md5 == null)
            {
                Debuger.Log("Md5值为空 FilePath = " + fullName);
                continue;
            }
            //文件大小
            string fileSize = Math.Ceiling(fileInfo.Length / 1024f).ToString();
            //是否初始数据
            bool isFirstData = true;
            //是否跳出循环
            bool isBreak = false;
            for (int k = 0; k < m_List.Count; k++)
            {
                foreach (var xmlPath in m_List[k].PathList)
                {
                    string tempPath = xmlPath;
                    if (xmlPath.IndexOf(".") != -1)
                    {
                        tempPath = xmlPath.Substring(0, xmlPath.IndexOf("."));
                    }
                    if (name.IndexOf(tempPath, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        isFirstData = m_List[k].IsFirstData;
                        isBreak = true;
                        break;
                    }
                }
                if (isBreak) break;
            }

            if (name.IndexOf("DataTable") != -1)
            {
                isFirstData = true;
            }
            string strLine = string.Format("{0} {1} {2} {3}", name, md5, fileSize, isFirstData ? 1 : 0);
            sbContent.AppendLine(strLine);
        }
        IOUtil.CreateTextFile(strVersionFilePath, sbContent.ToString());
        Debug.Log("创建版本文件成功");
    }
    #endregion
}
