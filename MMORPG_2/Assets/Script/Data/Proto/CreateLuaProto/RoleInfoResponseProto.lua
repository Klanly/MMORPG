--角色信息(返回)
RoleInfoResponseProto = { ProtoCode = 50203, IsSuccess = false, MsgCode = 0, RoleId = 0, JobId = 0, NickName = "", Sex = 0, Level = 0, Money = 0, Gold = 0, Exp = 0, MaxHP = 0, CurrHP = 0, MaxMP = 0, CurrMP = 0, Attack = 0, AttackAddition = 0, Defense = 0, DefenseAddition = 0, Res = 0, ResAddition = 0, Hit = 0, HitAddition = 0, Dodge = 0, DodgeAddition = 0, Cri = 0, CriAddition = 0, Fighting = 0, FightingAddition = 0, LastInWorldMapId = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
RoleInfoResponseProto.__index = RoleInfoResponseProto;

function RoleInfoResponseProto.New()
    local self = { }; --初始化self
    setmetatable(self, RoleInfoResponseProto); --将self的元表设定为Class
    return self;
end


--发送协议
function RoleInfoResponseProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteBool(proto.IsSuccess);
    if(proto.IsSuccess) then
        ms:WriteInt(RoleId);
        ms:WriteInt(JobId);
        ms:WriteUTF8String(NickName);
        ms:WriteInt(Sex);
        ms:WriteInt(Level);
        ms:WriteInt(Money);
        ms:WriteInt(Gold);
        ms:WriteInt(Exp);
        ms:WriteInt(MaxHP);
        ms:WriteInt(CurrHP);
        ms:WriteInt(MaxMP);
        ms:WriteInt(CurrMP);
        ms:WriteInt(Attack);
        ms:WriteInt(AttackAddition);
        ms:WriteInt(Defense);
        ms:WriteInt(DefenseAddition);
        ms:WriteInt(Res);
        ms:WriteInt(ResAddition);
        ms:WriteInt(Hit);
        ms:WriteInt(HitAddition);
        ms:WriteInt(Dodge);
        ms:WriteInt(DodgeAddition);
        ms:WriteInt(Cri);
        ms:WriteInt(CriAddition);
        ms:WriteInt(Fighting);
        ms:WriteInt(FightingAddition);
        ms:WriteInt(LastInWorldMapId);
        else
        ms:WriteInt(MsgCode);
    end

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function RoleInfoResponseProto.GetProto(buffer)

    local proto = RoleInfoResponseProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.IsSuccess = ms:ReadBool();
    if(proto.IsSuccess) then
        proto.RoleId = ms:ReadInt();
        proto.JobId = ms:ReadInt();
        proto.NickName = ms:ReadUTF8String();
        proto.Sex = ms:ReadInt();
        proto.Level = ms:ReadInt();
        proto.Money = ms:ReadInt();
        proto.Gold = ms:ReadInt();
        proto.Exp = ms:ReadInt();
        proto.MaxHP = ms:ReadInt();
        proto.CurrHP = ms:ReadInt();
        proto.MaxMP = ms:ReadInt();
        proto.CurrMP = ms:ReadInt();
        proto.Attack = ms:ReadInt();
        proto.AttackAddition = ms:ReadInt();
        proto.Defense = ms:ReadInt();
        proto.DefenseAddition = ms:ReadInt();
        proto.Res = ms:ReadInt();
        proto.ResAddition = ms:ReadInt();
        proto.Hit = ms:ReadInt();
        proto.HitAddition = ms:ReadInt();
        proto.Dodge = ms:ReadInt();
        proto.DodgeAddition = ms:ReadInt();
        proto.Cri = ms:ReadInt();
        proto.CriAddition = ms:ReadInt();
        proto.Fighting = ms:ReadInt();
        proto.FightingAddition = ms:ReadInt();
        proto.LastInWorldMapId = ms:ReadInt();
        else
        proto.MsgCode = ms:ReadInt();
    end

    ms:Dispose();
    return proto;
end