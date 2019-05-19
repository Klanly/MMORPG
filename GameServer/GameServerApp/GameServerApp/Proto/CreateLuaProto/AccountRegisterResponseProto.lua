--账号注册(返回)
AccountRegisterResponseProto = { ProtoCode = 50101, IsSuccess = false, MsgCode = 0, UserId = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
AccountRegisterResponseProto.__index = AccountRegisterResponseProto;

function AccountRegisterResponseProto.New()
    local self = { }; --初始化self
    setmetatable(self, AccountRegisterResponseProto); --将self的元表设定为Class
    return self;
end


--发送协议
function AccountRegisterResponseProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteBool(proto.IsSuccess);
    if(proto.IsSuccess) then
        ms:WriteInt(UserId);
        else
        ms:WriteInt(MsgCode);
    end

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function AccountRegisterResponseProto.GetProto(buffer)

    local proto = AccountRegisterResponseProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.IsSuccess = ms:ReadBool();
    if(proto.IsSuccess) then
        proto.UserId = ms:ReadInt();
        else
        proto.MsgCode = ms:ReadInt();
    end

    ms:Dispose();
    return proto;
end