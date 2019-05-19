/// <summary>
/// 功能描述    ：MathUtil  
/// 创 建 者    ：Administrator
/// 创建日期    ：2019/1/10 10:25:26 
/// 最后修改者  ：Administrator
/// 最后修改日期：2019/1/10 10:25:26 
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FightingUtil
{
    private static float m_HPFight = 1;
    private static float m_AttackFight = 3;
    private static float m_DefenseFight = 2;
    private static float m_ResFight = 2;
    private static float m_HitFight = 0.5f;
    private static float m_DodgeFight = 0.5f;
    private static float m_CriFight = 1f;

    public static float HPFight { get { return m_HPFight; } }
    public static float AttackFight { get { return m_AttackFight; } }
    public static float DefenseFight { get { return m_DefenseFight; } }
    public static float ResFight { get { return m_ResFight; } }
    public static float HitFight { get { return m_HitFight; } }
    public static float DodgeFight { get { return m_DodgeFight; } }
    public static float CriFight { get { return m_CriFight; } }

    public static int GetRoleFighting(int hp,int attack,int attackAdd,int def,int defAdd,int res,int resAdd,int hit,int hitAdd,int dodge,int dodgeAdd,int cri,int criAdd)
    {
        int fighting = 0;

        fighting += GetOnePropertyFighting(hp, 0, HPFight);
        fighting += GetOnePropertyFighting(attack, attackAdd, AttackFight);
        fighting += GetOnePropertyFighting(def, defAdd, DefenseFight);
        fighting += GetOnePropertyFighting(res, resAdd, ResFight);
        fighting += GetOnePropertyFighting(hit, hitAdd, HitFight);
        fighting += GetOnePropertyFighting(dodge, dodgeAdd, DodgeFight);
        fighting += GetOnePropertyFighting(cri, criAdd, CriFight);
        return fighting;
    }

    public static int GetRoleFighting(RoleInfoResponseProto proto)
    {
        int fighting = 0;

        fighting += GetOnePropertyFighting(proto.MaxHP, 0, HPFight);
        fighting += GetOnePropertyFighting(proto.Attack, proto.AttackAddition, AttackFight);
        fighting += GetOnePropertyFighting(proto.Defense, proto.DefenseAddition, DefenseFight);
        fighting += GetOnePropertyFighting(proto.Res, proto.ResAddition, ResFight);
        fighting += GetOnePropertyFighting(proto.Hit, proto.HitAddition, HitFight);
        fighting += GetOnePropertyFighting(proto.Dodge, proto.DodgeAddition, DodgeFight);
        fighting += GetOnePropertyFighting(proto.Cri, proto.CriAddition, CriFight);
        return fighting;
    }

    /// <summary> 获取一项属性的战斗力 </summary>
    /// <param name="propertyValue">属性值</param>
    /// <param name="propertyAddition">属性加成(需要除以1000)</param>
    /// <param name="rate">属性倍率</param>
    /// <returns></returns>
    public static int GetOnePropertyFighting(int propertyValue,int propertyAddition,float rate)
    {
        return (int)((1 + propertyAddition*0.001f) * propertyValue * rate);
    }
}
