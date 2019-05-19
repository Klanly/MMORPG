/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-25 09:33:50 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-25 09:33:50 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class StarContainer : MonoBehaviour {

    private List<StarItem> m_StarList = new List<StarItem>();
    [SerializeField]
    private GameObject m_StarItem;
    private int m_StarX;
    private int m_StarY;

    /// <summary> 初始化 </summary>
    /// <param name="starCount">点亮星数</param>
    /// <param name="totalCount">总星数</param>
    /// <param name="row">每行个数</param>
    /// <param name="showStarDelay">显示下一个星的时间间隔</param>
    public void Init(int starCount = 3,int totalCount = 3,int row = 3,float showStarDelay = 0f)
    {
        if (starCount > totalCount || row > totalCount)
        {
            Debuger.LogError("starCount or row more than taotalCount");
            return;
        }
        StarItem starItem1 = m_StarItem.GetComponent<StarItem>();
        m_StarList.Add(starItem1);
        Rect rect = starItem1.GetComponent<RectTransform>().rect;
        m_StarX = (int)rect.width;
        m_StarY = (int)rect.height;
        starItem1.SetLight(starCount > 0);
        StartCoroutine(InstantiateStar(starItem1, starCount, totalCount, row, showStarDelay));
    }


    private IEnumerator InstantiateStar(StarItem starItem,int starCount = 3, int totalCount = 3, int row = 3, float showStarDelay = 0f)
    {
        for (int i = 1; i < totalCount; i++)
        {
            yield return new WaitForSeconds(showStarDelay);
            Vector2 indexV2 = new Vector2(i % row, Mathf.FloorToInt(i / (float)row));
            GameObject obj = Instantiate(m_StarItem);
            obj.SetParent(transform);
            obj.name = "StarItem" + (i + 1);
            StarItem item = obj.GetComponent<StarItem>();
            item.SetLight(i < starCount);
            m_StarList.Add(item);
            obj.transform.localPosition = new Vector3(starItem.transform.localPosition.x + indexV2.x * m_StarX, starItem.transform.localPosition.y - indexV2.y * m_StarY);
        }
    }

    /// <summary> 设置容器大小 </summary>
    /// <param name="scale"></param>
    public void SetScale(float scale)
    {
        transform.localScale = new Vector2(scale, scale);
    }

    /// <summary> 重新设置星数(调用之前必须先实例化) </summary>
    /// <param name="lightCount"></param>
    public void SetStarCount(int lightCount)
    {
        for (int i = 0; i < m_StarList.Count; i++)
            m_StarList[i].SetLight(i < lightCount);
    }
}
