//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:21
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 服务器返回关卡消息 </summary>
public struct GameLevelEnterResponseProto : IProto
{
    public ushort ProtoCode { get { return 50401; } }

    /// <summary> 是否进入成功 </summary>
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
            }
            ms.WriteInt(MsgCode);
            return ms.ToArray();
        }
    }

    public static GameLevelEnterResponseProto GetProto(byte[] buffer)
    {
        GameLevelEnterResponseProto proto = new GameLevelEnterResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
            }
            proto.MsgCode = ms.ReadInt();
        }
        return proto;
    }
}