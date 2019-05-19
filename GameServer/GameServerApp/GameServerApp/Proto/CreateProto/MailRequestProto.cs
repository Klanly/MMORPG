//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 邮件(请求) </summary>
public struct MailRequestProto : IProto
{
    public ushort ProtoCode { get { return 10002; } }


    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            return ms.ToArray();
        }
    }

    public static MailRequestProto GetProto(byte[] buffer)
    {
        MailRequestProto proto = new MailRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
        }
        return proto;
    }
}