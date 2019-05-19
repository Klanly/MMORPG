//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 账号登录(返回) </summary>
public struct AccountLogOnResponseProto : IProto
{
    public ushort ProtoCode { get { return 50102; } }

    /// <summary> 是否成功 </summary>
    public bool IsSuccess;
    /// <summary> 错误码 </summary>
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

    public static AccountLogOnResponseProto GetProto(byte[] buffer)
    {
        AccountLogOnResponseProto proto = new AccountLogOnResponseProto();
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