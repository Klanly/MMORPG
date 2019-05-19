--客户端发送进去游戏关卡消息
GameLevelEnterRequestProto = { ProtoCode = 10401, GameChapterId = 0, Grade = 0, GameLevelId = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameLevelEnterRequestProto.__index = GameLevelEnterRequestProto;

function GameLevelEnterRequestProto.New()
    local self = { }; --初始化self
    setmetatable(self, GameLevelEnterRequestProto); --将self的元表设定为Class
    return self;
end


--发送协议
function GameLevelEnterRequestProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteInt(proto.GameChapterId);
    ms:WriteByte(proto.Grade);
    ms:WriteInt(proto.GameLevelId);

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function GameLevelEnterRequestProto.GetProto(buffer)

    local proto = GameLevelEnterRequestProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.GameChapterId = ms:ReadInt();
    proto.Grade = ms:ReadByte();
    proto.GameLevelId = ms:ReadInt();

    ms:Dispose();
    return proto;
end