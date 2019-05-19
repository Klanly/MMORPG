/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-12-31 09:14:59 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-12-31 09:14:59 
/// </summary>

using PathologicalGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  </summary>
public class GameLevelSceneCtrl : GameSceneCtrlBase {

    public static GameLevelSceneCtrl Instance;

    [SerializeField]
    private GameLevelRegionCtrl[] m_Region;

    private int m_Chapter = -1;
    private int m_Gamelevel = -1;

    /// <summary> 本关卡怪的总数量 </summary>
    private int m_AllMonsterCount;

    /// <summary> 怪的种类 </summary>
    private int[] m_MonsterIdArray;

    /// <summary> 当前区域怪的数量总数量 </summary>
    private int m_CurrRegionMonsterCount;

    /// <summary> 当前区域已经刷新的怪数量 </summary>
    private int m_CurrRegionCreateMonsterCount;

    /// <summary> 当前区域击杀的怪总数量 </summary>
    private int m_CurrRegionKillMonsterCount;

    /// <summary> 当前区域没种怪的数量 </summary>
    private List<int> m_RegionMonsterCountList = new List<int>();
    /// <summary> 区域怪id及数量 </summary>
    private List<GameLevelMonsterEntity> m_RegionMonsterList;


    /// <summary> 怪物镜像 </summary>
    private Dictionary<int, GameObject> m_MonsterGameObjectDic;

    /// <summary> 怪物池 </summary>
    private SpawnPool m_MonsterPool;

    private GameLevelRegionCtrl m_CurrRegionctrl;

    /// <summary> 当前区域 </summary>
    private int m_CurrRegionIndex;
    
    /// <summary> 是否处于战斗中 </summary>
    private bool m_IsFighting = false;

    /// <summary> 进入副本时长 </summary>
    private float m_UseTime = 0f;

    private int m_DropExp = 0;
    private int m_DropCoin= 0;

    /// <summary> 当前区域是否有怪 </summary>  
    public bool IsCurrRegionHaveMosnter { get { return m_CurrRegionKillMonsterCount < m_CurrRegionMonsterCount; } }

    /// <summary> 当前区域是否已经是最后一个区域了 </summary>
    public bool IsCurrRegionIsLast { get { return m_CurrRegionIndex >= m_Region.Length - 1; } }

    /// <summary> 杀死的所有怪物Id列表 </summary>
    private List<int> m_KillMonsterIdList;

    private List<GameLevelSuccessRequestProto.GoodsItem> m_DropGoodsList;
    public Vector3 NextRegionPlayerBornPos
    {
        get
        {
            return m_CurrRegionctrl.RoleBornPos.position;
        }
    }
    protected override void OnAwake()
    {
        Instance = this;
        base.OnAwake();
        m_KillMonsterIdList = new List<int>();
        m_DropGoodsList = new List<GameLevelSuccessRequestProto.GoodsItem>();
        m_RegionMonsterList = new List<GameLevelMonsterEntity>();
        m_MonsterGameObjectDic = new Dictionary<int, GameObject>();
        m_Chapter = GameLevelCtrl.Instance.ChapterId;
        m_Gamelevel = GameLevelCtrl.Instance.GameLevelId;
        for (int i = 0; i < m_Region.Length; i++)
        {
            m_Region[i].SetRegionId(i);
        }
    }

    protected override void OnStart ()
    {
        base.OnStart();
        m_IsFighting = true;
        m_UseTime = 0f;
        m_DropExp = 0;
        m_DropCoin = 0;
    }

