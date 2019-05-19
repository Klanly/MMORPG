//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 请求服务器单组列表(返回) </summary>
public struct GameServerOnePageResponseProto : IProto
{
    public ushort ProtoCode { get { return 50112; } }

    /// <summary> 单组服务器数量 </summary>
    public int ItemCount;
    /// <summary> 单个服务器信息 </summary>
    public List<GameServerOnePageItem> ServerOnePageItemList;

    /// <summary> 单个服务器信息 </summary>
    public struct GameServerOnePageItem
    {
        /// <summary> 服务器Id </summary>
        public int ServerId;
        /// <summary> 服务器状态 </summary>
        public int Status;
        /// <summary> 运行状态 </summary>
        public int RunState;
        /// <summary> 是否推荐 </summary>
        public bool IsCommand;
        /// <summary> 是否是新服 </summary>
        public bool IsNew;
        /// <summary> 服务器名称 </summary>
        public string Name;
        /// <summary> 服务器IP </summary>
        public string Ip;
        /// <summary> 服务器端口号 </summary>
        public int Port;
    }

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(ItemCount);
            for (int i = 0; i < ItemCount; i++)
            {
                ms.WriteInt(ServerOnePageItemList[i].ServerId);
                ms.WriteInt(ServerOnePageItemList[i].Status);
                ms.WriteInt(ServerOnePageItemList[i].RunState);
                ms.WriteBool(ServerOnePageItemList[i].IsCommand);
                ms.WriteBool(ServerOnePageItemList[i].IsNew);
                ms.WriteUTF8String(ServerOnePageItemList[i].Name);
                ms.WriteUTF8String(ServerOnePageItemList[i].Ip);
                ms.WriteInt(ServerOnePageItemList[i].Port);
            }
            return ms.ToArray();
        }
    }

    public static GameServerOnePageResponseProto GetProto(byte[] buffer)
    {
        GameServerOnePageResponseProto proto = new GameServerOnePageResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.ItemCount = ms.ReadInt();
            proto.ServerOnePageItemList = new List<GameServerOnePageItem>();
            for (int i = 0; i < proto.ItemCount; i++)
            {
                GameServerOnePageItem _ServerOnePageItem = new GameServerOnePageItem();
                _ServerOnePageItem.ServerId = ms.ReadInt();
                _ServerOnePageItem.Status = ms.ReadInt();
                _ServerOnePageItem.RunState = ms.ReadInt();
                _ServerOnePageItem.IsCommand = ms.ReadBool();
                _ServerOnePageItem.IsNew = ms.ReadBool();
                _ServerOnePageItem.Name = ms.ReadUTF8String();
                _ServerOnePageItem.Ip = ms.ReadUTF8String();
                _ServerOnePageItem.Port = ms.ReadInt();
                proto.ServerOnePageItemList.Add(_ServerOnePageItem);
            }
        }
        return proto;
    }
}