//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:21
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 角色技能主动通知协议 </summary>
public struct RoleSkillDataResponseProto : IProto
{
    public ushort ProtoCode { get { return 31001; } }

    /// <summary> 学会的技能数量 </summary>
    public byte SkillCount;
    /// <summary> 当前学会的技能 </summary>
    public List<SkillData> CurrSkillDataList;

    /// <summary> 当前学会的技能 </summary>
    public struct SkillData
    {
        /// <summary> 技能编号 </summary>
        public int SkillId;
        /// <summary> 技能等级 </summary>
        public int SkillLevel;
        /// <summary> 技能槽编号 </summary>
        public byte SlotsNode;
    }

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteByte(SkillCount);
            for (int i = 0; i < SkillCount; i++)
            {
                ms.WriteInt(CurrSkillDataList[i].SkillId);
                ms.WriteInt(CurrSkillDataList[i].SkillLevel);
                ms.WriteByte(CurrSkillDataList[i].SlotsNode);
            }
            return ms.ToArray();
        }
    }

    public static RoleSkillDataResponseProto GetProto(byte[] buffer)
    {
        RoleSkillDataResponseProto proto = new RoleSkillDataResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.SkillCount = (byte)ms.ReadByte();
            proto.CurrSkillDataList = new List<SkillData>();
            for (int i = 0; i < proto.SkillCount; i++)
            {
                SkillData _CurrSkillData = new SkillData();
                _CurrSkillData.SkillId = ms.ReadInt();
                _CurrSkillData.SkillLevel = ms.ReadInt();
                _CurrSkillData.SlotsNode = (byte)ms.ReadByte();
                proto.CurrSkillDataList.Add(_CurrSkillData);
            }
        }
        return proto;
    }
}