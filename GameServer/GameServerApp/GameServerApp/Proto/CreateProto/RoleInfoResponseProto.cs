//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 角色信息(返回) </summary>
public struct RoleInfoResponseProto : IProto
{
    public ushort ProtoCode { get { return 50203; } }

    /// <summary> 是否成功 </summary>
    public bool IsSuccess;
    /// <summary> 消息码 </summary>
    public int MsgCode;
    /// <summary> 角色编号 </summary>
    public int RoleId;
    /// <summary> 职业编号 </summary>
    public int JobId;
    /// <summary> 角色昵称 </summary>
    public string NickName;
    /// <summary> 角色性别 </summary>
    public int Sex;
    /// <summary> 等级 </summary>
    public int Level;
    /// <summary> 元宝 </summary>
    public int Money;
    /// <summary> 金币 </summary>
    public int Gold;
    /// <summary> 经验 </summary>
    public int Exp;
    /// <summary> 最大血量 </summary>
    public int MaxHP;
    /// <summary> 当前血量 </summary>
    public int CurrHP;
    /// <summary> 最大法力 </summary>
    public int MaxMP;
    /// <summary> 当前法力 </summary>
    public int CurrMP;
    /// <summary> 攻击 </summary>
    public int Attack;
    /// <summary> 攻击加成 </summary>
    public int AttackAddition;
    /// <summary> 物理防御 </summary>
    public int Defense;
    /// <summary> 物理防御加成 </summary>
    public int DefenseAddition;
    /// <summary> 魔法防御 </summary>
    public int Res;
    /// <summary> 魔法防御加成 </summary>
    public int ResAddition;
    /// <summary> 命中 </summary>
    public int Hit;
    /// <summary> 命中加成 </summary>
    public int HitAddition;
    /// <summary> 闪避 </summary>
    public int Dodge;
    /// <summary> 闪避加成 </summary>
    public int DodgeAddition;
    /// <summary> 必杀 </summary>
    public int Cri;
    /// <summary> 必杀加成 </summary>
    public int CriAddition;
    /// <summary> 战斗力 </summary>
    public int Fighting;
    /// <summary> 战斗力加成 </summary>
    public int FightingAddition;
    /// <summary> 最后进入的世界地图编号 </summary>
    public int LastInWorldMapId;

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(IsSuccess)
            {
                ms.WriteInt(RoleId);
                ms.WriteInt(JobId);
                ms.WriteUTF8String(NickName);
                ms.WriteInt(Sex);
                ms.WriteInt(Level);
                ms.WriteInt(Money);
                ms.WriteInt(Gold);
                ms.WriteInt(Exp);
                ms.WriteInt(MaxHP);
                ms.WriteInt(CurrHP);
                ms.WriteInt(MaxMP);
                ms.WriteInt(CurrMP);
                ms.WriteInt(Attack);
                ms.WriteInt(AttackAddition);
                ms.WriteInt(Defense);
                ms.WriteInt(DefenseAddition);
                ms.WriteInt(Res);
                ms.WriteInt(ResAddition);
                ms.WriteInt(Hit);
                ms.WriteInt(HitAddition);
                ms.WriteInt(Dodge);
                ms.WriteInt(DodgeAddition);
                ms.WriteInt(Cri);
                ms.WriteInt(CriAddition);
                ms.WriteInt(Fighting);
                ms.WriteInt(FightingAddition);
                ms.WriteInt(LastInWorldMapId);
            }
            else
            {
                ms.WriteInt(MsgCode);
            }
            return ms.ToArray();
        }
    }

    public static RoleInfoResponseProto GetProto(byte[] buffer)
    {
        RoleInfoResponseProto proto = new RoleInfoResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(proto.IsSuccess)
            {
                proto.RoleId = ms.ReadInt();
                proto.JobId = ms.ReadInt();
                proto.NickName = ms.ReadUTF8String();
                proto.Sex = ms.ReadInt();
                proto.Level = ms.ReadInt();
                proto.Money = ms.ReadInt();
                proto.Gold = ms.ReadInt();
                proto.Exp = ms.ReadInt();
                proto.MaxHP = ms.ReadInt();
                proto.CurrHP = ms.ReadInt();
                proto.MaxMP = ms.ReadInt();
                proto.CurrMP = ms.ReadInt();
                proto.Attack = ms.ReadInt();
                proto.AttackAddition = ms.ReadInt();
                proto.Defense = ms.ReadInt();
                proto.DefenseAddition = ms.ReadInt();
                proto.Res = ms.ReadInt();
                proto.ResAddition = ms.ReadInt();
                proto.Hit = ms.ReadInt();
                proto.HitAddition = ms.ReadInt();
                proto.Dodge = ms.ReadInt();
                proto.DodgeAddition = ms.ReadInt();
                proto.Cri = ms.ReadInt();
                proto.CriAddition = ms.ReadInt();
                proto.Fighting = ms.ReadInt();
                proto.FightingAddition = ms.ReadInt();
                proto.LastInWorldMapId = ms.ReadInt();
            }
            else
            {
                proto.MsgCode = ms.ReadInt();
            }
        }
        return proto;
    }
}