using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 提示工具类 </summary>
public class TipsUtil {

    /// <summary> Text提示 </summary>
    /// <param name="textTips">提示信息</param>
    public static void ShowTextTips(string textTips)
    {
        TextTipsUtil.Instance.ShowTips(textTips);
    }

    /// <summary> 根据Id获取提示信息 </summary>
    /// <param name="textId">提示ID</param>
    public static void ShowTextTips(int textId)
    {
        TextTipsUtil.Instance.ShowTips(StringUtil.GetStringById(textId));
    }
    /// <summary> 获取富文本提示信息 </summary>
    /// <param name="textId">提示ID</param>
    /// <param name="color">颜色</param>
    public static void ShowTextTips(int textId, string color)
    {
        TextTipsUtil.Instance.ShowTips(StringUtil.GetRichString(textId, color));
    }

    public static void ShowExpTips(int expValue)
    {
        TextTipsUtil.Instance.ShowTips("+ "+expValue,TextTipsType.ExpTips);
    }
    public static void ShowCoinTips(int coinValue)
    {
        TextTipsUtil.Instance.ShowTips("+ " + coinValue, TextTipsType.CoinTips);
    }
    public static void ShowGoldTips(int goldValue)
    {
        TextTipsUtil.Instance.ShowTips("+ " + goldValue, TextTipsType.GoldTips);
    }
    public static void ShowGoldBindingTips(int goldValue)
    {
        TextTipsUtil.Instance.ShowTips("+ " + goldValue, TextTipsType.GoldBindingTips);
    }

    /// <summary> 窗口提示 </summary>
    /// <param name="message">提示信息</param>
    /// <param name="title">标题(默认值 1000108:提示信息)</param>
    /// <param name="btnCount">按钮个数(1.只有确定按钮，2.确定和取消)</param>
    /// <param name="confirmCallBack">确定回调</param>
    /// <param name="cancelCallBack">取消回调</param>
    public static void ShowWindowTips(string message, string title = "", int btnCount = 2, Action confirmCallBack = null, Action cancelCallBack = null)
    {
        if (string.IsNullOrEmpty(title))
            title = LanguageUtil.GetStrById(1000108);
        WindowTipsUtil.Instance.Show(message, title, btnCount, confirmCallBack, cancelCallBack);
    }
}
