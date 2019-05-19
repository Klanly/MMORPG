using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIRegisterWindow : UIWindowViewBase {

    [HideInInspector] public InputField IFAccount;
    [HideInInspector] public InputField IFPwd;
    [HideInInspector] public InputField IFAgainPwd;
    protected override void OnAwake()
    {
        base.OnAwake();
        IFAccount = transform.Find("Account/InputField").GetComponent<InputField>();
        IFPwd = transform.Find("Pwd/InputField").GetComponent<InputField>();
        IFAgainPwd = transform.Find("AgainPwd/InputField").GetComponent<InputField>();
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
            case "Register":
                UIDispatcher.Instance.Dispatcher(ConstDefine.UIRegisterWindow_Register);
                break;
            case "ToLogOn":
                UIDispatcher.Instance.Dispatcher(ConstDefine.UIRegisterWindow_ToLogOn);
                break;
        }
    }

    public override void Close()
    {
        base.Close();
    }
}
