//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:21
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 登录区服(返回) </summary>
public struct RoleLogOnGameServerResponseProto : IProto
{
    public ushort ProtoCode { get { return 50201; } }

    /// <summary> 已有角色数量 </summary>
    public int RoleCount;
    /// <summary> 角色项 </summary>
    public List<RoleItem> RoleList;

    /// <summary> 角色项 </summary>
    public struct RoleItem
    {
        /// <summary> 角色编号 </summary>
        public int RoleId;
        /// <summary> 角色昵称 </summary>
        public string RoleNickName;
        /// <summary> 角色职业 </summary>
        public byte RoleJob;
        /// <summary> 角色等级 </summary>
        public int RoleLevel;
    }

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleCount);
            for (int i = 0; i < RoleCount; i++)
            {
                ms.WriteInt(RoleList[i].RoleId);
                ms.WriteUTF8String(RoleList[i].RoleNickName);
                ms.WriteByte(RoleList[i].RoleJob);
                ms.WriteInt(RoleList[i].RoleLevel);
            }
            return ms.ToArray();
        }
    }

    public static RoleLogOnGameServerResponseProto GetProto(byte[] buffer)
    {
        RoleLogOnGameServerResponseProto proto = new RoleLogOnGameServerResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.RoleCount = ms.ReadInt();
            proto.RoleList = new List<RoleItem>();
            for (int i = 0; i < proto.RoleCount; i++)
            {
                RoleItem _Role = new RoleItem();
                _Role.RoleId = ms.ReadInt();
                _Role.RoleNickName = ms.ReadUTF8String();
                _Role.RoleJob = (byte)ms.ReadByte();
                _Role.RoleLevel = ms.ReadInt();
                proto.RoleList.Add(_Role);
            }
        }
        return proto;
    }
}