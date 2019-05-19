using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 计时器管理类 </summary>
public class FrameTimerManager:Singleton<FrameTimerManager>
{
	public FrameTimerManager()
	{
	}

    private List<TimeFrameItem> list = new List<TimeFrameItem>();
		
	public void Clear(){
        list.Clear();
	}

    /// <summary>
    /// 延迟回调
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="callback"></param>
    public void DelayCall(float delay, TimeFrameItem.OnTick0 callback)
    {
        Add("DelayCall", delay, callback, 1);
    }

    /// <summary>
    /// 添加计时器
    /// </summary>
    /// <param name="name">计时器名称</param>
    /// <param name="delay">多少秒执行一次</param>
    /// <param name="callback">回调函数</param>
    /// <param name="repeat">重复次数，-1为无限次</param>
    /// <param name="onOver">结束函数</param>
    /// <param name="priority">优先级</param>
    public void Add(string name, float delay, TimeFrameItem.OnTick0 callback, int repeat = -1, TimeFrameItem.OnTick0 onOver = null, int priority = 10)
    {
        if (HasFunction(callback) == -1)
        {
            TimeFrameItem fti = new TimeFrameItem(name, delay, repeat, callback, onOver, priority);
            list.Add(fti);
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].callback == callback)
                {
                    list[i].delay = delay;
                    list[i].repeat = repeat;
                    list[i].SetEnd = false;
                }
            }
        }
    }

    public void RemoveCallback(TimeFrameItem.OnTick0 callback)
    {
        int index = HasFunction(callback);
        if (index != -1)
        {
            list[index].SetEnd = true;
        }
    }

    public int HasFunction(TimeFrameItem.OnTick0 callback)
    {
        int i = 0;
        for (i = 0; i < list.Count; i++)
        {
			if (list[i].callback == callback && list[i].IsEnd == false)
            {
                return i;
            }
        }
        return -1;
    }

	public void FrameHandle()
	{
        int i = 0;
        float nowTime = Time.time;
        for (i = 0; i < list.Count; i++ )
        {
            if (list[i].IsEnd == false)
                list[i].Execute(nowTime);
        }
        for (i = 0; i < list.Count; i++)
        {
            if (list[i].IsEnd == true)
            {
                list.RemoveAt(i);
                i--;
            }
        }
	}
}