    protected override void OnMainCityUILoadComplete()
    {
        base.OnMainCityUILoadComplete();
        EnterRegion(0);
        m_MonsterIdArray = GameLevelMonsterDBModel.Instance.GetGameLevelMonsterId(m_Chapter, m_Gamelevel);
        m_MonsterPool = PoolManager.Pools.Create("Monster");
        m_MonsterPool.group.parent = null;
        m_MonsterPool.group.localPosition = Vector3.zero;
        for (int i = 0; i < m_MonsterIdArray.Length; i++)
        {
            //m_MonsterGameObjectDic[m_MonsterIdArray[i]] = RoleManager.Instance.LoadMonster(m_MonsterIdArray[i]);
            PrefabPool prefabPool = new PrefabPool(RoleManager.Instance.LoadMonster(m_MonsterIdArray[i]).transform);
            prefabPool.preloadAmount = 5;//预加载数量
            prefabPool.cullDespawned = true;//是否开启缓存吃自动清理模式
            prefabPool.cullAbove = 5;//缓存池自动清理 但是始终保持几个对象不清理
            prefabPool.cullDelay = 2;//多长时间清理一次 时间是秒
            prefabPool.cullMaxPerPass = 2;//每次清理几个
            m_MonsterPool.CreatePrefabPool(prefabPool);//创建子池
        }
        PlayerCtrl.Instance.SetMainCityRoleInfo();
        UpdateMainMenuIcon(true);
    }

    /// <summary> 进入区域 </summary>
    /// <param name="regionIndex"></param>
    private void EnterRegion(int regionIndex)
    {
        m_CurrRegionctrl = m_Region[regionIndex];
        if (m_CurrRegionctrl == null) return;
        m_CurrRegionctrl.RegionMask.SetActive(false);
        if (regionIndex != 0)
        {
            m_Region[regionIndex-1].GetToNextRegionDoor();
        }
        m_CurrRegionMonsterCount = 0;
        m_CurrRegionCreateMonsterCount = 0;
        m_CurrRegionKillMonsterCount = 0;
        m_CurrRegionIndex = regionIndex;
        if (regionIndex == 0)
        {
            if (GlobalInit.Instance.CurrPlayer != null)
            {
                GlobalInit.Instance.CurrPlayer.SetBornPoint(m_Region[regionIndex].RoleBornPos.position);
                GlobalInit.Instance.CurrPlayer.ToIdle(RoleIdleState.IdleFight);
                GlobalInit.Instance.CurrPlayer.OnRoleDie = OnPlayerDie;
            }
            if (DelegateDefine.Instance.OnSceneLoadOk != null)
                DelegateDefine.Instance.OnSceneLoadOk();
        }
        m_AllMonsterCount = GameLevelMonsterDBModel.Instance.GetGameLevelMonsterCount(m_Chapter,m_Gamelevel);
        m_RegionMonsterCountList.Clear();
        m_RegionMonsterList = GameLevelMonsterDBModel.Instance.GetGamelLevelRegionMonster(m_Chapter,m_Gamelevel,regionIndex+1);
        for (int i = 0; i < m_RegionMonsterList.Count; i++)
        {
            m_CurrRegionMonsterCount += m_RegionMonsterList[i].MonsterCount;
            m_RegionMonsterCountList.Add(m_RegionMonsterList[i].MonsterCount);
        }

    }

    private void OnPlayerDie(RoleCtrl obj)
    {
        StartCoroutine(OnRoleResurgence());
    }

    private IEnumerator OnRoleResurgence()
    {
        yield return new WaitForSeconds(3);
        GameLevelCtrl.Instance.Result = 0;
        WindowManager.Instance.OpenWindow(Window.FightResult);
    }

    private float m_NextCreateMonsterTime = 0f;
    // Update is called once per frame
    protected override void OnUpdate()
    {
        if (m_IsFighting)
        {
            m_UseTime += Time.deltaTime;
            if (m_CurrRegionCreateMonsterCount < m_CurrRegionMonsterCount)
            {
                if (Time.time > m_NextCreateMonsterTime)
                {
                    m_NextCreateMonsterTime = Time.time + 1;
                    CreateMonster();
                }
            }
        }
    }
    private int m_Index = 0;
    /// <summary> 怪物Id(副本结算需要向服务器发送的id 不是配置表中的id 战斗场景中的每个怪的id都不同) </summary>
    private int m_MonsterTempId = 0;
    
