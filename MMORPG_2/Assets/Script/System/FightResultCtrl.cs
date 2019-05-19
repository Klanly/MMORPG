/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-21 08:33:51 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-21 08:33:51 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class FightResultCtrl : SystemCtrlBase<FightResultCtrl>, ISystemCtrl
{
    private UIFightResultWindow m_FightResultWin;
    public void OpenWindow(string winName)
    {
        switch (winName)
        {
            case Window.FightResult:
                OpenFightResultWindow();
                break;
        }
    }

    private void OpenFightResultWindow()
    {
        m_FightResultWin = WindowUtil.Instance.OpenWindow(Window.FightResult).GetComponent<UIFightResultWindow>();
        m_FightResultWin.OnRoleResurgenceCallBack = OnRoleResurgenceCallBack;
        TransferData data = new TransferData();
        data.SetValue(ConstDefine.FightValue, GameLevelCtrl.Instance.Result);
        data.SetValue(ConstDefine.UseTime, GameLevelCtrl.Instance.UseTime);
        data.SetValue(ConstDefine.ExpValue, GameLevelCtrl.Instance.DropExp);
        data.SetValue(ConstDefine.CoinValue, GameLevelCtrl.Instance.DropCoin);
        data.SetValue(ConstDefine.CurrStar, GameLevelCtrl.Instance.CurrStarCount);
        data.SetValue(ConstDefine.MaxStar, 3);

        m_FightResultWin.SetData(data);
    }

    private void OnRoleResurgenceCallBack()
    {
        m_FightResultWin.Close();
        GameUtil.OnGameLevelRoleResurgence();
    }
}
