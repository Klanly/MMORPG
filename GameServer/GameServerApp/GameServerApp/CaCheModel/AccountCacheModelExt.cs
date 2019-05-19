using GameServerApp;
using GameServerApp.Common;
using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class AccountCacheModel
{
    public void Init()
    {
        //客户端请求注册
        EventDispatcher.Instance.AddEventListener(ProtoCodeDef.AccountRegisterRequest, OnAccountRegisterRequest);
        //客户端请求登录
        EventDispatcher.Instance.AddEventListener(ProtoCodeDef.AccountLogOnRequest, OnAccountLogOnRequest);
    }

    private void OnAccountLogOnRequest(Role role, byte[] buffer)
    {
        AccountLogOnRequestProto proto = AccountLogOnRequestProto.GetProto(buffer);
        AccountLogOnResponseProto responseProto = LogOn(proto.UserName, proto.Pwd, proto.DeviceIdentifier, proto.DeviceModel);
        role.ClientSocket.SendMsg(responseProto.ToArray());
    }

    private void OnAccountRegisterRequest(Role role, byte[] buffer)
    {
        AccountRegisterRequestProto proto = AccountRegisterRequestProto.GetProto(buffer);
        //Console.WriteLine(proto.Account + "  " + proto.Pwd + "   " + proto.Channel);
        AccountRegisterResponseProto responseProto = Register(proto.Account, proto.Pwd,proto.Channel,proto.DeviceIdentifier,proto.DeviceModel);
        //AccountCacheModel.Instance.Register
        role.ClientSocket.SendMsg(responseProto.ToArray());
    }

    public AccountRegisterResponseProto Register(string userName, string pwd, string chennelId, string deviceIdentifier, string deviceModel)
    {
        return this.DBModel.Register(userName, pwd,chennelId,deviceIdentifier,deviceModel);
    }

    public AccountLogOnResponseProto LogOn(string userName, string pwd,string deviceIdentifier,string deviceModel)
    {
        return this.DBModel.LogOn(userName, pwd,deviceIdentifier,deviceModel);
    }

    public void Dispose()
    {
        EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.AccountRegisterRequest, OnAccountRegisterRequest);
        EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.AccountLogOnRequest, OnAccountLogOnRequest);
    }
}
