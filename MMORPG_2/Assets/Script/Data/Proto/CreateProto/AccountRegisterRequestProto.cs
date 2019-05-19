//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:21
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 账号注册(请求) </summary>
public struct AccountRegisterRequestProto : IProto
{
    public ushort ProtoCode { get { return 10101; } }

    /// <summary> 账号名 </summary>
    public string Account;
    /// <summary> 密码 </summary>
    public string Pwd;
    /// <summary> 渠道号 </summary>
    public string Channel;
    /// <summary> 设备标识符 </summary>
    public string DeviceIdentifier;
    /// <summary> 设备型号 </summary>
    public string DeviceModel;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteUTF8String(Account);
            ms.WriteUTF8String(Pwd);
            ms.WriteUTF8String(Channel);
            ms.WriteUTF8String(DeviceIdentifier);
            ms.WriteUTF8String(DeviceModel);
            return ms.ToArray();
        }
    }

    public static AccountRegisterRequestProto GetProto(byte[] buffer)
    {
        AccountRegisterRequestProto proto = new AccountRegisterRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.Account = ms.ReadUTF8String();
            proto.Pwd = ms.ReadUTF8String();
            proto.Channel = ms.ReadUTF8String();
            proto.DeviceIdentifier = ms.ReadUTF8String();
            proto.DeviceModel = ms.ReadUTF8String();
        }
        return proto;
    }
}