WorldMapEntity = { Id = 0, Name = "", SceneName = "", RoleBirthPos = 0, NPCExcel = "", NPCFloader = "", CameraRotation = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
WorldMapEntity.__index = WorldMapEntity;

function WorldMapEntity.New(Id, Name, SceneName, RoleBirthPos, NPCExcel, NPCFloader, CameraRotation)
    local self = { }; --初始化self
    setmetatable(self, WorldMapEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.SceneName = SceneName;
    self.RoleBirthPos = RoleBirthPos;
    self.NPCExcel = NPCExcel;
    self.NPCFloader = NPCFloader;
    self.CameraRotation = CameraRotation;

    return self;
end