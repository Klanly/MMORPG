//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 邮件(返回) </summary>
public struct MailResponseProto : IProto
{
    public ushort ProtoCode { get { return 50002; } }

    /// <summary> 邮件数量 </summary>
    public int MailCount;
    /// <summary> 邮件项 </summary>
    public List<MailItem> MailList;

    /// <summary> 邮件项 </summary>
    public struct MailItem
    {
        /// <summary> 邮件Id </summary>
        public int Id;
        /// <summary> 邮件内容 </summary>
        public string Content;
        /// <summary> 是否已读 </summary>
        public bool IsRead;
    }

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(MailCount);
            for (int i = 0; i < MailCount; i++)
            {
                ms.WriteInt(MailList[i].Id);
                ms.WriteUTF8String(MailList[i].Content);
                ms.WriteBool(MailList[i].IsRead);
            }
            return ms.ToArray();
        }
    }

    public static MailResponseProto GetProto(byte[] buffer)
    {
        MailResponseProto proto = new MailResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.MailCount = ms.ReadInt();
            proto.MailList = new List<MailItem>();
            for (int i = 0; i < proto.MailCount; i++)
            {
                MailItem _Mail = new MailItem();
                _Mail.Id = ms.ReadInt();
                _Mail.Content = ms.ReadUTF8String();
                _Mail.IsRead = ms.ReadBool();
                proto.MailList.Add(_Mail);
            }
        }
        return proto;
    }
}