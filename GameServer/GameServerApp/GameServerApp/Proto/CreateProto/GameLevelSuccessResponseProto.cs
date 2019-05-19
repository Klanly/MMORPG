//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 服务器返回关卡胜利协议 </summary>
public struct GameLevelSuccessResponseProto : IProto
{
    public ushort ProtoCode { get { return 50402; } }

    /// <summary> 是否胜利 </summary>
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

    public static GameLevelSuccessResponseProto GetProto(byte[] buffer)
    {
        GameLevelSuccessResponseProto proto = new GameLevelSuccessResponseProto();
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