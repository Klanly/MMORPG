using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class SettingsWindow : EditorWindow {

    /// <summary> 宏定义列表 </summary>
    private List<MacroItem> m_List = new List<MacroItem>();
    /// <summary> 是否选中 </summary>
    Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();
    private string m_Macro = null;

    private void OnEnable()
    {
        m_Macro = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        m_List.Clear();
        m_List.Add(new MacroItem() { Name = "DEBUG_MODEL",DisplayName = "调试模式",IsDebug = true,IsRelease = false });
        m_List.Add(new MacroItem() { Name = "DEBUG_LOG", DisplayName = "打印日志", IsDebug = true, IsRelease = false });
        m_List.Add(new MacroItem() { Name = "STAT_TD", DisplayName = "开启统计", IsDebug = false, IsRelease = true });
        m_List.Add(new MacroItem() { Name = "DEBUG_ROLESTATE", DisplayName = "调试角色状态", IsDebug = false, IsRelease = true });
        m_List.Add(new MacroItem() { Name = "DISABLE_ASSETBUNDLE", DisplayName = "禁用AssetBundle", IsDebug = false, IsRelease = false });
        m_List.Add(new MacroItem() { Name = "HOTFIX_ENABLE", DisplayName = "热补丁", IsDebug = false, IsRelease = true });
        for (int i = 0; i < m_List.Count; i++)
        {
            if (!string.IsNullOrEmpty(m_Macro) && m_Macro.IndexOf(m_List[i].Name)!=-1)
                m_Dic[m_List[i].Name] = true;
            else
                m_Dic[m_List[i].Name] = false;
        }
    }

    private void OnGUI()
    {
        for (int i = 0; i < m_List.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            m_Dic[m_List[i].Name] = GUILayout.Toggle(m_Dic[m_List[i].Name], m_List[i].DisplayName);
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("保存",GUILayout.Width(100)))
            SaveMacro();

        if (GUILayout.Button("调试模式",GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            SaveMacro();
        }

        if (GUILayout.Button("发布模式",GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
                m_Dic[m_List[i].Name] = m_List[i].IsRelease;
            SaveMacro();
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary> 保存宏 </summary>
    private void SaveMacro()
    {
        m_Macro = string.Empty;
        foreach (var item in m_Dic)
        {
            if (item.Value)
                m_Macro += string.Format("{0};", item.Key);
            
            if (item.Key.Equals("DISABLE_ASSETBUNDLE",System.StringComparison.CurrentCultureIgnoreCase))
            {
                //禁用AssetBundle 就让DownLoad下的场景生效
                EditorBuildSettingsScene[] arrScenes = EditorBuildSettings.scenes;
                for (int i = 0; i < arrScenes.Length; i++)
                {
                    if (arrScenes[i].path.IndexOf("Download",System.StringComparison.CurrentCultureIgnoreCase)>-1)
                        arrScenes[i].enabled = item.Value;
                }
                EditorBuildSettings.scenes = arrScenes;
            }
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android,m_Macro);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macro);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macro);
    }
}

public class MacroItem
{
    /// <summary> 名称 </summary>
    public string Name { get; set; }
    /// <summary> 显示的名称 </summary>
    public string DisplayName { get; set; }
    /// <summary> 是否调试项 </summary>
    public bool IsDebug { get; set; }
    /// <summary> 是否发布项 </summary>
    public bool IsRelease { get; set; }
}
