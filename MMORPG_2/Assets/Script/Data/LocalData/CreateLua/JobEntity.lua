JobEntity = { Id = 0, Name = "", Gender = 0, Rotate = 0, DescId = 0, AttackCoefficient = 0, DefenseCoefficient = 0, HitCoefficient = 0, DodgeCoefficient = 0, CriCoefficient = 0, ResCoefficient = 0, PhyAttackIdArray = 0, SkillAttackIdArray = 0, HeadPic = "", JobPic = "", FloaderName = "", PrefabName = "", WeaponFloader = "", WeaponPath = 0, WeaponParent = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
JobEntity.__index = JobEntity;

function JobEntity.New(Id, Name, Gender, Rotate, DescId, AttackCoefficient, DefenseCoefficient, HitCoefficient, DodgeCoefficient, CriCoefficient, ResCoefficient, PhyAttackIdArray, SkillAttackIdArray, HeadPic, JobPic, FloaderName, PrefabName, WeaponFloader, WeaponPath, WeaponParent)
    local self = { }; --初始化self
    setmetatable(self, JobEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Name = Name;
    self.Gender = Gender;
    self.Rotate = Rotate;
    self.DescId = DescId;
    self.AttackCoefficient = AttackCoefficient;
    self.DefenseCoefficient = DefenseCoefficient;
    self.HitCoefficient = HitCoefficient;
    self.DodgeCoefficient = DodgeCoefficient;
    self.CriCoefficient = CriCoefficient;
    self.ResCoefficient = ResCoefficient;
    self.PhyAttackIdArray = PhyAttackIdArray;
    self.SkillAttackIdArray = SkillAttackIdArray;
    self.HeadPic = HeadPic;
    self.JobPic = JobPic;
    self.FloaderName = FloaderName;
    self.PrefabName = PrefabName;
    self.WeaponFloader = WeaponFloader;
    self.WeaponPath = WeaponPath;
    self.WeaponParent = WeaponParent;

    return self;
end