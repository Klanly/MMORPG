--登录区服(返回)
RoleLogOnGameServerResponseProto = { ProtoCode = 50201, RoleCount = 0, RoleTable = { } }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
RoleLogOnGameServerResponseProto.__index = RoleLogOnGameServerResponseProto;

function RoleLogOnGameServerResponseProto.New()
    local self = { }; --初始化self
    setmetatable(self, RoleLogOnGameServerResponseProto); --将self的元表设定为Class
    return self;
end


--定义角色项
Role = { RoleId = 0, RoleNickName = "", RoleJob = 0, RoleLevel = 0 }
Role.__index = Role;
function Role.New()
    local self = { };
    setmetatable(self, Role);
    return self;
end


--发送协议
function RoleLogOnGameServerResponseProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteInt(proto.RoleCount);
    for i = 1, proto.RoleCount, 1 do
        ms:WriteInt(RoleList[i].RoleId);
        ms:WriteUTF8String(RoleList[i].RoleNickName);
        ms:WriteByte(RoleList[i].RoleJob);
        ms:WriteInt(RoleList[i].RoleLevel);
    end

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function RoleLogOnGameServerResponseProto.GetProto(buffer)

    local proto = RoleLogOnGameServerResponseProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.RoleCount = ms:ReadInt();
	proto.RoleTable = {};
    for i = 1, proto.RoleCount, 1 do
        local _Role = Role.New();
        _Role.RoleId = ms:ReadInt();
        _Role.RoleNickName = ms:ReadUTF8String();
        _Role.RoleJob = ms:ReadByte();
        _Role.RoleLevel = ms:ReadInt();
        proto.RoleTable[#proto.RoleTable+1] = _Role;
    end

    ms:Dispose();
    return proto;
end