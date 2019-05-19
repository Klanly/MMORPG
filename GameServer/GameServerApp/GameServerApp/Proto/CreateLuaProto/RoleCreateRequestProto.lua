--创建角色(请求)
RoleCreateRequestProto = { ProtoCode = 10202, JobId = 0, RoleName = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
RoleCreateRequestProto.__index = RoleCreateRequestProto;

function RoleCreateRequestProto.New()
    local self = { }; --初始化self
    setmetatable(self, RoleCreateRequestProto); --将self的元表设定为Class
    return self;
end


--发送协议
function RoleCreateRequestProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteByte(proto.JobId);
    ms:WriteUTF8String(proto.RoleName);

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function RoleCreateRequestProto.GetProto(buffer)

    local proto = RoleCreateRequestProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.JobId = ms:ReadByte();
    proto.RoleName = ms:ReadUTF8String();

    ms:Dispose();
    return proto;
end