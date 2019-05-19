MonsterEntity = { Id = 0, Name = "", Level = 0, HP = 0, Attack = 0, Defense = 0, ResDefense = 0, Hit = 0, Dodge = 0, Cri = 0, Rotate = 0, DescId = 0, IconFloader = "", IconName = "", HalfIcon = "", FloaderName = "", PrefabName = "", WeaponFloader = "", WeaponPath = 0, WeaponParent = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
MonsterEntity.__index = MonsterEntity;

function MonsterEntity.New(Id, Name, Level, HP, Attack, Defense, ResDefense, Hit, Dodge, Cri, Rotate, DescId, IconFloader, IconName, HalfIcon, FloaderName, PrefabName, WeaponFloader, WeaponPath, WeaponParent)
    local self = { }; --初始化self
    setmetatable(self, MonsterEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.Level = Level;
    self.HP = HP;
    self.Attack = Attack;
    self.Defense = Defense;
    self.ResDefense = ResDefense;
    self.Hit = Hit;
    self.Dodge = Dodge;
    self.Cri = Cri;
    self.Rotate = Rotate;
    self.DescId = DescId;
    self.IconFloader = IconFloader;
    self.IconName = IconName;
    self.HalfIcon = HalfIcon;
    self.FloaderName = FloaderName;
    self.PrefabName = PrefabName;
    self.WeaponFloader = WeaponFloader;
    self.WeaponPath = WeaponPath;
    self.WeaponParent = WeaponParent;

    return self;
end