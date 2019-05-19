using UnityEngine;
using System.Collections;
using Pathfinding;
using System;

/// <summary> 角色控制器 </summary>
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(FunnelModifier))]
public class RoleCtrl : MonoBehaviour 
{
    #region 成员变量或属性
    /// <summary> 昵称挂点 </summary>
    private Transform m_HeadBarTrans;

    /// <summary>
    /// 头顶UI条
    /// </summary>
    private GameObject m_HeadBar;

    /// <summary>
    /// 动画
    /// </summary>
    [SerializeField]
    public Animator Animator;

    /// <summary>
    /// 移动的目标点
    /// </summary>
    [HideInInspector]
    public Vector3 TargetPos = Vector3.zero;

    /// <summary>
    /// 控制器
    /// </summary>
    [HideInInspector]
    public CharacterController CharacterController;

    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField]
    public float Speed = 10f;

    /// <summary>
    /// 出生点
    /// </summary>
    [HideInInspector]
    public Vector3 BornPoint;

    /// <summary>
    /// 视野范围
    /// </summary>
    [HideInInspector]
    public float ViewRadius;

    /// <summary>
    /// 巡逻范围
    /// </summary>
    [HideInInspector]
    public float PatrolRadius;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float AttackRange;

    /// <summary> 当前角色类型 </summary>
    public RoleType CurrRoleType = RoleType.None;

    /// <summary> 当前角色信息 </summary>
    public RoleInfoBase CurrRoleInfo = null;

    /// <summary> 当前角色AI </summary>
    public IRoleAI CurrRoleAI = null;

    /// <summary> 锁定敌人 </summary>
    [HideInInspector]
    public RoleCtrl LockEnemy;


    /// <summary> 角色死亡 </summary>
    public Action<RoleCtrl> OnRoleDie;

    /// <summary> 角色销毁 </summary>
    public Action<Transform> OnRoleDestroy;

    /// <summary> 当前角色有限状态机管理器 </summary>
    public RoleFSMMgr CurrRoleFSMMgr = null;

    private RoleHeadBarView m_RoleHeadBarView = null;

    //==================== 寻路相关 =====================

    private Seeker m_Seeker;
    /// <summary> 路径 </summary>
    [HideInInspector]
    public ABPath AStartPath;
    /// <summary> 当前要去的路径点 </summary>
    [HideInInspector]
    public int AStartCurrWayPointIndex = 1;

    //==================== 战斗相关 =====================
    public RoleAttack Attack;
    private RoleHurt m_Hurt;

    /// <summary> 是否处于僵直状态 </summary>
    [HideInInspector]
    public bool IsRigidity;

    /// <summary> 上次战斗的时间(用于切换普通待机 当30秒内没攻击也没有被攻击就从战斗待机切换到普通待机) </summary>
    [HideInInspector]
    public float LastFightTime;

    private bool m_IsInit = false;

    /// <summary> HP变化 </summary>
    public OnValueChangeHandle OnHPChange;
    /// <summary> MP变化 </summary>
    public OnValueChangeHandle OnMPChange;

    #endregion

    /// <summary> 初始化 </summary>
    /// <param name="roleType">角色类型</param>
    /// <param name="roleInfo">角色信息</param>
    /// <param name="ai">AI</param>
    public void Init(RoleType roleType, RoleInfoBase roleInfo, IRoleAI ai)
    {
        CurrRoleType = roleType;
        CurrRoleInfo = roleInfo;
        CurrRoleAI = ai;
        if (CharacterController != null)
        {
            CharacterController.enabled = true;
        }
        if (CurrRoleType == RoleType.MainPlayer)
        {
            ToIdle();
            if (GlobalInit.Instance.PlayerInfo != null)
            {
                Attack.PhyAttackInfoList = SkillDBModel.Instance.GetPhyAttakInfoList(GlobalInit.Instance.PlayerInfo.JobId);
                Attack.SkillAttackInfoList = SkillDBModel.Instance.GetSkillAttakInfoList(GlobalInit.Instance.PlayerInfo.JobId);
            }
        }
        else if (CurrRoleType == RoleType.Monster)
        {
            ToIdle(RoleIdleState.IdleFight);
            Attack.PhyAttackInfoList = MonsterDBModel.Instance.GetPhyAttakInfoList(((RoleInfoMonster)CurrRoleInfo).MonsterEntity.Id);
            Attack.SkillAttackInfoList = MonsterDBModel.Instance.GetSkillAttakInfoList(((RoleInfoMonster)CurrRoleInfo).MonsterEntity.Id);
        }
    }

    void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        m_Seeker = GetComponent<Seeker>();
        if (CurrRoleType == RoleType.MainPlayer)
        {
            if (CameraCtrl.Instance != null)
            {
                CameraCtrl.Instance.Init();
            }
        }
        m_HeadBarTrans = new GameObject("HeadBarTrans").transform;
        m_HeadBarTrans.SetParent(transform);
        m_HeadBarTrans.transform.localPosition = Vector3.zero + new Vector3(0, CharacterController.height + 0.2f, 0);
        CurrRoleFSMMgr = new RoleFSMMgr(this,OnRoleDieCallBack,OnRoleDestroyCallBack);
        m_Hurt = new RoleHurt(CurrRoleFSMMgr);
        m_Hurt.OnRoleHurt = OnRoleHurtCallBack;
        Attack = new RoleAttack(CurrRoleFSMMgr);
        //Attack.SetFSM(CurrRoleFSMMgr);
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        
    }

    /// <summary> 角色受伤回调 </summary>
    private void OnRoleHurtCallBack()
    {
        if (m_RoleHeadBarView != null)
        {
            m_RoleHeadBarView.SetSliderHP(CurrRoleInfo.CurrHP / (float)CurrRoleInfo.MaxHP);
        }
        if (OnHPChange != null)
        {
            OnHPChange(ValueChnageType.Reduce);
        }
    }

    /// <summary> 角色死亡回调 </summary>
    private void OnRoleDieCallBack()
    {
        if (CharacterController != null)
        {
            CharacterController.enabled = false;
        }
        if (OnRoleDie != null && CurrRoleInfo != null)
        {
            OnRoleDie(this);
        }
    }

    /// <summary> 角色销毁回调 </summary>
    private void OnRoleDestroyCallBack()
    {
        if (OnRoleDestroy != null)
        {
            OnRoleDestroy(transform);
        }
        if (m_RoleHeadBarView != null)
        {
            Destroy(m_RoleHeadBarView.gameObject);
        }
    }

    /// <summary> 设置角色出生点 </summary>
    /// <param name="pos"></param>
    public void SetBornPoint(Vector3 pos)
    {
        BornPoint = pos;
        transform.position = pos;
        InitHeadBar();
    }

    void Update()
    {
        if (CurrRoleFSMMgr != null)
            CurrRoleFSMMgr.OnUpdate();

        //如果角色没有AI 直接返回
        if (CurrRoleAI == null) return;
        CurrRoleAI.DoAI();
        
        if (CharacterController == null) return;

        if (m_IsInit)
        {
            m_IsInit = false;
            
        }

        //让角色贴着地面
        if (!CharacterController.isGrounded)
        {
            CharacterController.Move((transform.position + new Vector3(0, -1000, 0)) - transform.position);
        }

        if (CurrRoleType == RoleType.MainPlayer)
        {
            CameraAutoFollow();
        }
        AutoSmallMap();
    }

    private void AutoSmallMap()
    {
        if (SmallMapHelper.Instance == null || UIMainCitySmallMapView.Instance == null)
            return;
        SmallMapHelper.Instance.gameObject.transform.position = transform.position;
        UIMainCitySmallMapView.Instance.SmallMap.transform.localPosition = new Vector3(SmallMapHelper.Instance.transform.localPosition.x * -512, SmallMapHelper.Instance.transform.localPosition.z * -512, 1);
        UIMainCitySmallMapView.Instance.PlayerRoleArrow.transform.localEulerAngles = new Vector3(0, 0, 360 - transform.eulerAngles.y);
    }

    /// <summary> 初始化头顶UI条 </summary>
    private void InitHeadBar()
    {
        if (RoleHeadBarRoot.Instance == null)
            return;
        if (CurrRoleInfo == null) return;

        //克隆预设
        m_HeadBar = ResourcesManager.Instance.Load(ResourcesManager.ResourceType.UIOther, "RoleHeadBar");
        m_HeadBar.transform.SetParent(RoleHeadBarRoot.Instance.gameObject.transform);
        m_HeadBar.transform.localScale = Vector3.one;


        m_RoleHeadBarView = m_HeadBar.GetComponent<RoleHeadBarView>();
        //给预设赋值
        m_RoleHeadBarView.Init(m_HeadBarTrans, CurrRoleInfo.NickName, isShowHPBar: (CurrRoleType == RoleType.MainPlayer ? false : true),sliderValue:CurrRoleInfo.CurrHP/(float)CurrRoleInfo.MaxHP);
    }


    #region 控制角色方法

    public void ToIdle(RoleIdleState state = RoleIdleState.IdleNormal)
    {
        CurrRoleFSMMgr.ToIdleState = state;
        CurrRoleFSMMgr.ChangeState(RoleState.Idle);
    }

    /// <summary> 临时测试用 </summary>
    public void ToRun()
    {
        CurrRoleFSMMgr.ChangeState(RoleState.Run);
    }

    public void MoveTo(Vector3 targetPos)
    {
        //处于死亡状态 不能移动
        if (CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Die) return;
        //处于僵直状态 不能移动
        if (IsRigidity) return;
        //如果目标点不是原点 进行移动
        if (targetPos == Vector3.zero) return;
        TargetPos = targetPos;
        //CurrRoleFSMMgr.ChangeState(RoleState.Run);
        m_Seeker.StartPath(transform.position, targetPos, (Path p) => 
        {
            if (!p.error)
            {
                AStartPath = (ABPath)p;
                if (Vector3.Distance(AStartPath.endPoint,new Vector3(AStartPath.originalEndPoint.x,AStartPath.endPoint.y,AStartPath.originalEndPoint.z))>0.5f)
                {
                    Debuger.Log("不能到达目标点");
                    AStartPath = null;
                    return;
                }
                AStartCurrWayPointIndex = 1;
                CurrRoleFSMMgr.ChangeState(RoleState.Run);
            }
            else
            {
                Debuger.Log("寻路出错");
                AStartPath = null;
            }
        });
    }
    /// <summary> 发起攻击 </summary>
    /// <param name="type">攻击类型</param>
    /// <param name="skillId">技能Id</param>
    public void ToAttack(RoleAttackType type, int skillId)
    {
        Attack.ToAttack(type, skillId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attackValue">受到的攻击力</param>
    /// <param name="delay">延迟时间</param>
    public void ToHurt(RoleTransferAttackInfo attackInfo)
    {
        StartCoroutine(m_Hurt.ToHurt(attackInfo));
    }

    public void ToDie()
    {
        CurrRoleFSMMgr.ChangeState(RoleState.Die);
    }
    public void ToSelect()
    {
        CurrRoleFSMMgr.ChangeState(RoleState.Select);
    }

    #endregion

    #region OnDestroy 销毁
    /// <summary>
    /// 销毁
    /// </summary>
    void OnDestroy()
    {
        if (m_HeadBar != null)
        {
            Destroy(m_HeadBar);
        }
    }
#endregion

#region CameraAutoFollow 摄像机自动跟随
    /// <summary>
    /// 摄像机自动跟随
    /// </summary>
    private void CameraAutoFollow()
    {
        if (CameraCtrl.Instance == null) return;

        CameraCtrl.Instance.transform.position = gameObject.transform.position;
        CameraCtrl.Instance.AutoLookAt(gameObject.transform.position);
    }
#endregion
}