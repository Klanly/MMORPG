using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary> 角色信息基类 </summary>
public class RoleInfoBase 
{
    /// <summary> 角色编号 </summary>
    public int RoleId { get; set; }
    /// <summary> 角色昵称 </summary>
    public string NickName { get; set; }
    /// <summary> 角色性别 </summary>
    public int Sex { get; set; }
    /// <summary> 等级 </summary>
    public int Level { get; set; }
    /// <summary> 经验 </summary>
    public int Exp { get; set; }
    /// <summary> 最大血量 </summary>
    public int MaxHP { get; set; }
    /// <summary> 当前血量 </summary>
    public int CurrHP { get; set; }
    /// <summary> 最大法力 </summary>
    public int MaxMP { get; set; }
    /// <summary> 当前法力 </summary>
    public int CurrMP { get; set; }
    /// <summary> 攻击 </summary>
    public int Attack { get; set; }
    /// <summary> 攻击加成 </summary>
    public int AttackAddition { get; set; }
    /// <summary> 物理防御 </summary>
    public int Defense { get; set; }
    /// <summary> 物理防御加成 </summary>
    public int DefenseAddition { get; set; }
    /// <summary> 魔法防御 </summary>
    public int Res { get; set; }
    /// <summary> 魔法防御加成 </summary>
    public int ResAddition { get; set; }
    /// <summary> 命中 </summary>
    public int Hit { get; set; }
    /// <summary> 命中加成 </summary>
    public int HitAddition { get; set; }
    /// <summary> 闪避 </summary>
    public int Dodge { get; set; }
    /// <summary> 闪避加成 </summary>
    public int DodgeAddition { get; set; }
    /// <summary> 必杀 </summary>
    public int Cri { get; set; }
    /// <summary> 必杀加成 </summary>
    public int CriAddition { get; set; }
    /// <summary> 战斗力 </summary>
    public int Fighting { get; private set; }
    /// <summary> 战斗力加成 </summary>
    public int FightingAddition { get; private set; }

    /// <summary> 角色已经学会的技能列表 </summary>
    public Dictionary<int, RoleSkillInfo> SkillDic;

    public Dictionary<int, RoleSkillInfo> PhySkillDic;
    public RoleInfoBase()
    {
        SkillDic = new Dictionary<int, RoleSkillInfo>();
        PhySkillDic = new Dictionary<int, RoleSkillInfo>();
    }

    public void Init(RoleInfoResponseProto proto)
    {
        Debuger.LogError("RoleInfoInit level = " + proto.Level);
        RoleId = proto.RoleId;
        NickName = proto.NickName;
        Sex = proto.Sex;
        Level = proto.Level;
        Exp = proto.Exp;
        MaxHP = proto.MaxHP;
        CurrHP = proto.CurrHP;
        MaxMP = proto.MaxMP;
        CurrMP = proto.CurrMP;
        Attack = proto.Attack;
        AttackAddition = (int)(proto.AttackAddition * 0.001f * Attack);
        Defense = proto.Defense;
        DefenseAddition = (int)(proto.DefenseAddition * 0.001f * Defense);
        Res = proto.Res;
        ResAddition = (int)(proto.ResAddition * 0.001f * Res);
        Hit = proto.Hit;
        HitAddition = (int)(proto.HitAddition * 0.001f * Hit);
        Dodge = proto.Dodge;
        DodgeAddition = (int)(proto.DodgeAddition * 0.001f * Dodge);
        Cri = proto.Cri;
        CriAddition = (int)(proto.CriAddition * 0.001f * Cri);
        Fighting = proto.Fighting;
        FightingAddition = (int)(proto.FightingAddition * Fighting);
    }

    public void SetFighting(int value)
    {
        Fighting = value;
    }

    /// <summary> 获取技能等级 </summary>
    /// <param name="skillId">技能Id</param>
    /// <returns></returns>
    public int GetSkillLevel(int skillId)
    {
        if (SkillDic == null) return 0;
        foreach (var item in SkillDic)
        {
            if (item.Value.SkillId == skillId)
            {
                return item.Value.SkillLevel;
            }
        }
        return 0;
    }

    public int GetFinalAttack() { return Attack + AttackAddition; }
    public int GetFinalDefense() { return Defense + DefenseAddition; }
    public int GetFinalRes() { return Res + ResAddition; }
    public int GetFinalHit() { return Hit + HitAddition; }
    public int GetFinalDodge() { return Dodge + DodgeAddition; }
    public int GetFinalCri() { return Cri + CriAddition; }
    
    /// <summary> 设置技能结束时间 </summary>
    /// <param name="skillId"></param>
    public void SetSkillCDEndTime(int skillId)
    {
        if (SkillDic.Count > 0)
        {
            foreach (var item in SkillDic)
            {
                if (item.Value.SkillId == skillId)
                {
                    item.Value.SkillCDEndTime = Time.time + item.Value.SkillCDTime;
                    break;
                }
            }
        }
    }

    /// <summary> 获取可使用的技能Id </summary>
    /// <returns></returns>
    public int GetCanUseSkillId()
    {
        if (SkillDic.Count > 0)
        {
            foreach (var item in SkillDic)
            {
                if (Time.time > item.Value.SkillCDEndTime)
                {
                    return item.Value.SkillId;
                }
            }
        }
        return 0;
    }
}