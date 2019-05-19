using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogOnWindow : UIWindowViewBase {

    [HideInInspector] public InputField IFAccount;
    [HideInInspector] public InputField IFPwd;

    protected override void OnAwake()
    {
        base.OnAwake();
        IFAccount = transform.Find("Account/InputField").GetComponent<InputField>();
        IFPwd = transform.Find("Pwd/InputField").GetComponent<InputField>();
        //IFAccount.text = "fan";
        IFPwd.text = "fan123";
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
            case "LogOn":
                UIDispatcher.Instance.Dispatcher(ConstDefine.UILogOnWindow_LogOn);
                break;
            case "ToRegister":
                UIDispatcher.Instance.Dispatcher(ConstDefine.UILogOnWindow_ToRegister);
                break;
        }
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
