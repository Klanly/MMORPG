//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 请求服务器单组列表(请求) </summary>
public struct GameServerOnePageRequestProto : IProto
{
    public ushort ProtoCode { get { return 10112; } }

    /// <summary> 页数 </summary>
    public int PageIndex;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(PageIndex);
            return ms.ToArray();
        }
    }

    public static GameServerOnePageRequestProto GetProto(byte[] buffer)
    {
        GameServerOnePageRequestProto proto = new GameServerOnePageRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.PageIndex = ms.ReadInt();
        }
        return proto;
    }
}