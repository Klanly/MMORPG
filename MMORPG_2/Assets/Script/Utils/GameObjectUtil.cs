using UnityEngine;
using System.Collections;

public static class GameObjectUtil 
{
    /// <summary> 设置对象层级(包括子对象)</summary>
    /// <param name="obj"></param>
    /// <param name="layerName"></param>
    public static void SetLayer(this GameObject obj,string layerName)
    {
        Transform[] trans = obj.GetComponentsInChildren<Transform>();
        for (int i = 0; i < trans.Length; i++)
            trans[i].gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    /// <summary> 设置物体父对象 </summary>
    /// <param name="obj"></param>
    /// <param name="parent"></param>
    public static void SetParent(this GameObject obj,Transform parent)
    {
        obj.transform.SetParent(parent);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.localEulerAngles = Vector3.zero;
    }
}