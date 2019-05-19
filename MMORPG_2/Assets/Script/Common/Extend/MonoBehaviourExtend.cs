using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class MonoBehaviourExtend  {

    #region Get Or Add Component By Path
    /// <summary>
    /// 通过路径获取或添加组件(可用于父对象通过路径获取子对象组件)
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="go"></param>
    /// <param name="path">路径(为空表示操作自身，否则表示操作子对象)</param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject go, string path = "") where T : Component
    {
        return GetOrAddComponent(go, typeof(T), path) as T;
    }

    /// <summary> 通过类型获取组件 </summary>
    /// <param name="go"></param>
    /// <param name="type">要获取的组件类型</param>
    /// <param name="path">路径(为空表示操作自身，否则表示操作子对象)</param>
    /// <returns></returns>
    public static Component GetOrAddComponent(this GameObject go, System.Type type, string path = "")
    {
        Transform t;
        if (string.IsNullOrEmpty(path))
        {
            t = go.transform;
        }
        else
        {
            t = go.transform.Find(path);
        }
        if (t == null)
        {
            Debuger.LogError(go.name + " not Find GameObject at Path: " + path);
            return null;
        }
        Component ret = t.gameObject.GetComponent(type);
        if (ret == null)
        {
            ret = t.gameObject.AddComponent(type);
        }
        return ret;
    }

    /// <summary> Lua调用 字符串效率最低 C#不用该API用泛型 </summary>
    /// <param name="go"></param>
    /// <param name="typeStr">要获取的组件类型字符串</param>
    /// <param name="path">路径(为空表示操作自身，否则表示操作子对象)</param>
    /// <returns></returns>
    public static Component GetOrAddComponent(this GameObject go, string typeStr, string path = "")
    {
        Transform t;
        if (string.IsNullOrEmpty(path))
        {
            t = go.transform;
        }
        else
        {
            t = go.transform.Find(path);
        }
        if (t == null)
        {
            Debuger.LogError(go.name + " not Find GameObject at Path: " + path);
            return null;
        }
        System.Type type = System.Type.GetType(typeStr);
        Component ret = t.gameObject.GetComponent(type);
        if (ret == null)
        {
            ret = t.gameObject.AddComponent(type);
        }
        return ret;
    }
    #endregion

    #region Get Component By Path

    /// <summary> 通过路径获取组件(父对象获取子对象) </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="trans">父对象Treansform</param>
    /// <param name="path">路径</param>
    /// <returns></returns>
    public static T GetComponent<T>(this Transform trans, string path) where T : Component
    {
        Transform temp = trans;
        if (string.IsNullOrEmpty(path) == false)
        {
            temp = trans.Find(path);
            if (temp == null)
            {
                Debuger.LogError(trans.name + " not Find Transform at Path: " + path);
                return null;
            }
        }
        return temp.GetComponent<T>();
    }
    /// <summary> 通过路径获取组件(父对象获取子对象) </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="go">父对象</param>
    /// <param name="path">路径</param>
    /// <returns></returns>
    public static T GetComponent<T>(this GameObject go, string path) where T : Component
    {
        return go.transform.GetComponent<T>(path);
    }

    #endregion

    #region ------ Image ------

    /// <summary> 设置小图片 </summary>
    /// <param name="image"></param>
    /// <param name="atlasName">图集名</param>
    /// <param name="spriteName">图片名</param>
    public static void SetSprite(this UnityEngine.UI.Image image, string atlasName,string spriteName)
    {
        image.sprite = AssetBundleManager.Instance.LoadSprite(atlasName, spriteName);
    }

    /// <summary> 设置背景图片 </summary>
    /// <param name="image"></param>
    /// <param name="folderName">背景所在文件夹名</param>
    /// <param name="spriteName">背景图片名</param>
    public static void SetBg(this UnityEngine.UI.Image image, string folderName, string spriteName)
    {
        image.sprite = AssetBundleManager.Instance.LoadBg(folderName, spriteName);
    }

    /// <summary> 图片置灰 </summary>
    /// <param name="image"></param>
    public static void SetGray(this UnityEngine.UI.Image image,bool isGray = true)
    {
        if (isGray)
            image.material = Resources.Load("Custom/Materials/GrayMaterial") as Material;
        else
            image.material = null;
    }

    /// <summary> 图片置灰 </summary>
    /// <param name="image"></param>
    public static void SetGray(this UnityEngine.UI.RawImage image, bool isGray = true)
    {
        if (isGray)
            image.material = Resources.Load("Custom/Materials/GrayMaterial") as Material;
        else
            image.material = null;
    }

    #endregion

    #region ------ Text ------

    public static void SetText(this Text text, string textValue, bool isAnimation = false,float duration = 0.2f,ScrambleMode scrambleMode = ScrambleMode.None)
    {
        if (text != null)
        {
            text.text = "";
            text.DOText(textValue, duration, scrambleMode: scrambleMode);
        }
        else
        {
            text.text = textValue;
        }
    }

    #endregion
}
