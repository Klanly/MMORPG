//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 客户端发送战斗胜利协议 </summary>
public struct GameLevelSuccessRequestProto : IProto
{
    public ushort ProtoCode { get { return 10402; } }

    /// <summary> 游戏关卡Id </summary>
    public int GameLevelId;
    /// <summary> 难度等级 </summary>
    public byte Grade;
    /// <summary> 获得星级 </summary>
    public byte Star;
    /// <summary> 获得经验 </summary>
    public int Exp;
    /// <summary> 获得金币 </summary>
    public int Gold;
    /// <summary> 杀怪数量 </summary>
    public int KillTotalMonsterCount;
    /// <summary> 杀怪列表 </summary>
    public List<MonsterItem> KillMonsterList;
    /// <summary> 获得物品数量 </summary>
    public int GoodsTotalCount;
    /// <summary> 获得物品 </summary>
    public List<GoodsItem> GetGoodsList;
    /// <summary> 章节Id </summary>
    public int ChapterId;

    /// <summary> 杀怪列表 </summary>
    public struct MonsterItem
    {
        /// <summary> 怪物id </summary>
        public int MonsterId;
        /// <summary> 怪数量 </summary>
        public int MonsterCount;
    }

    /// <summary> 获得物品 </summary>
    public struct GoodsItem
    {
        /// <summary> 物品类型 </summary>
        public byte GoodsType;
        /// <summary> 物品Id </summary>
        public int GoodsId;
        /// <summary> 物品数量 </summary>
        public int GoodsCount;
    }

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(GameLevelId);
            ms.WriteByte(Grade);
            ms.WriteByte(Star);
            ms.WriteInt(Exp);
            ms.WriteInt(Gold);
            ms.WriteInt(KillTotalMonsterCount);
            for (int i = 0; i < KillTotalMonsterCount; i++)
            {
                ms.WriteInt(KillMonsterList[i].MonsterId);
                ms.WriteInt(KillMonsterList[i].MonsterCount);
            }
            ms.WriteInt(GoodsTotalCount);
            for (int i = 0; i < GoodsTotalCount; i++)
            {
                ms.WriteByte(GetGoodsList[i].GoodsType);
                ms.WriteInt(GetGoodsList[i].GoodsId);
                ms.WriteInt(GetGoodsList[i].GoodsCount);
            }
            ms.WriteInt(ChapterId);
            return ms.ToArray();
        }
    }

    public static GameLevelSuccessRequestProto GetProto(byte[] buffer)
    {
        GameLevelSuccessRequestProto proto = new GameLevelSuccessRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.GameLevelId = ms.ReadInt();
            proto.Grade = (byte)ms.ReadByte();
            proto.Star = (byte)ms.ReadByte();
            proto.Exp = ms.ReadInt();
            proto.Gold = ms.ReadInt();
            proto.KillTotalMonsterCount = ms.ReadInt();
            proto.KillMonsterList = new List<MonsterItem>();
            for (int i = 0; i < proto.KillTotalMonsterCount; i++)
            {
                MonsterItem _KillMonster = new MonsterItem();
                _KillMonster.MonsterId = ms.ReadInt();
                _KillMonster.MonsterCount = ms.ReadInt();
                proto.KillMonsterList.Add(_KillMonster);
            }
            proto.GoodsTotalCount = ms.ReadInt();
            proto.GetGoodsList = new List<GoodsItem>();
            for (int i = 0; i < proto.GoodsTotalCount; i++)
            {
                GoodsItem _GetGoods = new GoodsItem();
                _GetGoods.GoodsType = (byte)ms.ReadByte();
                _GetGoods.GoodsId = ms.ReadInt();
                _GetGoods.GoodsCount = ms.ReadInt();
                proto.GetGoodsList.Add(_GetGoods);
            }
            proto.ChapterId = ms.ReadInt();
        }
        return proto;
    }
}