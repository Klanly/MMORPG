using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 统计类(接第三方平台) </summary>
public class Stat {

    public static void Init()
    {

    }

    /// <summary> 注册 </summary>
    /// <param name="userId"></param>
    /// <param name="nickName"></param>
    public static void Register(int userId, string nickName)
    {

    }

    public static void LogOn(int userId,string nickName)
    {

    }

    public static void ChangeNickName(string nickName)
    {

    }

    public static void UpLevel(int level)
    {

    }

    //===============================任务
    public static void TaskBegin(int taskId,string taskName)
    {

    }

    public static void TaskEnd(int taskId,string taskName,int status)
    {

    }

    //===============================任务
    public static void GameLevelBegin(int gameLevelId, string taskName)
    {

    }

    public static void GameLevelEnd(int gameLevelId, string taskName, int status,int star)
    {

    }

    /// <summary> 充值 </summary>
    /// <param name="orderId">订单号</param>
    /// <param name="productId">产品编号</param>
    /// <param name="money">充值金额</param>
    /// <param name="type">币种</param>
    /// <param name="virtualMoney">虚拟金额</param>
    /// <param name="channelId">渠道号</param>
    public static void RechargeBegin(string orderId,string productId,double money,string type,double virtualMoney,string channelId)
    {

    }

    public static void RechargeEnd()
    {

    }

    public static void BuyItem(int itemId,string itemName,int price,int count)
    {

    }

    public static void ItemUse(int itemId, string itemName, int count,int useType)
    {

    }

    public static void AddEvent(string key,string value)
    {

    }
}
