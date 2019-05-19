using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRoleSceneCtrl : MonoBehaviour {

    /// <summary> 柱子 创建角色显示  选择角色进入游戏隐藏 </summary>
    public GameObject[] Pillars;
    
    private List<JobEntity> m_JobList;

    [SerializeField]
    private Transform[] CreateRoleContainer;
    [SerializeField]
    private Transform SelectRoleContainer;

    private Dictionary<int, RoleCtrl> m_JobRoleCtrl = new Dictionary<int, RoleCtrl>();

    private UISceneSelectRoleView m_SelectRoleView;

    private ToggleList<UICreateRoleJobItem> m_JobToggleList;
    private int m_SelectJobId = 0;
    private int m_SelectRoleId = 0;

    private ToggleList<UISelectRoleItem> m_SelectRoleItemList;
    private List<GameObject> m_CreateCloneRoleGoList = new List<GameObject>();

    private bool m_IsHaveRole = false;
    private bool m_IsSelectRoleView = false;

    //private int m_LastInWorldMapId = 0;
    private void Awake()
    {
        m_SelectRoleView = UISceneCtrl.Instance.LoadSceneUI(UISceneCtrl.SceneUIType.SelectRole).GetComponent<UISceneSelectRoleView>();
        m_SelectRoleView.OnBtnBackGameClick = OnBtnBackGameClick;
        m_SelectRoleView.OnBtnBeginGameClick = OnBtnBeginGameClick;

        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleCreateResponse, OnRoleCreateResponse);
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleLogOnGameServerResponse, OnLogOnGameServerResponse);
        //SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleInfoResponse,OnRoleInfoResponse);
        //SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleSkillDataResponse, OnRoleSkillDataResponse);
    }

    // Use this for initialization
    void Start () {
        if (DelegateDefine.Instance.OnSceneLoadOk != null)
        {
            DelegateDefine.Instance.OnSceneLoadOk();
        }
        if (m_SelectRoleView != null)
        {
            m_SelectRoleView.SelectRoleDragView.OnSelectRoleDrag = OnSelectRoleDrag;
        }
        //监听协议
        LoadRole();
        LogOnGameServer();

        if (m_SelectRoleView.JobItemList != null && m_SelectRoleView.JobItemList.Count > 0)
        {
            m_JobToggleList = new ToggleList<UICreateRoleJobItem>(m_SelectRoleView.JobItemList,m_SelectRoleView.JobItemDependList);
            for (int i = 0; i < m_JobToggleList.Count; i++)
            {
                Text text = m_SelectRoleView.JobItemDependList[i].GetComponent<Text>("Text");
                text.text = StringUtil.GetStringById(JobDBModel.Instance.Get(m_JobList[i].Id).DescId);
                m_JobToggleList.GetValue(i).SetData(i,text, m_JobToggleList.Select);
            }
            m_JobToggleList.CallBack = OnToggleListCallBack;
            m_JobToggleList.Select(0);
            OnToggleListCallBack(0);
        }
    }

    private void OnToggleListCallBack(int index)
    {
        JobEntity entity = JobDBModel.Instance.Get(m_JobList[index].Id);
        m_TargetAngle = entity.Rotate;
        m_IsRotating = true;
        m_SelectJobId = entity.Id;
        m_SelectRoleView.Gender = entity.Gender;
        m_SelectRoleView.RandomName(entity.Gender);
    }

    private void LoadRole()
    {
        m_JobList = JobDBModel.Instance.GetList();
        GlobalInit.Instance.JobDic.Clear();
        for (int i = 0; i < m_JobList.Count; i++)
        {
            GameObject obj = AssetBundleManager.Instance.Load(string.Format("Download/Model/Player/{0}/{1}.assetbundle",m_JobList[i].FloaderName,m_JobList[i].PrefabName),m_JobList[i].PrefabName);
            
            GlobalInit.Instance.JobDic.Add(m_JobList[i].Id, obj);
        }
    }

    private void CloneCreateRole()
    {
        if (m_CreateCloneRoleGoList.Count == 0)
        {
            if (CreateRoleContainer == null || CreateRoleContainer.Length < 4) return;
            for (int i = 0; i < m_JobList.Count; i++)
            {
                GameObject obj = Instantiate(GlobalInit.Instance.JobDic[m_JobList[i].Id], CreateRoleContainer[i]);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
                obj.transform.localScale = Vector3.one;
                JobEntity entity = JobDBModel.Instance.Get(m_JobList[i].Id);
                for (int j = 0; j < entity.WeaponPath.Length; j++)
                {
                    GameObject weaponObj = AssetBundleManager.Instance.Load(string.Format("Download/Model/{0}/{1}.assetbundle", entity.WeaponFloader, entity.WeaponPath[j]), entity.WeaponPath[j]);
                    Instantiate(weaponObj, obj.transform.GetChild(0).Find(entity.WeaponParent[j]));
                }
                RoleCtrl roleCtrl = obj.GetComponent<RoleCtrl>();
                if (roleCtrl != null)
                {
                    m_JobRoleCtrl[m_JobList[i].Id] = roleCtrl;
                }
                m_CreateCloneRoleGoList.Add(obj);
            }
        }
        else
        {
            for (int i = 0; i < m_CreateCloneRoleGoList.Count; i++)
            {
                m_CreateCloneRoleGoList[i].SetActive(true);
            }
        }
    }

    /// <summary> 发送登录区服消息 </summary>
    private void LogOnGameServer()
    {
        RoleLogOnGameServerRequestProto proto = new RoleLogOnGameServerRequestProto();
        proto.AccountId = GlobalInit.Instance.AccountId;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    private void OnLogOnGameServerResponse(byte[] p)
    {
        RoleLogOnGameServerResponseProto proto = RoleLogOnGameServerResponseProto.GetProto(p);
        m_IsHaveRole = proto.RoleCount > 0;
        if (proto.RoleCount>0)
        {
            DragTarget.rotation = new Quaternion();
            m_IsSelectRoleView = false;
            SetPillarsActive(false);
            //有角色 进入 进入游戏界面
            Debuger.Log("有角色 进入 进入游戏界面");
            //m_SelectRoleView.SelectRoleContainer.gameObject.SetActive(true);
            m_SelectRoleView.CreateRoleContainer.gameObject.SetActive(false);
            int count = proto.RoleCount;
            m_SelectRoleItemList = new ToggleList<UISelectRoleItem>();
            GameObject obj = ResourcesManager.Instance.LoadItem("SelectRoleHeadItem");
            for (int i = 0; i < m_CreateCloneRoleGoList.Count; i++)
            {
                m_CreateCloneRoleGoList[i].SetActive(false);
            }
            for (int i = 0; i < count; i++)
            {
                UISelectRoleItem item = Instantiate(obj, m_SelectRoleView.SelectRoleContainer).GetComponent<UISelectRoleItem>();
                JobEntity entity = JobDBModel.Instance.Get(proto.RoleList[i].RoleJob);
                UISelectRoleItemInfo itemInfo = new UISelectRoleItemInfo();
                itemInfo.Index = i;
                itemInfo.AtalsName = "RoleIcon";
                itemInfo.SpriteName = entity.HeadPic;
                itemInfo.RoleId = proto.RoleList[i].RoleId;
                itemInfo.RoleName = proto.RoleList[i].RoleNickName;
                itemInfo.Level = proto.RoleList[i].RoleLevel;
                itemInfo.JobName = entity.Name;
                //itemInfo.SpriteName = JobDBModel.Instance.get
                item.SetData(itemInfo, OnSelectCallBack);
                m_SelectRoleItemList.Add(item);
                item.transform.localPosition = new Vector3(0, 123 - 113 * i, 0);
                GameObject roleObj = AssetBundleManager.Instance.Load(string.Format("Download/Model/Player/{0}/{1}.assetbundle", entity.FloaderName, entity.PrefabName), entity.PrefabName);
                
                GameObject roleGameObject = Instantiate(roleObj, SelectRoleContainer);
                for (int j = 0; j < entity.WeaponPath.Length; j++)
                {
                    GameObject weaponObj = AssetBundleManager.Instance.Load(string.Format("Download/Model/{0}/{1}.assetbundle", entity.WeaponFloader, entity.WeaponPath[j]), entity.WeaponPath[j]);
                    Instantiate(weaponObj, roleGameObject.transform.GetChild(0).Find(entity.WeaponParent[j]));
                }

                roleGameObject.transform.localPosition = Vector3.zero;
                roleGameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                roleGameObject.transform.localScale = Vector3.one;
                m_SelectRoleItemList.AddDepend(roleGameObject);
                roleGameObject.SetActive(i == 0);
            }
            OnSelectCallBack(0);
        }
        else
        {
            ShowCreateRoleView();
        }
    }
    /// <summary> 服务器返回角色信息 </summary>
    /// <param name="buffer"></param>
    //private void OnRoleInfoResponse(byte[] buffer)
    //{
    //    RoleInfoResponseProto proto = RoleInfoResponseProto.GetProto(buffer);
    //    if (proto.IsSuccess)
    //    {
    //        GlobalInit.Instance.PlayerInfo = new RoleInfoMainPlayer(proto);
    //        m_LastInWorldMapId = proto.LastInWorldMapId;
    //    }
    //    else
    //    {
    //        TipsUtil.ShowTextTips(proto.MsgCode);
    //    }
    //}

    //private void OnRoleSkillDataResponse(byte[] buffer)
    //{
    //    RoleSkillDataResponseProto proto = RoleSkillDataResponseProto.GetProto(buffer);
    //    GlobalInit.Instance.PlayerInfo.LoadSkill(proto);
    //    PlayerCtrl.Instance.LastInWorldMapId = m_LastInWorldMapId;
    //    SceneMgr.Instance.LoadToWorldMap(m_LastInWorldMapId);
    //}

    private void ShowCreateRoleView()
    {
        m_IsSelectRoleView = true;
        SetPillarsActive(true);
        //没有角色 进入 创建角色界面
        Debuger.Log("进入 创建角色界面");
        //m_SelectRoleView.SelectRoleContainer.gameObject.SetActive(false);
        for (int i = 0; i < m_SelectRoleView.SelectRoleContainer.childCount; i++)
        {
            Destroy(m_SelectRoleView.SelectRoleContainer.GetChild(i).gameObject);
        }
        for (int i = 0; i < SelectRoleContainer.childCount; i++)
        {
            Destroy(SelectRoleContainer.GetChild(i).gameObject);
        }
        m_SelectRoleView.CreateRoleContainer.gameObject.SetActive(true);
        //SelectRoleContainer.gameObject.SetActive(false);
        CloneCreateRole();
    }
    private void SetPillarsActive(bool value)
    {
        for (int i = 0; i < Pillars.Length; i++)
        {
            Pillars[i].SetActive(value);
        }
    }

    private void OnSelectCallBack(int index)
    {
        if (m_SelectRoleItemList.NowSelect == index)
            return;
        if (m_SelectRoleItemList.NowSelect != -1)
        {
            m_SelectRoleItemList.LastSelect = m_SelectRoleItemList.NowSelect;
            m_SelectRoleItemList.GetValue(m_SelectRoleItemList.LastSelect).DOPlayBackwards();
            m_SelectRoleItemList.GetDependValue(m_SelectRoleItemList.LastSelect).SetActive(false);
        }
        m_SelectRoleItemList.NowSelect = index;
        m_SelectRoleItemList.GetValue(m_SelectRoleItemList.NowSelect).DoPlayForward();
        m_SelectRoleItemList.GetDependValue(m_SelectRoleItemList.NowSelect).SetActive(true);
        m_SelectRoleId = m_SelectRoleItemList.GetValue(m_SelectRoleItemList.NowSelect).RoleId;
    }

    [SerializeField]
    /// <summary> 拖拽的目标 </summary>
    private Transform DragTarget;

    /// <summary> 拖拽的角度 </summary>
    private float m_RotateAngle = 90;

    /// <summary> 目标角度 </summary>
    private float m_TargetAngle = 0;

    /// <summary> 是否在旋转中 </summary>
    private bool m_IsRotating = false;

    [SerializeField]
    /// <summary> 旋转速度 </summary>
    private float m_RotateSpeed = 100f;

    /// <summary> 拖拽旋转角色 </summary>
    /// <param name="obj"></param>
    private void OnSelectRoleDrag(int obj)
    {
        if (m_IsRotating)
            return;
        m_RotateAngle = Mathf.Abs(m_RotateAngle) * (obj == 0 ? -1 : 1);
        m_IsRotating = true;
        m_TargetAngle = DragTarget.eulerAngles.y + m_RotateAngle;
        int index = -1;
        if (obj == 0)
        {
            index = m_JobToggleList.NowSelect % 4 + 1;
            index = index > 3 ? 0 : index;
        }
        else
        {
            index = m_JobToggleList.NowSelect % 4 - 1;
            index = index < 0 ? 3 : index;
        }
        m_JobToggleList.Select(index);
    }


    /// <summary> 开始游戏回调 </summary>
    private void OnBtnBeginGameClick()
    {
        if (m_IsSelectRoleView)
        {
            RoleCreateRequestProto proto = new RoleCreateRequestProto();
            proto.JobId = (byte)m_SelectJobId;
            proto.RoleName = m_SelectRoleView.IFNickName.text;
            if (string.IsNullOrEmpty(proto.RoleName))
            {
                TipsUtil.ShowTextTips(1000306);
                return;
            }
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
        else
        {
            if (!m_IsHaveRole)
            {
                RoleCreateRequestProto proto = new RoleCreateRequestProto();
                proto.JobId = (byte)m_SelectJobId;
                proto.RoleName = m_SelectRoleView.IFNickName.text;
                if (string.IsNullOrEmpty(proto.RoleName))
                {
                    TipsUtil.ShowTextTips(1000306);
                    return;
                }
                NetWorkSocket.Instance.SendMsg(proto.ToArray());
            }
            else
            {
                EnterGame();
            }
        }
    }
    private void EnterGame()
    {
        Debug.Log("请求进入游戏");
        RoleInfoRequestProto proto = new RoleInfoRequestProto();
        proto.RoleId = m_SelectRoleId;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    /// <summary> 返回选区回调 </summary>
    private void OnBtnBackGameClick()
    {
        if (SelectRoleContainer.childCount==0)
        {
            //返回选区界面
            SceneMgr.Instance.LoadToLogOn();
        }
        else
        {
            //返回选角色界面
            ShowCreateRoleView();
        }
    }
    private void OnRoleCreateResponse(byte[] buffer)
    {
        RoleCreateResponseProto proto = RoleCreateResponseProto.GetProto(buffer);
        if (proto.IsSuccess)
        {
            Debuger.Log("创建成功");
            LogOnGameServer();
        }
        else
        {
            TipsUtil.ShowTextTips(proto.MsgCode);
        }
    }
    void Update () {
        if (m_IsRotating)
        {
            float toAngle = Mathf.MoveTowardsAngle(DragTarget.eulerAngles.y, m_TargetAngle, Time.deltaTime * m_RotateSpeed);
            DragTarget.eulerAngles = Vector3.up * toAngle;
            if (Mathf.RoundToInt(m_TargetAngle) == Mathf.RoundToInt(toAngle) || Mathf.RoundToInt(m_TargetAngle+360) == Mathf.RoundToInt(toAngle))
            {
                m_IsRotating = false;
                DragTarget.eulerAngles = Vector3.up * toAngle;
            }
        }
	}

    private void OnDestroy()
    {
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleCreateResponse, OnRoleCreateResponse);
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleLogOnGameServerResponse, OnLogOnGameServerResponse);
        //SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleInfoResponse, OnRoleInfoResponse);
        //SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleSkillDataResponse, OnRoleSkillDataResponse);
    }
}
