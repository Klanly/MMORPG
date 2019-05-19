--请求服务器单组列表(返回)
GameServerOnePageResponseProto = { ProtoCode = 50112, ItemCount = 0, ServerOnePageItemTable = { } }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameServerOnePageResponseProto.__index = GameServerOnePageResponseProto;

function GameServerOnePageResponseProto.New()
    local self = { }; --初始化self
    setmetatable(self, GameServerOnePageResponseProto); --将self的元表设定为Class
    return self;
end


--定义单个服务器信息
ServerOnePageItem = { ServerId = 0, Status = 0, RunState = 0, IsCommand = false, IsNew = false, Name = "", Ip = "", Port = 0 }
ServerOnePageItem.__index = ServerOnePageItem;
function ServerOnePageItem.New()
    local self = { };
    setmetatable(self, ServerOnePageItem);
    return self;
end


--发送协议
function GameServerOnePageResponseProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteInt(proto.ItemCount);
    for i = 1, proto.ItemCount, 1 do
        ms:WriteInt(ServerOnePageItemList[i].ServerId);
        ms:WriteInt(ServerOnePageItemList[i].Status);
        ms:WriteInt(ServerOnePageItemList[i].RunState);
        ms:WriteBool(ServerOnePageItemList[i].IsCommand);
        ms:WriteBool(ServerOnePageItemList[i].IsNew);
        ms:WriteUTF8String(ServerOnePageItemList[i].Name);
        ms:WriteUTF8String(ServerOnePageItemList[i].Ip);
        ms:WriteInt(ServerOnePageItemList[i].Port);
    end

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function GameServerOnePageResponseProto.GetProto(buffer)

    local proto = GameServerOnePageResponseProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.ItemCount = ms:ReadInt();
	proto.ServerOnePageItemTable = {};
    for i = 1, proto.ItemCount, 1 do
        local _ServerOnePageItem = ServerOnePageItem.New();
        _ServerOnePageItem.ServerId = ms:ReadInt();
        _ServerOnePageItem.Status = ms:ReadInt();
        _ServerOnePageItem.RunState = ms:ReadInt();
        _ServerOnePageItem.IsCommand = ms:ReadBool();
        _ServerOnePageItem.IsNew = ms:ReadBool();
        _ServerOnePageItem.Name = ms:ReadUTF8String();
        _ServerOnePageItem.Ip = ms:ReadUTF8String();
        _ServerOnePageItem.Port = ms:ReadInt();
        proto.ServerOnePageItemTable[#proto.ServerOnePageItemTable+1] = _ServerOnePageItem;
    end

    ms:Dispose();
    return proto;
end