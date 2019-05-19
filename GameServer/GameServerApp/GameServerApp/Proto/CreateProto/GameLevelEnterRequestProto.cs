//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 客户端发送进去游戏关卡消息 </summary>
public struct GameLevelEnterRequestProto : IProto
{
    public ushort ProtoCode { get { return 10401; } }

    /// <summary> 游戏Id </summary>
    public int GameChapterId;
    /// <summary> 难度等级 </summary>
    public byte Grade;
    /// <summary> 章节关卡Id </summary>
    public int GameLevelId;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(GameChapterId);
            ms.WriteByte(Grade);
            ms.WriteInt(GameLevelId);
            return ms.ToArray();
        }
    }

    public static GameLevelEnterRequestProto GetProto(byte[] buffer)
    {
        GameLevelEnterRequestProto proto = new GameLevelEnterRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.GameChapterId = ms.ReadInt();
            proto.Grade = (byte)ms.ReadByte();
            proto.GameLevelId = ms.ReadInt();
        }
        return proto;
    }
}