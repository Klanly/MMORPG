/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-12 17:30:28 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-12 17:30:28 
/// </summary>

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>  </summary>
public class UIRoleInfoDragView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Transform m_Target;
    private Vector2 m_DragBeginPos = Vector2.zero;
    private Vector2 m_DargEndPos = Vector2.zero;

    /// <summary> 旋转速度 </summary>
    private int m_Speed = 300;
    public void OnBeginDrag(PointerEventData eventData)
    {
        m_DragBeginPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_DargEndPos = eventData.position;
        float x = m_DragBeginPos.x - m_DargEndPos.x;
        m_Target.Rotate(0, Time.deltaTime * m_Speed * (x > 0 ? 1 : -1), 0);
        m_DragBeginPos = m_DargEndPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
