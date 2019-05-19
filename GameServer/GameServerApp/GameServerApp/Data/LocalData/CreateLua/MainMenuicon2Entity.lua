MainMenuIcon2Entity = { Id = 0, Name = "", Weight = 0, AtlasName = "", IconName = "", ShowLevel = 0, LimitLevel = 0, OpenWindowName = "", Page = 0,  }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
MainMenuIcon2Entity.__index = MainMenuIcon2Entity;

function MainMenuIcon2Entity.New(Id, Name, Weight, AtlasName, IconName, ShowLevel, LimitLevel, OpenWindowName, Page, )
    local self = { }; --初始化self
    setmetatable(self, MainMenuIcon2Entity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.Weight = Weight;
    self.AtlasName = AtlasName;
    self.IconName = IconName;
    self.ShowLevel = ShowLevel;
    self.LimitLevel = LimitLevel;
    self.OpenWindowName = OpenWindowName;
    self.Page = Page;
    self. = ;

    return self;
end