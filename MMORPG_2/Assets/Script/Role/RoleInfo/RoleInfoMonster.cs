using UnityEngine;
using System.Collections;

/// <summary> 怪信息 </summary>
public class RoleInfoMonster : RoleInfoBase
{
    /// <summary> 怪物实体 </summary>
    public MonsterEntity MonsterEntity { get; set; }

    /// <summary> 怪物实体 </summary>
    public GameLevelMonsterEntity GameLevelMonsterEntity { get; set; }

    public void SetMonsterData(MonsterEntity entity)
    {
        NickName = entity.Name;
        CurrHP = MaxHP = entity.HP;
        //CurrHP = MaxHP = entity.HP+99999999;
        Attack = entity.Attack;
        Defense = entity.Defense;
        Res = entity.ResDefense;
        Hit = entity.Hit;
        Dodge = entity.Dodge;
        Cri = entity.Cri;
        int fighting = FightingUtil.CalculateRoleFighting(MaxHP, Attack, AttackAddition, Defense, DefenseAddition, Res, ResAddition, Hit, HitAddition, Dodge, DodgeAddition, Cri, CriAddition);
        SetFighting(fighting);
    }
}