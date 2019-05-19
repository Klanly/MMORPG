using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary> 加密工具类 </summary>
public class EncryptUtil {


    /// <summary> 获取字符串的Md5值 </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string Md5(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }
        System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bytResult = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(value));
        string strResult = BitConverter.ToString(bytResult);
        strResult = strResult.Replace("-", "");
        return strResult;
    }

    /// <summary> 获取文件的Md5值 </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string GetFileMd5(string filePath)
    {
        if (string.IsNullOrEmpty(filePath)|| !File.Exists(filePath))
        {
            Debuger.Log("路径不存在...FilePath = "+filePath);
            return null;
        }
        try
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytResult = md5.ComputeHash(fileStream);
            string strResult = BitConverter.ToString(bytResult);
            strResult = strResult.Replace("-", "");
            return strResult;
        }
        catch
        {
            return null;
        }
    }
}
