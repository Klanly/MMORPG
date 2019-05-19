using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

/// <summary> 主场景场景控制器 </summary>
public class WorldMapSceneCtrl : GameSceneCtrlBase 
{
    /// <summary> 主角出生点 </summary>
    [SerializeField]
    private Transform m_PlayerBornPos;

    protected override void OnAwake()
    {
        base.OnAwake();
        
    }

    protected override void OnMainCityUILoadComplete()
    {
        base.OnMainCityUILoadComplete();
        if (DelegateDefine.Instance.OnSceneLoadOk != null)
            DelegateDefine.Instance.OnSceneLoadOk();
        RoleManager.Instance.InitMainPlayer();
        PlayerCtrl.Instance.SetMainCityRoleInfo();
        UpdateMainMenuIcon();

        if (GlobalInit.Instance == null)
            return;
        if (GlobalInit.Instance.CurrPlayer != null)
        {
            //GlobalInit.Instance.CurrPlayer.gameObject.transform.position = m_PlayerBornPos.position;
            WorldMapEntity entity = WorldMapDBModel.Instance.Get(SceneMgr.Instance.CurrentWorldMapId);
            if (entity.RoleBirthPos != null && entity.RoleBirthPos.Length > 3)
            {
                GlobalInit.Instance.CurrPlayer.SetBornPoint(new Vector3(entity.RoleBirthPos[0], entity.RoleBirthPos[1], entity.RoleBirthPos[2]));
                GlobalInit.Instance.CurrPlayer.gameObject.transform.eulerAngles = new Vector3(0, entity.RoleBirthPos[3], 0);
            }
            else
            {
                GlobalInit.Instance.CurrPlayer.SetBornPoint(m_PlayerBornPos.position);
            }
        }
        StartCoroutine(RoleManager.Instance.InitNPC(SceneMgr.Instance.CurrentWorldMapId));
    }


    protected override void OnStart()
    {
        //if (GlobalInit.Instance == null)
        //    return;
        //RoleManager.Instance.InitMainPlayer();
        //if (GlobalInit.Instance.CurrPlayer != null)
        //{
        //    //GlobalInit.Instance.CurrPlayer.gameObject.transform.position = m_PlayerBornPos.position;
        //    WorldMapEntity entity = WorldMapDBModel.Instance.Get(SceneMgr.Instance.CurrentWorldMapId);
        //    if (entity.RoleBirthPos != null && entity.RoleBirthPos.Length > 3)
        //    {
        //        GlobalInit.Instance.CurrPlayer.SetBornPoint(new Vector3(entity.RoleBirthPos[0], entity.RoleBirthPos[1], entity.RoleBirthPos[2]));
        //        GlobalInit.Instance.CurrPlayer.gameObject.transform.eulerAngles = new Vector3(0, entity.RoleBirthPos[3], 0);
        //    }
        //    else
        //    {
        //        GlobalInit.Instance.CurrPlayer.SetBornPoint(m_PlayerBornPos.position);
        //    }
        //}
        //StartCoroutine(RoleManager.Instance.InitNPC(SceneMgr.Instance.CurrentWorldMapId));
    }
}