    /// <summary> 刷怪 </summary>
    private void CreateMonster()
    {
        //临时测试
        //if (m_CurrRegionCreateMonsterCount>=1) return;

        if (m_RegionMonsterList.Count == 0)
        {
            return;
        }
        for (int i = 0; i < 1; i++)
        {
            m_Index = UnityEngine.Random.Range(0, m_RegionMonsterCountList.Count);
            if (m_RegionMonsterCountList[m_Index] == 0)
            {
                i--;
            }
        }
        int monsterId = m_RegionMonsterList[m_Index].MonsterId;
        RoleInfoMonster monsterInfo = new RoleInfoMonster();
        MonsterEntity monsterEntity = MonsterDBModel.Instance.Get(monsterId);
        monsterInfo.MonsterEntity = monsterEntity;
        monsterInfo.GameLevelMonsterEntity = GameLevelMonsterDBModel.Instance.GetGameLevelMonsterEntity(GameLevelCtrl.Instance.ChapterId, GameLevelCtrl.Instance.GameLevelId, m_CurrRegionIndex + 1, monsterId);
        monsterInfo.RoleId = ++m_MonsterTempId;
        m_KillMonsterIdList.Add(monsterInfo.RoleId);
        monsterInfo.SetMonsterData(monsterEntity);
        //从池子中取怪
        Transform monsterTrans = m_MonsterPool.Spawn(MonsterDBModel.Instance.Get(monsterId).PrefabName);
        //将怪放在出生点
        Transform monsterBornPos = m_CurrRegionctrl.MonsterBornPos[UnityEngine.Random.Range(0, m_CurrRegionctrl.MonsterBornPos.Length)];
        RoleCtrl monsterCtrl = monsterTrans.GetComponent<RoleCtrl>();
        monsterCtrl.PatrolRadius = monsterEntity.PatrolRadius;
        monsterCtrl.ViewRadius = monsterEntity.ViewRadius;
        monsterCtrl.Speed = monsterEntity.MoveSpeed;
        monsterCtrl.Init(RoleType.Monster, monsterInfo, new GameLevelRoleMonsterAI(monsterCtrl,monsterInfo));
        monsterCtrl.OnRoleDie = OnRoleDieCallBack;
        monsterCtrl.OnRoleDestroy = OnRoleDestroyCallBack;
        monsterCtrl.SetBornPoint(monsterBornPos.TransformPoint(UnityEngine.Random.Range(-0.5f, 0.5f), 0, UnityEngine.Random.Range(-0.5f, 0.5f)));
        m_RegionMonsterCountList[m_Index]--;
        m_CurrRegionCreateMonsterCount++;
    }
    private void OnRoleDieCallBack(RoleCtrl ctrl)
    {
        GameLevelMonsterEntity entity = ((RoleInfoMonster)ctrl.CurrRoleInfo).GameLevelMonsterEntity;
        TipsUtil.ShowExpTips(entity.DropExp);
        TipsUtil.ShowCoinTips(entity.DropCoin);
        m_DropExp += entity.DropExp;
        m_DropCoin += entity.DropCoin;
        m_CurrRegionKillMonsterCount++;
        //掉落装备/材料
        int equipNum = UnityEngine.Random.Range(1, 101);
        if (equipNum > 50)
        {
            int tempNum = UnityEngine.Random.Range(1, 101);
            int equipId = 0;
            if (tempNum < 35)
                equipId = entity.DropEquip[0][0].ToInt();
            else if (tempNum < 60)
                equipId = entity.DropEquip[1][0].ToInt();
            else if (tempNum < 80)
                equipId = entity.DropEquip[2][0].ToInt();
            else if (tempNum < 95)
                equipId = entity.DropEquip[3][0].ToInt();
            else
                equipId = entity.DropEquip[4][0].ToInt();
            //掉落装备
            //Debuger.Log("掉落装备Id = " + equipId);
            GameLevelSuccessRequestProto.GoodsItem item = new GameLevelSuccessRequestProto.GoodsItem();
            item.GoodsType = 1;
            item.GoodsCount = 1;
            item.GoodsId = equipId;
            m_DropGoodsList.Add(item);
        }
        int matNum = UnityEngine.Random.Range(1, 101);
        if (matNum > 50)
        {
            int tempNum2 = UnityEngine.Random.Range(1, 101);
            int matId = 0;
            if (tempNum2 < 35)
                matId = entity.DropMaterial[0][0].ToInt();
            else if (tempNum2 < 60)
                matId = entity.DropMaterial[1][0].ToInt();
            else if (tempNum2 < 80)
                matId = entity.DropMaterial[2][0].ToInt();
            else if (tempNum2 < 95)
                matId = entity.DropMaterial[3][0].ToInt();
            else
                matId = entity.DropMaterial[4][0].ToInt();
            //掉落材料
            //Debuger.LogError("掉落材料Id = " + matId);
            bool isHaveMat = false;
            for (int i = 0; i < m_DropGoodsList.Count; i++)
            {
                if (m_DropGoodsList[i].GoodsId == matId)
                {
                    isHaveMat = true;
                    GameLevelSuccessRequestProto.GoodsItem item = new GameLevelSuccessRequestProto.GoodsItem();
                    item.GoodsType = m_DropGoodsList[i].GoodsType;
                    item.GoodsId = m_DropGoodsList[i].GoodsId;
                    item.GoodsCount = m_DropGoodsList[i].GoodsCount +1;
                    m_DropGoodsList[i] = item;
                    break;
                }
            }
            if (!isHaveMat)
            {
                GameLevelSuccessRequestProto.GoodsItem item = new GameLevelSuccessRequestProto.GoodsItem();
                item.GoodsType = 2;
                item.GoodsCount = 1;
                item.GoodsId = matId;
                m_DropGoodsList.Add(item);
            }
        }

        if (m_CurrRegionKillMonsterCount >= m_CurrRegionMonsterCount)
        {
            if (m_CurrRegionIndex < m_Region.Length-1)
            {
                //进入下一区域
                m_CurrRegionIndex++;
                EnterRegion(m_CurrRegionIndex);
            }
            else
            {
                m_IsFighting = false;
                GameLevelCtrl.Instance.Result = 1;
                GameLevelCtrl.Instance.UseTime = m_UseTime;
                GameLevelCtrl.Instance.DropExp = m_DropExp;
                GameLevelCtrl.Instance.DropCoin = m_DropCoin;
                //最后一击播放慢动画
                TimeManager.Instance.ChangeTimeScale(0.3f, 5f, OpenFightResultWindow);
                //战斗胜利
                Debug.Log("战斗胜利");
            }
        }
    }
    public void OpenFightResultWindow()
    {
        WindowManager.Instance.OpenWindow(Window.FightResult);
        //发送战斗胜利协议
        OnSuccessRequest();
    }

