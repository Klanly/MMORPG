--请求服务器组列表(返回)
GameServerPageResponseProto = { ProtoCode = 50111, ItemCount = 0, ServerPageItemTable = { } }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameServerPageResponseProto.__index = GameServerPageResponseProto;

function GameServerPageResponseProto.New()
    local self = { }; --初始化self
    setmetatable(self, GameServerPageResponseProto); --将self的元表设定为Class
    return self;
end


--定义单组服务器信息
ServerPageItem = { PageIndex = 0, PageServerName = "" }
ServerPageItem.__index = ServerPageItem;
function ServerPageItem.New()
    local self = { };
    setmetatable(self, ServerPageItem);
    return self;
end


--发送协议
function GameServerPageResponseProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteInt(proto.ItemCount);
    for i = 1, proto.ItemCount, 1 do
        ms:WriteInt(ServerPageItemList[i].PageIndex);
        ms:WriteUTF8String(ServerPageItemList[i].PageServerName);
    end

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function GameServerPageResponseProto.GetProto(buffer)

    local proto = GameServerPageResponseProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.ItemCount = ms:ReadInt();
	proto.ServerPageItemTable = {};
    for i = 1, proto.ItemCount, 1 do
        local _ServerPageItem = ServerPageItem.New();
        _ServerPageItem.PageIndex = ms:ReadInt();
        _ServerPageItem.PageServerName = ms:ReadUTF8String();
        proto.ServerPageItemTable[#proto.ServerPageItemTable+1] = _ServerPageItem;
    end

    ms:Dispose();
    return proto;
end