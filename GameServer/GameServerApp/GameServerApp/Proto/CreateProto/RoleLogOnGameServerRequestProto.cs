//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 登录区服(请求) </summary>
public struct RoleLogOnGameServerRequestProto : IProto
{
    public ushort ProtoCode { get { return 10201; } }

    /// <summary>  </summary>
    public int AccountId;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(AccountId);
            return ms.ToArray();
        }
    }

    public static RoleLogOnGameServerRequestProto GetProto(byte[] buffer)
    {
        RoleLogOnGameServerRequestProto proto = new RoleLogOnGameServerRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.AccountId = ms.ReadInt();
        }
        return proto;
    }
}