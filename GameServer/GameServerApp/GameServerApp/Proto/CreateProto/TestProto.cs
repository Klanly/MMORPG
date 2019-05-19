//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 测试协议 </summary>
public struct TestProto : IProto
{
    public ushort ProtoCode { get { return 10001; } }

    /// <summary> 编号 </summary>
    public int Id;
    /// <summary> 名称 </summary>
    public string Name;
    /// <summary> 类型 </summary>
    public int Type;
    /// <summary> 价格 </summary>
    public float Price;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(Id);
            ms.WriteUTF8String(Name);
            ms.WriteInt(Type);
            ms.WriteFloat(Price);
            return ms.ToArray();
        }
    }

    public static TestProto GetProto(byte[] buffer)
    {
        TestProto proto = new TestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.Id = ms.ReadInt();
            proto.Name = ms.ReadUTF8String();
            proto.Type = ms.ReadInt();
            proto.Price = ms.ReadFloat();
        }
        return proto;
    }
}