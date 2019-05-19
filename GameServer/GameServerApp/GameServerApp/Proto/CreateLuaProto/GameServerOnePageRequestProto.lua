--请求服务器单组列表(请求)
GameServerOnePageRequestProto = { ProtoCode = 10112, PageIndex = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameServerOnePageRequestProto.__index = GameServerOnePageRequestProto;

function GameServerOnePageRequestProto.New()
    local self = { }; --初始化self
    setmetatable(self, GameServerOnePageRequestProto); --将self的元表设定为Class
    return self;
end


--发送协议
function GameServerOnePageRequestProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteInt(proto.PageIndex);

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function GameServerOnePageRequestProto.GetProto(buffer)

    local proto = GameServerOnePageRequestProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.PageIndex = ms:ReadInt();

    ms:Dispose();
    return proto;
end