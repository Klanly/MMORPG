using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestScene : MonoBehaviour {

    // Use this for initialization
    //public Animator Anim;
    //public Button btn;

    private void Awake()
    {
       
    }
    void Start () {
        //WindowManager.Instance.OpenWindow(Window.RolePackage);
        //GameObject obj = AssetBundleManager.Instance.LoadClone(@"Role\role_mainplayer.assetbundle", "Role_MainPlayer");
        //AssetBundleLoaderAsync async = AssetBundleManager.Instance.LoadAsync(@"download\model\player\dali\sz_dali_xsz.assetbundle", "sz_dali_xsz");
        //async.OnLoadComplete = OnLoadComplete;
        //AssetBundleManager.Instance.LoadClone(@"download\model\player\dali\sz_dali_xsz.assetbundle", "sz_dali_xsz");
        //Debug.Log(TestDBModel.Instance.Get(1).NameList[1]);
        //int i = 1;
        //btn.onClick.AddListener(delegate
        //{
        //    i++;
        //    if (i%2==0)
        //    {
        //        Debug.Log("ToRun");
        //        Anim.SetBool("ToIdle", false);
        //        Anim.SetBool("ToRun", true);
        //        AnimatorStateInfo info = Anim.GetCurrentAnimatorStateInfo(0);
        //        if (info.IsName("Run"))
        //        {
        //            Anim.SetInteger("CurrState", 2);
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("ToIdle");
        //        Anim.SetBool("ToIdle", true);
        //        Anim.SetBool("ToRun", false);
        //        AnimatorStateInfo info = Anim.GetCurrentAnimatorStateInfo(0);
        //        if (info.IsName("Idle"))
        //        {
        //            Anim.SetInteger("CurrState", 1);
        //        }
        //    }
        //});
    }

    private void OnLoadComplete(UnityEngine.Object obj)
    {
        Instantiate((GameObject)obj);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            
        }
	}
}
