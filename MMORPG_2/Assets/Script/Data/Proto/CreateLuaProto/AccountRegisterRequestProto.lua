--账号注册(请求)
AccountRegisterRequestProto = { ProtoCode = 10101, Account = "", Pwd = "", Channel = "", DeviceIdentifier = "", DeviceModel = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
AccountRegisterRequestProto.__index = AccountRegisterRequestProto;

function AccountRegisterRequestProto.New()
    local self = { }; --初始化self
    setmetatable(self, AccountRegisterRequestProto); --将self的元表设定为Class
    return self;
end


--发送协议
function AccountRegisterRequestProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteUTF8String(proto.Account);
    ms:WriteUTF8String(proto.Pwd);
    ms:WriteUTF8String(proto.Channel);
    ms:WriteUTF8String(proto.DeviceIdentifier);
    ms:WriteUTF8String(proto.DeviceModel);

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function AccountRegisterRequestProto.GetProto(buffer)

    local proto = AccountRegisterRequestProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.Account = ms:ReadUTF8String();
    proto.Pwd = ms:ReadUTF8String();
    proto.Channel = ms:ReadUTF8String();
    proto.DeviceIdentifier = ms:ReadUTF8String();
    proto.DeviceModel = ms:ReadUTF8String();

    ms:Dispose();
    return proto;
end