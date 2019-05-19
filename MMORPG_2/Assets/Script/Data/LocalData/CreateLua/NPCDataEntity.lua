NPCDataEntity = { Id = 0, Name = "", PrefabName = "", RoleBirthPos = 0, HeadPicAtlas = "", HeadPic = "", HalfBodyPath = "", Talk = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
NPCDataEntity.__index = NPCDataEntity;

function NPCDataEntity.New(Id, Name, PrefabName, RoleBirthPos, HeadPicAtlas, HeadPic, HalfBodyPath, Talk)
    local self = { }; --初始化self
    setmetatable(self, NPCDataEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.PrefabName = PrefabName;
    self.RoleBirthPos = RoleBirthPos;
    self.HeadPicAtlas = HeadPicAtlas;
    self.HeadPic = HeadPic;
    self.HalfBodyPath = HalfBodyPath;
    self.Talk = Talk;

    return self;
end