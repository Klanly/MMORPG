//===================================================
//作    者：xxx
//创建时间：2019-05-18 15:13:23
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary> 请求服务器组列表(返回) </summary>
public struct GameServerPageResponseProto : IProto
{
    public ushort ProtoCode { get { return 50111; } }

    /// <summary> 服务器组数量 </summary>
    public int ItemCount;
    /// <summary> 单组服务器信息 </summary>
    public List<GameServerPageItem> ServerPageItemList;

    /// <summary> 单组服务器信息 </summary>
    public struct GameServerPageItem
    {
        /// <summary> 单组下标 </summary>
        public int PageIndex;
        /// <summary> 单组名称 </summary>
        public string PageServerName;
    }

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(ItemCount);
            for (int i = 0; i < ItemCount; i++)
            {
                ms.WriteInt(ServerPageItemList[i].PageIndex);
                ms.WriteUTF8String(ServerPageItemList[i].PageServerName);
            }
            return ms.ToArray();
        }
    }

    public static GameServerPageResponseProto GetProto(byte[] buffer)
    {
        GameServerPageResponseProto proto = new GameServerPageResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.ItemCount = ms.ReadInt();
            proto.ServerPageItemList = new List<GameServerPageItem>();
            for (int i = 0; i < proto.ItemCount; i++)
            {
                GameServerPageItem _ServerPageItem = new GameServerPageItem();
                _ServerPageItem.PageIndex = ms.ReadInt();
                _ServerPageItem.PageServerName = ms.ReadUTF8String();
                proto.ServerPageItemList.Add(_ServerPageItem);
            }
        }
        return proto;
    }
}