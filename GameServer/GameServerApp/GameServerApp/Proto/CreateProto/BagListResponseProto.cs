//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 背包列表(返回) </summary>
public struct BagListResponseProto : IProto
{
    public ushort ProtoCode { get { return 50301; } }


    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            return ms.ToArray();
        }
    }

    public static BagListResponseProto GetProto(byte[] buffer)
    {
        BagListResponseProto proto = new BagListResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
        }
        return proto;
    }
}