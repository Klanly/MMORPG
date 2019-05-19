/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-11-21 17:51:26 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-11-21 17:51:26 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

/// <summary>  </summary>
public class UISelectRoleDragView : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private Vector2 m_DragBeginPos = Vector2.zero;
    private Vector2 m_DargEndPos = Vector2.zero;

    /// <summary> 拖拽委托 0=左 1=右 </summary>
    public Action<int> OnSelectRoleDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        m_DragBeginPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_DargEndPos = eventData.position;

        float x = m_DragBeginPos.x - m_DargEndPos.x;
        if (x > 20)
        {
            OnSelectRoleDrag(0);
        }
        else if(x < -20)
        {
            OnSelectRoleDrag(1);
        }
    }
}
