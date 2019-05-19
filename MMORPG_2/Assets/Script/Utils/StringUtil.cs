using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 
/// </summary>
public static class StringUtil 
{
    #region ------ 类型转换 ------
    /// <summary> 扩展方法(string转int) </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str)
    {
        int temp = 0;
        int.TryParse(str, out temp);
        return temp;
    }
    public static float ToFloat(this string str)
    {
        float temp = 0;
        float.TryParse(str, out temp);
        return temp;
    }

    /// <summary> string转string[] </summary>
    public static string[] ToStringArray(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return null;
        str = str.Substring(1, str.Length - 2);
        string[] result = str.Split(',');
        return result;
    }
    /// <summary> string转int[] </summary>
    public static int[] ToIntArray(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return null;
        str = str.Substring(1, str.Length - 2);
        int[] result = str.Split(',').ToIntArray();
        return result;
    }
    /// <summary> string[]转int[] </summary>
    public static int[] ToIntArray(this string[] strArray)
    {
        if (strArray == null)
            return null;
        int[] resultArray = new int[strArray.Length];
        for (int i = 0; i < strArray.Length; i++)
        {
            resultArray[i] = int.Parse(strArray[i]);
        }
        return resultArray;
    }
    /// <summary> string转float[] </summary>
    public static float[] ToFloatArray(this string str)
    {
        if (str == null)
            return null;
        str = str.Substring(1, str.Length - 2);
        float[] result = str.Split(',').ToFloatArray();
        return result;
    }
    /// <summary> string[]转float[] </summary>
    public static float[] ToFloatArray(this string[] strArray)
    {
        if (strArray == null)
            return null;
        float[] resultArray = new float[strArray.Length];
        for (int i = 0; i < strArray.Length; i++)
        {
            resultArray[i] = float.Parse(strArray[i]);
        }
        return resultArray;
    }
    /// <summary> string转string[][] </summary>
    public static string[][] ToDoubleStringArray(this string str)
    {
        if (str == null)
            return null;
        string[] splitArray = { "},{" };
        string[] result1 = str.Split(splitArray, StringSplitOptions.None);

        string[][] result = new string[result1.Length][];
        for (int i = 0; i < result.Length; i++)
        {
            result1[i] = result1[i].Replace("{", "").Replace("{", "");
            string[] tempStrArray = result1[i].Split(',');
            result[i] = tempStrArray;
        }
        return result;
    }
    /// <summary> string转int[][] </summary>
    public static int[][] ToDoubleIntArray(this string str)
    {
        if (str == null)
            return null;
        string[] splitArray = { "},{" };
        string[] result1 = str.Split(splitArray, StringSplitOptions.None);

        int[][] result = new int[result1.Length][];
        for (int i = 0; i < result.Length; i++)
        {
            result1[i] = result1[i].Replace("{", "").Replace("}", "");
            int[] tempStrArray = result1[i].Split(',').ToIntArray();
            result[i] = tempStrArray;
        }
        return result;
    }
    /// <summary> string转float[][] </summary>
    public static float[][] ToDoubleFloatArray(this string str)
    {
        if (str == null)
            return null;
        string[] splitArray = { "},{" };
        string[] result1 = str.Split(splitArray, StringSplitOptions.None);

        float[][] result = new float[result1.Length][];
        for (int i = 0; i < result.Length; i++)
        {
            result1[i] = result1[i].Replace("{", "").Replace("}", "");
            float[] tempStrArray = result1[i].Split(',').ToFloatArray();
            result[i] = tempStrArray;
        }
        return result;
    }
    #endregion

    /// <summary> 获取富文本字符串 </summary>
    /// <param name="textId">字符串Id</param>
    /// <param name="color">颜色</param>
    /// <returns></returns>
    public static string GetRichString(int textId, string color)
    {
        return "<color=" + color + ">" + LanguageUtil.GetStrById(textId) + "</color>";
    }
    /// <summary> 获取富文本字符串 </summary>
    /// <param name="textId">字符串Id</param>
    /// <returns></returns>
    public static string GetStringById(int textId)
    {
        return LanguageUtil.GetStrById(textId);
    }

    /// <summary> 获取富文本字符串 </summary>
    /// <param name="text">要更改的字符串</param>
    /// <param name="color">颜色</param>
    /// <returns></returns>
    public static string GetRichString(string text, string color)
    {
        return "<color=" + color + ">" + text + "</color>";
    }
}