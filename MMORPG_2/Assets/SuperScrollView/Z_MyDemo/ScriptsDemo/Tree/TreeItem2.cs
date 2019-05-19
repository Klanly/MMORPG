/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：#CreateTime# 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：#CreateTime# 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

#pragma warning disable 0414
/// <summary>  </summary>
public class TreeItem2 : MonoBehaviour {

    public Text mNameText;
    public Button BtnGet;
    public Color32 mRedStarColor = new Color32(249, 227, 101, 255);
    public Color32 mGrayStarColor = new Color32(215, 215, 215, 255);
    public GameObject mContentRootObj;
    int mItemDataIndex = -1;
    int mChildDataIndex = -1;

    public void Init()
    {
        

    }

    public void SetData(string name)
    {
        mNameText.text = name;
    }

    public void SetItemData(GridItemData itemData, int itemIndex, int childIndex)
    {
        mItemDataIndex = itemIndex;
        mChildDataIndex = childIndex;
    }
}
