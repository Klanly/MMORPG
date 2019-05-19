using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GlobalInit :MonoBehaviour
{
    public static GlobalInit Instance;
    #region 常量
    /// <summary>
    /// 昵称KEY
    /// </summary>
    public const string MMO_NICKNAME = "MMO_NICKNAME";

    /// <summary>
    /// 密码KEY
    /// </summary>
    public const string MMO_PWD = "MMO_PWD";

    #endregion

    [HideInInspector]
    public int AccountId;
    [HideInInspector]
    public string AccountUserName;
    //public static GlobalInit Instance;

    /// <summary>
    /// 玩家注册时候的昵称
    /// </summary>
    [HideInInspector]
    public string CurrRoleNickName;

    /// <summary> UI动画曲线 </summary>
    public AnimationCurve UIAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    /// <summary> 主角镜像 </summary>
    [HideInInspector]
    public Dictionary<int, GameObject> JobDic = new Dictionary<int, GameObject>();

    /// <summary> 角色信息 </summary>
    [HideInInspector]
    public RoleInfoMainPlayer PlayerInfo;

    /// <summary> 当前玩家控制器 </summary>
    [HideInInspector]
    public RoleCtrl CurrPlayer;
    /// <summary> 当前点击的技能槽 </summary>
    [HideInInspector]
    public UISkillSlotsItem SkillSlotsItem;

    [HideInInspector]
    public bool IsAutoFight = false;

    public Action<int> OnSkillClick;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        Debuger.DebugMode = AppConst.DebugMode;
        LanguageUtil.Init();
    }
    private void FixedUpdate()
    {
        FrameTimerManager.Instance.FrameHandle();
    }

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    if (CurrPlayer == null)
        //        return;
        //    Transform tran = CurrPlayer.transform;
        //    Debuger.Log(string.Format("{0}_{1}_{2}_{3}", tran.position.x, tran.position.y, tran.position.z, tran.rotation.eulerAngles.y));
        //}
    }
}