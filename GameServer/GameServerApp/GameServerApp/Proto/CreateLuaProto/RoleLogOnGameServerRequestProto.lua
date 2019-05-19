--登录区服(请求)
RoleLogOnGameServerRequestProto = { ProtoCode = 10201, AccountId = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
RoleLogOnGameServerRequestProto.__index = RoleLogOnGameServerRequestProto;

function RoleLogOnGameServerRequestProto.New()
    local self = { }; --初始化self
    setmetatable(self, RoleLogOnGameServerRequestProto); --将self的元表设定为Class
    return self;
end


--发送协议
function RoleLogOnGameServerRequestProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteInt(proto.AccountId);

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function RoleLogOnGameServerRequestProto.GetProto(buffer)

    local proto = RoleLogOnGameServerRequestProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.AccountId = ms:ReadInt();

    ms:Dispose();
    return proto;
end