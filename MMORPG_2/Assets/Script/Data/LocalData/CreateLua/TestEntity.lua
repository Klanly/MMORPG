TestEntity = { Id = 0, Name = "", Gender = 0, Rotate = 0, NameList = 0, NumList = 0, DoubleNameList = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
TestEntity.__index = TestEntity;

function TestEntity.New(Id, Name, Gender, Rotate, NameList, NumList, DoubleNameList)
    local self = { }; --初始化self
    setmetatable(self, TestEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.Gender = Gender;
    self.Rotate = Rotate;
    self.NameList = NameList;
    self.NumList = NumList;
    self.DoubleNameList = DoubleNameList;

    return self;
end