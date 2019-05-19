using GameServerApp;
using GameServerApp.Common;
using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class GameServerCacheModel
{
    public void Init()
    {
        EventDispatcher.Instance.AddEventListener(ProtoCodeDef.GameServerPageRequest, OnGameServerPageRequest);
        EventDispatcher.Instance.AddEventListener(ProtoCodeDef.GameServerOnePageRequest, OnGameServerOnePageRequest);
    }

    private void OnGameServerPageRequest(Role role, byte[] buffer)
    {
        GameServerPageResponseProto proto = new GameServerPageResponseProto();
        proto.ServerPageItemList = GetServerPageList();
        proto.ItemCount = proto.ServerPageItemList.Count;
        role.ClientSocket.SendMsg(proto.ToArray());

        GameServerOnePageResponseProto onePageProto = new GameServerOnePageResponseProto();
        onePageProto.ServerOnePageItemList = GetGameServerOnePageList(proto.ServerPageItemList.Count);
        onePageProto.ItemCount = onePageProto.ServerOnePageItemList.Count;
        role.ClientSocket.SendMsg(onePageProto.ToArray());
    }
    private void OnGameServerOnePageRequest(Role role, byte[] buffer)
    {
        GameServerOnePageRequestProto requestProto = GameServerOnePageRequestProto.GetProto(buffer);
        GameServerOnePageResponseProto proto = new GameServerOnePageResponseProto();
        proto.ServerOnePageItemList = GetGameServerOnePageList(requestProto.PageIndex);
        proto.ItemCount = proto.ServerOnePageItemList.Count;
        role.ClientSocket.SendMsg(proto.ToArray());
    }

    public List<GameServerPageResponseProto.GameServerPageItem> GetServerPageList()
    {
        List<GameServerPageResponseProto.GameServerPageItem> list = new List<GameServerPageResponseProto.GameServerPageItem>();
        List<GameServerEntity> gameServerList = GetList(isDesc: false,isAutoStatus:false);
        GameServerPageResponseProto.GameServerPageItem entity =new GameServerPageResponseProto.GameServerPageItem();
        int pageIndex = 1;
        for (int i = 0; i < gameServerList.Count; i++)
        {
            //每10个服务器一组
            if (i % 10 == 0)
            {
                //每组的第一个
                entity = new GameServerPageResponseProto.GameServerPageItem();
                entity.PageIndex = pageIndex;
                pageIndex++;
                entity.PageServerName = gameServerList[i].Id.ToString()+"服";
            }
            if (i % 10 == 9 || i == gameServerList.Count - 1)
            {
                string name = entity.PageServerName + " - " + gameServerList[i].Id.ToString()+"服";
                entity.PageServerName = name;
                list.Add(entity);
            }
        }
        list.Reverse();
        return list;
    }

    public List<GameServerOnePageResponseProto.GameServerOnePageItem> GetGameServerOnePageList(int page)
    {
        List<GameServerOnePageResponseProto.GameServerOnePageItem> retList = new List<GameServerOnePageResponseProto.GameServerOnePageItem>();
        //List<GameServerEntity> gameServerOnePageList = new List<GameServerEntity>();

        MFReturnValue <List<GameServerEntity>> retValue = GetPageList(isDesc: false,pageSize:10,pageIndex: page,isAutoStatus:false);
        if (!retValue.HasError)
        {
            List<GameServerEntity> list = retValue.Value;
            for (int i = 0; i < list.Count; i++)
            {
                GameServerOnePageResponseProto.GameServerOnePageItem item = new GameServerOnePageResponseProto.GameServerOnePageItem();
                item.ServerId = (int)list[i].Id;
                item.Status = (int)list[i].Status;
                item.RunState = list[i].RunStatus;
                item.IsCommand = list[i].IsCommand;
                item.IsNew = list[i].IsNew;
                item.Name = list[i].Name;
                item.Ip = list[i].Ip;
                item.Port = list[i].Port;
                retList.Add(item);
            }
        }
        
        return retList;
    }
}
