--邮件(返回)
MailResponseProto = { ProtoCode = 50002, MailCount = 0, MailTable = { } }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
MailResponseProto.__index = MailResponseProto;

function MailResponseProto.New()
    local self = { }; --初始化self
    setmetatable(self, MailResponseProto); --将self的元表设定为Class
    return self;
end


--定义邮件项
Mail = { Id = 0, Content = "", IsRead = false }
Mail.__index = Mail;
function Mail.New()
    local self = { };
    setmetatable(self, Mail);
    return self;
end


--发送协议
function MailResponseProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteInt(proto.MailCount);
    for i = 1, proto.MailCount, 1 do
        ms:WriteInt(MailList[i].Id);
        ms:WriteUTF8String(MailList[i].Content);
        ms:WriteBool(MailList[i].IsRead);
    end

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function MailResponseProto.GetProto(buffer)

    local proto = MailResponseProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.MailCount = ms:ReadInt();
	proto.MailTable = {};
    for i = 1, proto.MailCount, 1 do
        local _Mail = Mail.New();
        _Mail.Id = ms:ReadInt();
        _Mail.Content = ms:ReadUTF8String();
        _Mail.IsRead = ms:ReadBool();
        proto.MailTable[#proto.MailTable+1] = _Mail;
    end

    ms:Dispose();
    return proto;
end