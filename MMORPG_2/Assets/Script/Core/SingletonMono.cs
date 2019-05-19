using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T>:MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject(typeof(T).Name);
                DontDestroyOnLoad(obj);
                instance = obj.GetOrAddComponent<T>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }
    private void Update()
    {
        OnUpdate();
    }

    private void OnDestroy()
    {
        BeforeOnDestroy();
    }
    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void BeforeOnDestroy() { }
}
