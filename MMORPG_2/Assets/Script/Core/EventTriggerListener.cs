using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary> UGUI点击监听事件 </summary>
public class EventTriggerListener :MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerUpHandler,
    ISelectHandler,
    IUpdateSelectedHandler
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
    public VoidDelegate onDoubleClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;
    public static EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount==1)
        {
            if (onClick != null)
                onClick(gameObject);
        }
        if (eventData.clickCount == 2)
        {
            if (onDoubleClick != null)
                onDoubleClick(gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null)
            onDown(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null)
            onEnter(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null)
            onExit(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null)
            onUp(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null)
            onSelect(gameObject);
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null)
            onUpdateSelect(gameObject);
    }
}