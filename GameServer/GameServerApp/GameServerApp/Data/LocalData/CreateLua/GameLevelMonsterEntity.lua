GameLevelMonsterEntity = { Id = 0, Chapter = 0, GameLevel = 0, Grade = 0, RegionId = 0, MonsterId = 0, MonsterCount = 0, DropExp = 0, DropGold = 0, DropEquip = 0, DropMaterial = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameLevelMonsterEntity.__index = GameLevelMonsterEntity;

function GameLevelMonsterEntity.New(Id, Chapter, GameLevel, Grade, RegionId, MonsterId, MonsterCount, DropExp, DropGold, DropEquip, DropMaterial)
    local self = { }; --初始化self
    setmetatable(self, GameLevelMonsterEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Chapter = Chapter;
    self.GameLevel = GameLevel;
    self.Grade = Grade;
    self.RegionId = RegionId;
    self.MonsterId = MonsterId;
    self.MonsterCount = MonsterCount;
    self.DropExp = DropExp;
    self.DropGold = DropGold;
    self.DropEquip = DropEquip;
    self.DropMaterial = DropMaterial;

    return self;
end