    private void OnRoleDestroyCallBack(Transform obj)
    {
        //角色销毁 进行回池
        m_MonsterPool.Despawn(obj);
    }

    private void OnSuccessRequest()
    {
        GameLevelSuccessRequestProto proto = new GameLevelSuccessRequestProto();
        proto.ChapterId = GameLevelCtrl.Instance.ChapterId;
        proto.GameLevelId = GameLevelCtrl.Instance.GameLevelId;
        proto.Grade = 1;
        proto.Exp = GameLevelCtrl.Instance.DropExp;
        proto.Gold = GameLevelCtrl.Instance.DropCoin;
        proto.KillTotalMonsterCount = m_KillMonsterIdList.Count;
        proto.KillMonsterList = new List<GameLevelSuccessRequestProto.MonsterItem>();
        for (int i = 0; i < m_KillMonsterIdList.Count; i++)
        {
            GameLevelSuccessRequestProto.MonsterItem monsterItem = new GameLevelSuccessRequestProto.MonsterItem();
            monsterItem.MonsterId = m_KillMonsterIdList[i];
            monsterItem.MonsterCount = 1;
            proto.KillMonsterList.Add(monsterItem);
        }
        proto.GoodsTotalCount = m_DropGoodsList.Count;
        proto.GetGoodsList = m_DropGoodsList;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

#if UNITY_EDITOR

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.8f);

        if (m_Region!=null && m_Region.Length >0)
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < m_Region.Length; i++)
            {
                Gizmos.DrawLine(transform.position, m_Region[i].transform.position);
            }
        }
    }

#endif
}
