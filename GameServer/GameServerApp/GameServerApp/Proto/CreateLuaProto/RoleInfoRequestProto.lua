--角色信息(请求)
RoleInfoRequestProto = { ProtoCode = 10203, RoleId = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
RoleInfoRequestProto.__index = RoleInfoRequestProto;

function RoleInfoRequestProto.New()
    local self = { }; --初始化self
    setmetatable(self, RoleInfoRequestProto); --将self的元表设定为Class
    return self;
end


--发送协议
function RoleInfoRequestProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteInt(proto.RoleId);

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function RoleInfoRequestProto.GetProto(buffer)

    local proto = RoleInfoRequestProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.RoleId = ms:ReadInt();

    ms:Dispose();
    return proto;
end