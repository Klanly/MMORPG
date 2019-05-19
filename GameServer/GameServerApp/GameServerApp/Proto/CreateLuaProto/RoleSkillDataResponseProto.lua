--角色技能主动通知协议
RoleSkillDataResponseProto = { ProtoCode = 31001, SkillCount = 0, CurrSkillDataTable = { } }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
RoleSkillDataResponseProto.__index = RoleSkillDataResponseProto;

function RoleSkillDataResponseProto.New()
    local self = { }; --初始化self
    setmetatable(self, RoleSkillDataResponseProto); --将self的元表设定为Class
    return self;
end


--定义当前学会的技能
CurrSkillData = { SkillId = 0, SkillLevel = 0, SlotsNode = 0 }
CurrSkillData.__index = CurrSkillData;
function CurrSkillData.New()
    local self = { };
    setmetatable(self, CurrSkillData);
    return self;
end


--发送协议
function RoleSkillDataResponseProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteByte(proto.SkillCount);
    for i = 1, proto.SkillCount, 1 do
        ms:WriteInt(CurrSkillDataList[i].SkillId);
        ms:WriteInt(CurrSkillDataList[i].SkillLevel);
        ms:WriteByte(CurrSkillDataList[i].SlotsNode);
    end

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function RoleSkillDataResponseProto.GetProto(buffer)

    local proto = RoleSkillDataResponseProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.SkillCount = ms:ReadByte();
	proto.CurrSkillDataTable = {};
    for i = 1, proto.SkillCount, 1 do
        local _CurrSkillData = CurrSkillData.New();
        _CurrSkillData.SkillId = ms:ReadInt();
        _CurrSkillData.SkillLevel = ms:ReadInt();
        _CurrSkillData.SlotsNode = ms:ReadByte();
        proto.CurrSkillDataTable[#proto.CurrSkillDataTable+1] = _CurrSkillData;
    end

    ms:Dispose();
    return proto;
end