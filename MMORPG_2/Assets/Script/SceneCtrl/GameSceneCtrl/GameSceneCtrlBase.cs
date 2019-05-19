/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-02 13:39:21 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-02 13:39:21 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>  </summary>
public class GameSceneCtrlBase : MonoBehaviour {
    
    protected UISceneMainCityView m_MainCityView;

    /// <summary> 上面部分所有的功能图标列表 </summary>
    private List<MainMenuIconItem> m_MenuIconItemList;

    /// <summary> 折叠或者展示的时候 最后一个播放完动画的item </summary>
    private MainMenuIconItem m_LastPlayOverItem;

    private bool m_IsShow = true;
    private void Awake()
    {
        OnAwake();
        m_MenuIconItemList = new List<MainMenuIconItem>();
        GlobalInit.Instance.OnSkillClick = OnSkillClick;
    }

    private void Start()
    {
        OnStart();
        m_MainCityView = UISceneCtrl.Instance.LoadSceneUI(UISceneCtrl.SceneUIType.MainCity, OnMainCityUILoadComplete).GetComponent<UISceneMainCityView>();
        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
            FingerEvent.Instance.OnZoom += OnZoom;
            FingerEvent.Instance.OnPlayerClick += OnPlayerClick;
        }
        EffectManager.Instance.Init();
        UpdatePhySkill();
        UpdateSkill();
    }

    private void UpdatePhySkill()
    {
        JobEntity jobEntity = JobDBModel.Instance.Get(GlobalInit.Instance.PlayerInfo.JobId);
        SkillEntity skillEntity = SkillDBModel.Instance.Get(jobEntity.PhyAttackIdArray[0]);
        int skillId = jobEntity.PhyAttackIdArray[0];
        int skillLevel = 1;
        float skillCD = skillEntity.SkillCD - (skillLevel - 1) * skillEntity.SkillUpCDTime;
        bool isActive = true;
        TransferData data = new TransferData();
        m_MainCityView.SkillSlotsItemList[0].SetGray(!isActive);
        data.SetValue(ConstDefine.FolderName, skillEntity.SkillIconFolder);
        data.SetValue(ConstDefine.IconName, skillEntity.SkillIconName);
        data.SetValue(ConstDefine.SkillId, skillId);
        data.SetValue(ConstDefine.SkillLevel, skillLevel);
        data.SetValue(ConstDefine.SkillCD, skillCD);
        data.SetValue(ConstDefine.IsActive, isActive);
        m_MainCityView.SkillSlotsItemList[0].SetData(data);
    }

    private void UpdateSkill()
    {
        JobEntity jobEntity = JobDBModel.Instance.Get(GlobalInit.Instance.PlayerInfo.JobId);
        List<int> skillIdList = new List<int>(jobEntity.SkillAttackIdArray);
        //更新1-6技能槽
        for (int i = 1; i < m_MainCityView.SkillSlotsItemList.Count-1; i++)
        {
            TransferData data = new TransferData();
            SkillEntity skillEntity;
            int skillId = 0;
            int skillLevel = 0;
            float skillCD = 0;
            bool isActive = false;
            //该技能槽有技能 设置服务器的数据
            if (GlobalInit.Instance.PlayerInfo.SkillDic.ContainsKey(i))
            {
                skillEntity = SkillDBModel.Instance.Get(GlobalInit.Instance.PlayerInfo.SkillDic[i].SkillId);
                if (skillIdList.Contains(GlobalInit.Instance.PlayerInfo.SkillDic[i].SkillId))
                {
                    skillIdList.Remove(GlobalInit.Instance.PlayerInfo.SkillDic[i].SkillId);
                }
                skillId = GlobalInit.Instance.PlayerInfo.SkillDic[i].SkillId;
                skillLevel = GlobalInit.Instance.PlayerInfo.SkillDic[i].SkillLevel;
                skillCD = skillEntity.SkillCD - (skillLevel - 1) * skillEntity.SkillUpCDTime;
                isActive = true;
            }
            else
            {
                //技能槽为空 设置配置表中的数据
                skillEntity = SkillDBModel.Instance.Get(skillIdList[0]);
                skillId = skillIdList[0];
                skillIdList.Remove(skillIdList[0]);
            }
            m_MainCityView.SkillSlotsItemList[i].SetGray(!isActive);
            data.SetValue(ConstDefine.FolderName, skillEntity.SkillIconFolder);
            data.SetValue(ConstDefine.IconName, skillEntity.SkillIconName);
            data.SetValue(ConstDefine.SkillId, skillId);
            data.SetValue(ConstDefine.SkillLevel, skillLevel);
            data.SetValue(ConstDefine.SkillCD, skillCD);
            data.SetValue(ConstDefine.IsActive, isActive);
            m_MainCityView.SkillSlotsItemList[i].SetData(data);
        }
    }


    private void Update()
    {
        OnUpdate();
    }


    public void UpdateMainMenuIcon(bool isHide = false)
    {
        //加载图标

        List<MainMenuIcon1Entity> menuIcon1List = MainMenuIcon1DBModel.Instance.GetList();
        SortUtil.Sort(menuIcon1List, "Weight", true);
        List<MainMenuIcon2Entity> menuIcon2List = MainMenuIcon2DBModel.Instance.GetList();
        SortUtil.Sort(menuIcon2List, "Weight", true);
        GameObject obj = ResourcesManager.Instance.LoadItem("MainCity/MainMenuIconItem");
        for (int i = 0; i < menuIcon1List.Count; i++)
        {
            if (menuIcon1List[i].ShowLevel > GlobalInit.Instance.CurrPlayer.CurrRoleInfo.Level)
                continue;
            GameObject itemObj = Instantiate(obj);
            MainMenuIconItem item = itemObj.GetComponent<MainMenuIconItem>();
            MainMenuIconItemInfo info = new MainMenuIconItemInfo();
            bool isLockIcon = false;
            if (menuIcon1List[i].LimitLevel > GlobalInit.Instance.CurrPlayer.CurrRoleInfo.Level)
                isLockIcon = true;
            else
                isLockIcon = false;
            info.Row = 1;
            info.Index = i + 1;
            info.AtlasName = menuIcon1List[i].AtlasName;
            info.IconName = menuIcon1List[i].IconName;
            info.IsLockActive = isLockIcon;
            info.IsRedDotActive = UnityEngine.Random.Range(1, 10) > 5;
            info.ActivityName = menuIcon1List[i].Name;
            info.OpenWinName = menuIcon1List[i].OpenWindowName;
            item.SetData(info, OnMenuIconClickCallBack);
            m_MenuIconItemList.Add(item);
            if (i == menuIcon1List.Count - 1)
                m_LastPlayOverItem = item;
        }
        for (int i = 0; i < menuIcon2List.Count; i++)
        {
            if (menuIcon2List[i].ShowLevel > GlobalInit.Instance.CurrPlayer.CurrRoleInfo.Level)
                continue;
            GameObject itemObj = Instantiate(obj);
            MainMenuIconItem item = itemObj.GetComponent<MainMenuIconItem>();
            MainMenuIconItemInfo info = new MainMenuIconItemInfo();
            bool isLockIcon = false;
            if (menuIcon2List[i].LimitLevel > GlobalInit.Instance.CurrPlayer.CurrRoleInfo.Level)
                isLockIcon = true;
            else
                isLockIcon = false;
            info.Row = 2;
            info.Index = i + 1;
            info.AtlasName = menuIcon2List[i].AtlasName;
            info.IconName = menuIcon2List[i].IconName;
            info.IsLockActive = isLockIcon;
            info.IsRedDotActive = UnityEngine.Random.Range(1, 10) > 5;
            info.ActivityName = menuIcon2List[i].Name;
            info.OpenWinName = menuIcon2List[i].OpenWindowName;
            item.SetData(info, OnMenuIconClickCallBack);
            m_MenuIconItemList.Add(item);
            if (info.Index > m_LastPlayOverItem.Index)
                m_LastPlayOverItem = item;
        }
        MainMenuAndMapView.Instance.SetMainMenu(m_MenuIconItemList, OnShowOrHideCallBack);
        if (isHide)
        {
            OnShowOrHideCallBack();
        }
    }

    private void OnMenuIconClickCallBack(string openWindowName)
    {
        Debuger.Log("需要打开" + openWindowName + "界面");
        switch (openWindowName)
        {
            case Window.GameLevelDetail:
                return;
        }
        if (openWindowName == "Pan_Tips")
        {
            TipsUtil.ShowWindowTips("需要打开" + openWindowName + "界面");
        }
        else
        {
            WindowManager.Instance.OpenWindow(openWindowName);
        }
    }

    private void OnShowOrHideCallBack()
    {
        if (m_LastPlayOverItem.IsPlaying)
        {
            Debuger.Log("动画播放中");
            return;
        }
        if (m_IsShow)
        {
            for (int i = 0; i < m_MenuIconItemList.Count; i++)
            {
                m_MenuIconItemList[i].DoPlayerForward();
            }
            MainMenuAndMapView.Instance.IsShow(true);
        }
        else
        {
            for (int i = 0; i < m_MenuIconItemList.Count; i++)
            {
                m_MenuIconItemList[i].DOPlayBackwards();
            }
            MainMenuAndMapView.Instance.IsShow(false);
        }
        m_IsShow = !m_IsShow;
    }


    private void OnDestroy()
    {
        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
            FingerEvent.Instance.OnZoom -= OnZoom;
            FingerEvent.Instance.OnPlayerClick -= OnPlayerClick;
        }
        EffectManager.Instance.Clear();
        BeforOnDestory();
    }



    #region OnZoom 摄像机缩放
    /// <summary>
    /// 摄像机缩放
    /// </summary>
    /// <param name="obj"></param>
    private void OnZoom(FingerEvent.ZoomType obj)
    {
        switch (obj)
        {
            case FingerEvent.ZoomType.In:
                CameraCtrl.Instance.SetCameraZoom(0);
                break;
            case FingerEvent.ZoomType.Out:
                CameraCtrl.Instance.SetCameraZoom(1);
                break;
        }
    }
    #endregion

    #region OnPlayerClickGround 玩家点击
    /// <summary>
    /// 玩家点击
    /// </summary>
    private void OnPlayerClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hitArr;

        hitArr = Physics.RaycastAll(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("Role"));
        if (hitArr.Length > 0)
        {
            RoleCtrl hitRole = hitArr[0].collider.gameObject.GetComponent<RoleCtrl>();
            if (hitRole.CurrRoleType == RoleType.Monster)
            {
                GlobalInit.Instance.CurrPlayer.LockEnemy = hitRole;
                return;
            }
        }
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000f, 1 << LayerMask.NameToLayer("Ground")))
        {
            if (GlobalInit.Instance.CurrPlayer != null)
            {
            
                if (SceneMgr.Instance.CurrSceneType == ConstDefine.GameLevel)
                {
                    Vector3 point = new Vector3(hitInfo.point.x, hitInfo.point.y + 50, hitInfo.point.z);
                    if (Physics.Raycast(point, new Vector3(0, -100, 0), out hitInfo, 1000f, 1 << LayerMask.NameToLayer("RegionMask")))
                    {
                        return;
                    }
                }
                GlobalInit.Instance.CurrPlayer.LockEnemy = null;
                GlobalInit.Instance.CurrPlayer.MoveTo(hitInfo.point);
            }
        }
    }
    #endregion


    #region OnFingerDrag 手指滑动
    /// <summary>
    /// 手指滑动
    /// </summary>
    /// <param name="obj"></param>
    private void OnFingerDrag(FingerEvent.FingerDir obj)
    {
        switch (obj)
        {
            case FingerEvent.FingerDir.Left:
                CameraCtrl.Instance.SetCameraRotate(0);
                break;
            case FingerEvent.FingerDir.Right:
                CameraCtrl.Instance.SetCameraRotate(1);
                break;
            case FingerEvent.FingerDir.Up:
                CameraCtrl.Instance.SetCameraUpAndDown(1);
                break;
            case FingerEvent.FingerDir.Down:
                CameraCtrl.Instance.SetCameraUpAndDown(0);
                break;
        }
    }
    #endregion

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TipsUtil.ShowExpTips(10000);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            TipsUtil.ShowCoinTips(10000);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TipsUtil.ShowTextTips("世事如棋 乾坤莫测");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            TipsUtil.ShowGoldTips(666666);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            TipsUtil.ShowGoldBindingTips(888888);
        }
    }
    protected virtual void BeforOnDestory() { }

    protected virtual void OnMainCityUILoadComplete()
    {
        m_MainCityView.BtnCallBack = OnBtnCallBack;
    }

    protected virtual void OnBtnCallBack(GameObject go)
    {
        switch (go.name)
        {
            case "Icon":
            case "MainButton_Bag":
                PlayerCtrl.Instance.OpenWindow(Window.RoleInfo);
                break;
            case "HideSkill":
                PlayerCtrl.Instance.OpenWindow(Window.RoleInfo);
                break;
            case "ChangeSkill":
                m_MainCityView.SkillListTrans.DORotate(new Vector3(0, 0, m_MainCityView.SkillListTrans.rotation.z - 180), 0.3f,RotateMode.LocalAxisAdd);
                break;
            case "NormalAttackIcon":
                //GlobalInit.Instance.CurrPlayer.ToAttack(RoleAttackType.PhyAttack, go.transform.parent.GetComponent<UISkillSlotsItem>().SkillId);
                OnPhyAttack();
                break;
            case "Skill1Icon":
            case "Skill2Icon":
            case "Skill3Icon":
            case "Skill4Icon":
            case "Skill5Icon":
            case "Skill6Icon":
                ToSkillAttack(go.transform.parent.GetComponent<UISkillSlotsItem>());
                break;
            case "AutoFight":
                GlobalInit.Instance.IsAutoFight = m_MainCityView.AutoFight();
                break;
        }
    }

    private void OnPhyAttack()
    {
        if (GlobalInit.Instance.SkillSlotsItem != null)
        {
            GlobalInit.Instance.SkillSlotsItem = null;
        }
        if (GlobalInit.Instance.CurrPlayer.IsRigidity)
        {
            //技能施放中
            TipsUtil.ShowTextTips(1000503);
            return;
        }
        int[] phyIdArray = JobDBModel.Instance.Get(GlobalInit.Instance.PlayerInfo.JobId).PhyAttackIdArray;
        int id = UnityEngine.Random.Range(phyIdArray[0], phyIdArray[phyIdArray.Length - 1]);
        GlobalInit.Instance.CurrPlayer.ToAttack(RoleAttackType.PhyAttack, id);
    }

    private void ToSkillAttack(UISkillSlotsItem item)
    {
        //if (GlobalInit.Instance.CurrPlayer.IsRigidity)
        //{
            //技能施放中
            //TipsUtil.ShowTextTips(1000503);
            //return;
        //}
        if (!item.IsActive)
        {
            SceneMgr.Instance.LoadToWorldMap(PlayerCtrl.Instance.LastInWorldMapId);
            //技能未开启
            TipsUtil.ShowTextTips(1000502);
            return;
        }
        if (item.LeftTime > 0)
        {
            //技能冷却中
            TipsUtil.ShowTextTips(1000501);
            return;
        }
        GlobalInit.Instance.SkillSlotsItem = item;
        GlobalInit.Instance.CurrPlayer.ToAttack(RoleAttackType.SkillAttack, item.SkillId);
    }

    private void OnSkillClick(int skillId)
    {
        for (int i = 0; i < m_MainCityView.SkillSlotsItemList.Count; i++)
        {
            if (skillId == m_MainCityView.SkillSlotsItemList[i].SkillId)
            {
                GlobalInit.Instance.SkillSlotsItem = m_MainCityView.SkillSlotsItemList[i];
                ToSkillAttack(GlobalInit.Instance.SkillSlotsItem);
                break;
            }
        }
    }
}
