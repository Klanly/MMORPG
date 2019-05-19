--请求服务器组列表(请求)
GameServerPageRequestProto = { ProtoCode = 10111 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameServerPageRequestProto.__index = GameServerPageRequestProto;

function GameServerPageRequestProto.New()
    local self = { }; --初始化self
    setmetatable(self, GameServerPageRequestProto); --将self的元表设定为Class
    return self;
end


--发送协议
function GameServerPageRequestProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);


    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function GameServerPageRequestProto.GetProto(buffer)

    local proto = GameServerPageRequestProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);


    ms:Dispose();
    return proto;
end