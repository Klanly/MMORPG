--账号登录(请求)
AccountLogOnRequestProto = { ProtoCode = 10102, UserName = "", Pwd = "", DeviceIdentifier = "", DeviceModel = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
AccountLogOnRequestProto.__index = AccountLogOnRequestProto;

function AccountLogOnRequestProto.New()
    local self = { }; --初始化self
    setmetatable(self, AccountLogOnRequestProto); --将self的元表设定为Class
    return self;
end


--发送协议
function AccountLogOnRequestProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteUTF8String(proto.UserName);
    ms:WriteUTF8String(proto.Pwd);
    ms:WriteUTF8String(proto.DeviceIdentifier);
    ms:WriteUTF8String(proto.DeviceModel);

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function AccountLogOnRequestProto.GetProto(buffer)

    local proto = AccountLogOnRequestProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.UserName = ms:ReadUTF8String();
    proto.Pwd = ms:ReadUTF8String();
    proto.DeviceIdentifier = ms:ReadUTF8String();
    proto.DeviceModel = ms:ReadUTF8String();

    ms:Dispose();
    return proto;
end