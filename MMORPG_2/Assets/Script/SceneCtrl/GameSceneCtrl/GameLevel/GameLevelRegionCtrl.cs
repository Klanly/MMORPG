/// <summary>
/// 功能描述    ：游戏关卡区域控制器
/// 创 建 者    ：
/// 创建日期    ：2018-12-31 09:17:41 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-31 09:17:41 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 游戏关卡区域控制器 </summary>
public class GameLevelRegionCtrl : MonoBehaviour
{
    private int m_RegionId;
    /// <summary> 角色出生点 </summary>
    [SerializeField]
    public Transform RoleBornPos;
    /// <summary> 怪物出生点 </summary>
    public Transform[] MonsterBornPos;
    /// <summary> 所有的门 </summary>
    [SerializeField]
    private GameLevelDoorCtrl[] m_AllDoor;

    /// <summary> 区域遮挡 </summary>
    public GameObject RegionMask;

    private void Awake()
    {
        Renderer playerRender = RoleBornPos.GetComponent<Renderer>();
        if(playerRender != null) playerRender.enabled = false;
        if (MonsterBornPos!= null && MonsterBornPos.Length > 0)
        {
            for (int i = 0; i < MonsterBornPos.Length; i++)
            {
                Renderer render = MonsterBornPos[i].GetComponent<Renderer>();
                if(render != null) render.enabled = false;
            }
        }
        if(m_AllDoor != null && m_AllDoor.Length > 0)
        {
            for (int i = 0; i < m_AllDoor.Length; i++)
            {
                Renderer render = m_AllDoor[i].GetComponent<Renderer>();
                if (render != null) render.enabled = false;
            }
        }
    }

    public void SetRegionId(int id)
    {
        m_RegionId = id;
        if (m_AllDoor != null && m_AllDoor.Length > 0)
        {
            for (int i = 0; i < m_AllDoor.Length; i++)
            {
                m_AllDoor[i].OwnerRegionId = m_RegionId;
            }
        }
    }

    public void GetToNextRegionDoor()
    {
        for (int i = 0; i < m_AllDoor.Length; i++)
        {
            m_AllDoor[i].gameObject.SetActive(false);
            if (m_AllDoor[i].CorrelationDoor != null)
            {
                m_AllDoor[i].CorrelationDoor.gameObject.SetActive(false);
            }
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(this.transform.position, 0.6f);
        if (RoleBornPos != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(RoleBornPos.position, 0.5f);
            Gizmos.DrawLine(transform.position, RoleBornPos.position);
        }
        if (MonsterBornPos != null && MonsterBornPos.Length > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < MonsterBornPos.Length; i++)
            {
                Gizmos.DrawLine(transform.position, MonsterBornPos[i].transform.position);
                Gizmos.DrawSphere(MonsterBornPos[i].transform.position, 0.8f);
            }
        }
        if (m_AllDoor != null && m_AllDoor.Length > 0)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < m_AllDoor.Length; i++)
            {
                Gizmos.DrawLine(transform.position, m_AllDoor[i].transform.position);
            }
        }
    }

#endif
}
