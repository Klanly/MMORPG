//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:21
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 账号登录(请求) </summary>
public struct AccountLogOnRequestProto : IProto
{
    public ushort ProtoCode { get { return 10102; } }

    /// <summary> 用户名 </summary>
    public string UserName;
    /// <summary> 密码 </summary>
    public string Pwd;
    /// <summary> 设备标识符 </summary>
    public string DeviceIdentifier;
    /// <summary> 设备型号 </summary>
    public string DeviceModel;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteUTF8String(UserName);
            ms.WriteUTF8String(Pwd);
            ms.WriteUTF8String(DeviceIdentifier);
            ms.WriteUTF8String(DeviceModel);
            return ms.ToArray();
        }
    }

    public static AccountLogOnRequestProto GetProto(byte[] buffer)
    {
        AccountLogOnRequestProto proto = new AccountLogOnRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.UserName = ms.ReadUTF8String();
            proto.Pwd = ms.ReadUTF8String();
            proto.DeviceIdentifier = ms.ReadUTF8String();
            proto.DeviceModel = ms.ReadUTF8String();
        }
        return proto;
    }
}