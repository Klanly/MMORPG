--创建角色(返回)
RoleCreateResponseProto = { ProtoCode = 50202, IsSuccess = false, MsgCode = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
RoleCreateResponseProto.__index = RoleCreateResponseProto;

function RoleCreateResponseProto.New()
    local self = { }; --初始化self
    setmetatable(self, RoleCreateResponseProto); --将self的元表设定为Class
    return self;
end


--发送协议
function RoleCreateResponseProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteBool(proto.IsSuccess);
    if(not proto.IsSuccess) then
        ms:WriteInt(MsgCode);
    end

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function RoleCreateResponseProto.GetProto(buffer)

    local proto = RoleCreateResponseProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.IsSuccess = ms:ReadBool();
    if(not proto.IsSuccess) then
        proto.MsgCode = ms:ReadInt();
    end

    ms:Dispose();
    return proto;
end