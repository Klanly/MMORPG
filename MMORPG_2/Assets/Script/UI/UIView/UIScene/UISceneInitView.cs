using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISceneInitView : MonoBehaviour {

    private Transform m_Trans;
    [HideInInspector]
    public RawImage Bg;
    [HideInInspector]
    public Slider SliderLoad;
    [HideInInspector]
    public Text SliderText;
    [HideInInspector]
    public Text TextLoad;

    private void Awake()
    {
        m_Trans = transform;
        Bg = m_Trans.GetComponent<RawImage>("Bg");
        SliderLoad = m_Trans.GetComponent<Slider>("SliderLoad");
        SliderText = m_Trans.GetComponent<Text>("SliderLoad/ValueText");
        TextLoad = m_Trans.GetComponent<Text>("TextLoad");    }
}
