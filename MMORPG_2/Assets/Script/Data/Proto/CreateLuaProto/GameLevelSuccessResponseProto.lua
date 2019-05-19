--服务器返回关卡胜利协议
GameLevelSuccessResponseProto = { ProtoCode = 50402, IsSuccess = false, MsgCode = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameLevelSuccessResponseProto.__index = GameLevelSuccessResponseProto;

function GameLevelSuccessResponseProto.New()
    local self = { }; --初始化self
    setmetatable(self, GameLevelSuccessResponseProto); --将self的元表设定为Class
    return self;
end


--发送协议
function GameLevelSuccessResponseProto.SendProto(proto)

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
function GameLevelSuccessResponseProto.GetProto(buffer)

    local proto = GameLevelSuccessResponseProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.IsSuccess = ms:ReadBool();
    if(not proto.IsSuccess) then
        proto.MsgCode = ms:ReadInt();
    end

    ms:Dispose();
    return proto;
end