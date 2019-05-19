/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2018-11-21 16:22:06 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2018-11-21 16:22:06 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
/// <summary>  </summary>
public class UISceneSelectRoleView : UISceneViewBase {


    public UISelectRoleDragView SelectRoleDragView;

    [HideInInspector]
    public Transform CreateRoleContainer;
    [HideInInspector]
    public Transform SelectRoleContainer;

    public List<UICreateRoleJobItem> JobItemList;
    public List<GameObject> JobItemDependList;

    private Transform m_Trans;
    [HideInInspector]
    public InputField IFNickName;

    [HideInInspector]
    public int Gender;
    public Action OnBtnBeginGameClick;
    public Action OnBtnBackGameClick;
    private void Awake()
    {
        m_Trans = transform;
        CreateRoleContainer = m_Trans.Find("Canvas/CreateRoleContainer");
        SelectRoleContainer = m_Trans.Find("Canvas/SelectRoleContainer");
        IFNickName = CreateRoleContainer.Find("Container_Bottom/InputField").GetComponent<InputField>();

    }
    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "BtnRandom":
                RandomName(Gender);
                break;
            case "BtnBack":
                if (OnBtnBackGameClick != null) OnBtnBackGameClick();
                break;
            case "BtnEnter":
                if (OnBtnBeginGameClick != null) OnBtnBeginGameClick();
                break;
        }
    }

    public void RandomName(int gender)
    {
        IFNickName.text = GameUtil.RandomName(gender);
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        SelectRoleDragView = null;
        CreateRoleContainer = null;
        SelectRoleContainer = null;
        if (JobItemList!=null)
        {
            for (int i = 0; i < JobItemList.Count; i++)
            {
                JobItemList[i] = null;
            }
        }
        if (JobItemDependList != null)
        {
            for (int i = 0; i < JobItemDependList.Count; i++)
            {
                JobItemDependList[i] = null;
            }
        }
        m_Trans = null;
        IFNickName = null;
        OnBtnBeginGameClick = null;
        OnBtnBackGameClick = null;
    }
}
