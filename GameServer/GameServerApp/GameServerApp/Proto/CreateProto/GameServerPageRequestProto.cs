//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 请求服务器组列表(请求) </summary>
public struct GameServerPageRequestProto : IProto
{
    public ushort ProtoCode { get { return 10111; } }


    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            return ms.ToArray();
        }
    }

    public static GameServerPageRequestProto GetProto(byte[] buffer)
    {
        GameServerPageRequestProto proto = new GameServerPageRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
        }
        return proto;
    }
}