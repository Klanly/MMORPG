ChapterLevelEntity = { Id = 0, Chapter = 0, Name = "", IsBoos = 0, FolderName = "", IconName = "", RecommendFight = 0, MaxCount = 0, NeedVitality = 0, FirstThreeStar = 0, Desc = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
ChapterLevelEntity.__index = ChapterLevelEntity;

function ChapterLevelEntity.New(Id, Chapter, Name, IsBoos, FolderName, IconName, RecommendFight, MaxCount, NeedVitality, FirstThreeStar, Desc)
    local self = { }; --初始化self
    setmetatable(self, ChapterLevelEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Chapter = Chapter;
    self.Name = Name;
    self.IsBoos = IsBoos;
    self.FolderName = FolderName;
    self.IconName = IconName;
    self.RecommendFight = RecommendFight;
    self.MaxCount = MaxCount;
    self.NeedVitality = NeedVitality;
    self.FirstThreeStar = FirstThreeStar;
    self.Desc = Desc;

    return self;
end