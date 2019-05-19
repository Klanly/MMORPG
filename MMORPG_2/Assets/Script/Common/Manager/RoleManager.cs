using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleManager:Singleton<RoleManager> 
{
    private bool m_IsMainPlayerInit = false;
    /// <summary> 初始化主角 </summary>
    public void InitMainPlayer()
    {
        if (m_IsMainPlayerInit)
            return;
        if (GlobalInit.Instance.PlayerInfo != null)
        {
            GameObject mainPlayerObj = Object.Instantiate(GlobalInit.Instance.JobDic[GlobalInit.Instance.PlayerInfo.JobId]);
            Object.DontDestroyOnLoad(mainPlayerObj);
            GlobalInit.Instance.CurrPlayer = mainPlayerObj.GetComponent<RoleCtrl>();
            GlobalInit.Instance.CurrPlayer.Init(RoleType.MainPlayer, GlobalInit.Instance.PlayerInfo,new RoleMainPlayerCityAI(GlobalInit.Instance.CurrPlayer));
            JobEntity entity = JobDBModel.Instance.Get(GlobalInit.Instance.PlayerInfo.JobId);
            for (int j = 0; j < entity.WeaponPath.Length; j++)
            {
                GameObject weaponObj = AssetBundleManager.Instance.Load(string.Format("Download/Model/{0}/{1}.assetbundle", entity.WeaponFloader, entity.WeaponPath[j]), entity.WeaponPath[j]);
                Object.Instantiate(weaponObj, mainPlayerObj.transform.GetChild(0).Find(entity.WeaponParent[j]));
            }
            int[] phySkillIdArray = entity.PhyAttackIdArray;
            for (int i = 0; i < phySkillIdArray.Length; i++)
            {
                RoleSkillInfo info = new RoleSkillInfo();
                info.SkillId = phySkillIdArray[i];
                info.SkillLevel = 1;
                info.SlotsNode = 0;
                GlobalInit.Instance.PlayerInfo.PhySkillDic[i] = info;
            }
        }

        m_IsMainPlayerInit = true;
    }

    public GameObject LoadPlayer()
    {
        GameObject mainPlayerObj = null;
        if (GlobalInit.Instance.PlayerInfo != null)
        {
            mainPlayerObj = Object.Instantiate(GlobalInit.Instance.JobDic[GlobalInit.Instance.PlayerInfo.JobId]);
            JobEntity entity = JobDBModel.Instance.Get(GlobalInit.Instance.PlayerInfo.JobId);
            for (int j = 0; j < entity.WeaponPath.Length; j++)
            {
                GameObject weaponObj = AssetBundleManager.Instance.Load(string.Format("Download/Model/{0}/{1}.assetbundle", entity.WeaponFloader, entity.WeaponPath[j]), entity.WeaponPath[j]);
                Object.Instantiate(weaponObj, mainPlayerObj.transform.GetChild(0).Find(entity.WeaponParent[j]));
            }
        }
        return mainPlayerObj;
    }

    /// <summary> 加载怪物镜像(不实例化) </summary>
    /// <param name="monsterId"></param>
    /// <returns></returns>
    public GameObject LoadMonster(int monsterId)
    {
        MonsterEntity entity = MonsterDBModel.Instance.Get(monsterId);
        if (entity == null) return null;
        return AssetBundleManager.Instance.Load(string.Format("Download/{0}/{1}.assetbundle", entity.FloaderName, entity.PrefabName), entity.PrefabName);
    }

    /// <summary> 加载NPC并实例化 </summary>
    /// <param name="npcExcelId">NPCId</param>
    /// <returns></returns>
    public IEnumerator InitNPC(int npcExcelId)
    {
        List<NPCDataEntity> entityList = NPCManager.Instance.GetWorldMapEntityList(npcExcelId);
        WorldMapEntity worldMapEntity = WorldMapDBModel.Instance.Get(npcExcelId);
        if (entityList != null && entityList.Count >0)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                yield return new WaitForEndOfFrame();
                GameObject obj = AssetBundleManager.Instance.Load(string.Format("Download/Model/Npc/{0}/{1}.assetbundle", worldMapEntity.NPCFloader, entityList[i].PrefabName), entityList[i].PrefabName);
                GameObject npcObj = Object.Instantiate(obj);
                npcObj.GetComponent<NPCCtrl>().Init(entityList[i]);
            }
        }
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}