SkillEntity = { Id = 0, SkillName = "", SkillDesc = "", EffectName = "", SkillIconFolder = "", SkillIconName = "", HurtValueRate = 0, HurtValueRateUp = 0, LevelLimit = 0, SpendMP = 0, SpendMPLevelUp = 0, IsPhyAttack = 0, IsDoCameraShake = 0, CameraShakeDelay = 0, SkillCD = 0, SkillUpCDTime = 0, EffectLifeTime = 0, AttackTargetCount = 0, IsTargetMonster = 0, AttackRange = 0, AreaAttackRadius = 0, ShowEffectDelay = 0, ShowHurtEffectDelaySecond = 0, RedScreen = 0, AttackState = 0, AbnormalState = 0, BuffInfoID = 0, BuffTargetFilter = 0, BuffIsPercentage = 0 }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
SkillEntity.__index = SkillEntity;

function SkillEntity.New(Id, SkillName, SkillDesc, EffectName, SkillIconFolder, SkillIconName, HurtValueRate, HurtValueRateUp, LevelLimit, SpendMP, SpendMPLevelUp, IsPhyAttack, IsDoCameraShake, CameraShakeDelay, SkillCD, SkillUpCDTime, EffectLifeTime, AttackTargetCount, IsTargetMonster, AttackRange, AreaAttackRadius, ShowEffectDelay, ShowHurtEffectDelaySecond, RedScreen, AttackState, AbnormalState, BuffInfoID, BuffTargetFilter, BuffIsPercentage)
    local self = { }; --初始化self
    setmetatable(self, SkillEntity); --将self的元表设定为Class

    self.Id = Id;
    self.SkillName = SkillName;
    self.SkillDesc = SkillDesc;
    self.EffectName = EffectName;
    self.SkillIconFolder = SkillIconFolder;
    self.SkillIconName = SkillIconName;
    self.HurtValueRate = HurtValueRate;
    self.HurtValueRateUp = HurtValueRateUp;
    self.LevelLimit = LevelLimit;
    self.SpendMP = SpendMP;
    self.SpendMPLevelUp = SpendMPLevelUp;
    self.IsPhyAttack = IsPhyAttack;
    self.IsDoCameraShake = IsDoCameraShake;
    self.CameraShakeDelay = CameraShakeDelay;
    self.SkillCD = SkillCD;
    self.SkillUpCDTime = SkillUpCDTime;
    self.EffectLifeTime = EffectLifeTime;
    self.AttackTargetCount = AttackTargetCount;
    self.IsTargetMonster = IsTargetMonster;
    self.AttackRange = AttackRange;
    self.AreaAttackRadius = AreaAttackRadius;
    self.ShowEffectDelay = ShowEffectDelay;
    self.ShowHurtEffectDelaySecond = ShowHurtEffectDelaySecond;
    self.RedScreen = RedScreen;
    self.AttackState = AttackState;
    self.AbnormalState = AbnormalState;
    self.BuffInfoID = BuffInfoID;
    self.BuffTargetFilter = BuffTargetFilter;
    self.BuffIsPercentage = BuffIsPercentage;

    return self;
end