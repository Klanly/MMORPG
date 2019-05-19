/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-05 16:44:40 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-05 16:44:40 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

/// <summary> 排序工具类 </summary>
public static class SortUtil{

    /// <summary> 排序列表 </summary>
    /// <typeparam name="T">排序列表类型</typeparam>
    /// <param name="list">列表</param>
    /// <param name="property">排序属性</param>
    /// <param name="isGoUp">是否升序(默认升序)</param>
    public static void Sort<T>(List<T> list,string property,bool isGoUp = true)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            bool isChange = false;
            for (int j = 0; j < list.Count - i - 1; j++)
            {
                int num1 = (int)(list[j].GetType().GetProperty(property).GetValue(list[j],null));
                int num2 = (int)(list[j + 1].GetType().GetProperty(property).GetValue(list[j + 1], null));
                if (isGoUp)
                {
                    if (num1 > num2)
                    {
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                        isChange = true;
                    }
                }
                else
                {
                    if (num1 < num2)
                    {
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                        isChange = true;
                    }
                }
            }
            if (!isChange)
                break;
        }
    }

    /// <summary> 排序数组 </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="property">属性</param>
    /// <param name="isGoUp">是否升序(默认升序)</param>
    public static void Sort<T>(T[] array, string property, bool isGoUp = true)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            bool isChange = false;
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                int num1 = (int)(array[j].GetType().GetProperty(property).GetValue(array[j], null));
                int num2 = (int)(array[j + 1].GetType().GetProperty(property).GetValue(array[j + 1], null));
                if (isGoUp)
                {
                    if (num1 > num2)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        isChange = true;
                    }
                }
                else
                {
                    if (num1 < num2)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        isChange = true;
                    }
                }
            }
            if (!isChange)
                break;
        }

    }

    /// <summary> 对key为int类型的字典排序 </summary>
    /// <typeparam name="T">Value类型</typeparam>
    /// <param name="dic">字典</param>
    /// <param name="property">排序属性</param>
    /// <param name="isGoUp">升序(默认)</param>
    public static void Sort<T>(Dictionary<int,T> dic,string property,bool isGoUp = true)
    {
        for (int i = 0; i < dic.Count - 1; i++)
        {
            bool isChange = false;
            for (int j = 0; j < dic.Count - i - 1; j++)
            {
                int num1 = (int)(dic[j].GetType().GetProperty(property).GetValue(dic[j], null));
                int num2 = (int)(dic[j + 1].GetType().GetProperty(property).GetValue(dic[j + 1], null));
                if (isGoUp)
                {
                    if (num1 > num2)
                    {
                        T temp = dic[j];
                        dic[j] = dic[j + 1];
                        dic[j + 1] = temp;
                        isChange = true;
                    }
                }
                else
                {
                    if (num1 < num2)
                    {
                        T temp = dic[j];
                        dic[j] = dic[j + 1];
                        dic[j + 1] = temp;
                        isChange = true;
                    }
                }
            }
            if (!isChange)
                break;
        }
        
    }
}
