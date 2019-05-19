//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:21
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 创建角色(请求) </summary>
public struct RoleCreateRequestProto : IProto
{
    public ushort ProtoCode { get { return 10202; } }

    /// <summary> 职业Id </summary>
    public byte JobId;
    /// <summary> 角色名称 </summary>
    public string RoleName;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteByte(JobId);
            ms.WriteUTF8String(RoleName);
            return ms.ToArray();
        }
    }

    public static RoleCreateRequestProto GetProto(byte[] buffer)
    {
        RoleCreateRequestProto proto = new RoleCreateRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.JobId = (byte)ms.ReadByte();
            proto.RoleName = ms.ReadUTF8String();
        }
        return proto;
    }
}