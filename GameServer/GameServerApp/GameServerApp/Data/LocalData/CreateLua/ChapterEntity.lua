ChapterEntity = { Id = 0, Name = "", LevelCount = 0, FolderName = "", IconName = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
ChapterEntity.__index = ChapterEntity;

function ChapterEntity.New(Id, Name, LevelCount, FolderName, IconName)
    local self = { }; --初始化self
    setmetatable(self, ChapterEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.LevelCount = LevelCount;
    self.FolderName = FolderName;
    self.IconName = IconName;

    return self;
end