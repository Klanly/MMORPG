using UnityEngine;
using System.Collections;

public class LogOnSceneCtrl : MonoBehaviour {

    GameObject obj;

    void Awake()
    {
        UISceneCtrl.Instance.LoadSceneUI(UISceneCtrl.SceneUIType.LogOn);
    }

	// Use this for initialization
	void Start () {

        if (DelegateDefine.Instance.OnSceneLoadOk != null)
        {
            DelegateDefine.Instance.OnSceneLoadOk();
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}