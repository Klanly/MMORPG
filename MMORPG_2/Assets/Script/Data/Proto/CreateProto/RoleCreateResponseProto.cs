//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:21
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 创建角色(返回) </summary>
public struct RoleCreateResponseProto : IProto
{
    public ushort ProtoCode { get { return 50202; } }

    /// <summary> 是否创建成功 </summary>
    public bool IsSuccess;
    /// <summary> 消息码 </summary>
    public int MsgCode;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(!IsSuccess)
            {
                ms.WriteInt(MsgCode);
            }
            return ms.ToArray();
        }
    }

    public static RoleCreateResponseProto GetProto(byte[] buffer)
    {
        RoleCreateResponseProto proto = new RoleCreateResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
                proto.MsgCode = ms.ReadInt();
            }
        }
        return proto;
    }
}