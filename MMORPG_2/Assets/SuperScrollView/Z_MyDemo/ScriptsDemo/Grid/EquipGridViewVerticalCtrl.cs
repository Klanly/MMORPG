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
using SuperScrollView;
using UnityEngine.UI;

/// <summary>  </summary>
public class EquipGridViewVerticalCtrl : GridViewVerticalCtrlBase {

    public int NeedMoveToIndex = 3;
    public Sprite[] SpriteArray;
    private void Start()
    {
        Init(Resources.Load<GameObject>("UIPrefab/Item_01"));
    }

    protected override LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= DataManger.TotalItemCount)
        {
            return null;
        }
        GridItemData itemData = DataManger.GetItemDataByIndex(index);
        if (itemData == null)
        {
            return null;
        }
        LoopListViewItem2 item = listView.NewListViewItem(ItemName);

        for (int i = 0; i < row; i++)
        {
            int itemIndex = index * row + i;
            EquipItem itemScript = item.transform.GetChild(i).GetComponent<EquipItem>();
            if (itemIndex >= DataManger.TotalDataCount)
            {
                itemScript.gameObject.SetActive(false);
                continue;
            }
            else
            {
                itemScript.gameObject.SetActive(true);
            }
            itemScript.Init();
            itemScript.SetData(itemIndex, UnityEngine.Random.Range(1, 6), SpriteArray[UnityEngine.Random.Range(0, SpriteArray.Length)].name, UnityEngine.Random.Range(0, 2) == 1,
                UnityEngine.Random.Range(1, 1000), UnityEngine.Random.Range(1, 6));
            itemScript.GetComponent<Button>().onClick.RemoveAllListeners();
            itemScript.GetComponent<Button>().onClick.AddListener(delegate
            {
                Debug.Log(itemScript.Index);
            });
        }
        return item;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MoveToIndex(NeedMoveToIndex);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            SetListItemTotalCount(10);
        }
    }
}
