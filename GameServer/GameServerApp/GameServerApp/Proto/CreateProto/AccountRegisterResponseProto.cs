//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 账号注册(返回) </summary>
public struct AccountRegisterResponseProto : IProto
{
    public ushort ProtoCode { get { return 50101; } }

    /// <summary> 是否注册成功 </summary>
    public bool IsSuccess;
    /// <summary> 消息码 </summary>
    public int MsgCode;
    /// <summary> 用户编号 </summary>
    public int UserId;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(IsSuccess)
            {
                ms.WriteInt(UserId);
            }
            else
            {
                ms.WriteInt(MsgCode);
            }
            return ms.ToArray();
        }
    }

    public static AccountRegisterResponseProto GetProto(byte[] buffer)
    {
        AccountRegisterResponseProto proto = new AccountRegisterResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(proto.IsSuccess)
            {
                proto.UserId = ms.ReadInt();
            }
            else
            {
                proto.MsgCode = ms.ReadInt();
            }
        }
        return proto;
    }
}