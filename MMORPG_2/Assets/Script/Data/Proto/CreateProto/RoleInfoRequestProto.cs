//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:21
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 角色信息(请求) </summary>
public struct RoleInfoRequestProto : IProto
{
    public ushort ProtoCode { get { return 10203; } }

    /// <summary> 角色编号 </summary>
    public int RoleId;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleId);
            return ms.ToArray();
        }
    }

    public static RoleInfoRequestProto GetProto(byte[] buffer)
    {
        RoleInfoRequestProto proto = new RoleInfoRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.RoleId = ms.ReadInt();
        }
        return proto;
    }
}