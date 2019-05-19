using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageUtil  {

    public static string LanguageType = "Chinese";
    private static Dictionary<string, Dictionary<int, string>> m_AllLanguageDic = new Dictionary<string, Dictionary<int, string>>();

    /// <summary> 初始化语言 默认中文 </summary>
    public static void Init()
    {
        TextAsset chineseText = null;
        if (!m_AllLanguageDic.ContainsKey("Chinese"))
        {
            chineseText = Resources.Load<TextAsset>(string.Format("{0}", LanguageType));
        }
        PraseText(chineseText.text);
    }
    /// <summary> 通过Id获取字符串 </summary>
    /// <param name="tipsId">字符串Id</param>
    /// <returns></returns>
    public static string GetStrById(int tipsId)
    {
        try
        {
            return m_AllLanguageDic[LanguageType][tipsId];
        }
        catch
        {
            return tipsId.ToString();
        }
        
    }

    private static void PraseText(string languageText)
    {
        string[] allLineStr = languageText.Split('\n');
        Dictionary<int, string> languageDic = new Dictionary<int, string>();
        for (int i = 0; i < allLineStr.Length; i++)
        {
            if (allLineStr[i].Contains("="))
            {
                string[] oneLineStr = allLineStr[i].Split('=');
                try
                {
                    int id = int.Parse(oneLineStr[0]);
                    oneLineStr[1] = oneLineStr[1].Trim();
                    languageDic.Add(id, oneLineStr[1].Substring(1,oneLineStr[1].Length-2));
                }
                catch
                {
                    Debuger.LogError(oneLineStr[0] + "is can not be converted to int");
                }
            }
        }
        m_AllLanguageDic.Add(LanguageType, languageDic);
    }
}
