/// <summary>
/// 类名 : RoleDBModel
/// 作者 : 北京-边涯
/// 说明 : 
/// 创建日期 : 2019-05-05 22:04:33
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Mmcoy.Framework.AbstractBase;

/// <summary>
/// DBModel
/// </summary>
public partial class RoleDBModel : MFAbstractSQLDBModel<RoleEntity>
{
    #region RoleDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private RoleDBModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static RoleDBModel instance = null;
    public static RoleDBModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new RoleDBModel();
                    }
                }
            }
            return instance;
        }
    }
    #endregion

    #region 实现基类的属性和方法

    #region ConnectionString 数据库连接字符串
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    protected override string ConnectionString
    {
        get { return DBConn.DBGameServer; }
    }
    #endregion

    #region TableName 表名
    /// <summary>
    /// 表名
    /// </summary>
    protected override string TableName
    {
        get { return "Role"; }
    }
    #endregion

    #region ColumnList 列名集合
    private IList<string> _ColumnList;
    /// <summary>
    /// 列名集合
    /// </summary>
    protected override IList<string> ColumnList
    {
        get
        {
            if (_ColumnList == null)
            {
                _ColumnList = new List<string> { "Id", "Status", "AccountId", "JobId", "NickName", "Sex", "Level", "Money", "Gold", "Exp", "MaxHP", "CurrHP", "MaxMP", "CurrMP", "Attack", "AttackAddition", "Defense", "DefenseAddition", "Res", "ResAddition", "Hit", "HitAddition", "Dodge", "DodgeAddition", "Cri", "CriAddition", "Fighting", "FightingAddition", "LastPassGameLevelId", "LastInWorldMapId", "LastInWorldMapPos", "CreateTime", "UpdateTime", "MaxExp" };
            }
            return _ColumnList;
        }
    }
    #endregion

    #region ValueParas 转换参数
    /// <summary>
    /// 转换参数
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected override SqlParameter[] ValueParas(RoleEntity entity)
    {
        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Id", entity.Id) { DbType = DbType.Int32 },
                new SqlParameter("@Status", entity.Status) { DbType = DbType.Int32 },
                new SqlParameter("@AccountId", entity.AccountId) { DbType = DbType.Int32 },
                new SqlParameter("@JobId", entity.JobId) { DbType = DbType.Int32 },
                new SqlParameter("@NickName", entity.NickName) { DbType = DbType.String },
                new SqlParameter("@Sex", entity.Sex) { DbType = DbType.Byte },
                new SqlParameter("@Level", entity.Level) { DbType = DbType.Int32 },
                new SqlParameter("@Money", entity.Money) { DbType = DbType.Int32 },
                new SqlParameter("@Gold", entity.Gold) { DbType = DbType.Int32 },
                new SqlParameter("@Exp", entity.Exp) { DbType = DbType.Int32 },
                new SqlParameter("@MaxHP", entity.MaxHP) { DbType = DbType.Int32 },
                new SqlParameter("@CurrHP", entity.CurrHP) { DbType = DbType.Int32 },
                new SqlParameter("@MaxMP", entity.MaxMP) { DbType = DbType.Int32 },
                new SqlParameter("@CurrMP", entity.CurrMP) { DbType = DbType.Int32 },
                new SqlParameter("@Attack", entity.Attack) { DbType = DbType.Int32 },
                new SqlParameter("@AttackAddition", entity.AttackAddition) { DbType = DbType.Int32 },
                new SqlParameter("@Defense", entity.Defense) { DbType = DbType.Int32 },
                new SqlParameter("@DefenseAddition", entity.DefenseAddition) { DbType = DbType.Int32 },
                new SqlParameter("@Res", entity.Res) { DbType = DbType.Int32 },
                new SqlParameter("@ResAddition", entity.ResAddition) { DbType = DbType.Int32 },
                new SqlParameter("@Hit", entity.Hit) { DbType = DbType.Int32 },
                new SqlParameter("@HitAddition", entity.HitAddition) { DbType = DbType.Int32 },
                new SqlParameter("@Dodge", entity.Dodge) { DbType = DbType.Int32 },
                new SqlParameter("@DodgeAddition", entity.DodgeAddition) { DbType = DbType.Int32 },
                new SqlParameter("@Cri", entity.Cri) { DbType = DbType.Int32 },
                new SqlParameter("@CriAddition", entity.CriAddition) { DbType = DbType.Int32 },
                new SqlParameter("@Fighting", entity.Fighting) { DbType = DbType.Int32 },
                new SqlParameter("@FightingAddition", entity.FightingAddition) { DbType = DbType.Int32 },
                new SqlParameter("@LastPassGameLevelId", entity.LastPassGameLevelId) { DbType = DbType.Int32 },
                new SqlParameter("@LastInWorldMapId", entity.LastInWorldMapId) { DbType = DbType.Int32 },
                new SqlParameter("@LastInWorldMapPos", entity.LastInWorldMapPos) { DbType = DbType.String },
                new SqlParameter("@CreateTime", entity.CreateTime) { DbType = DbType.DateTime },
                new SqlParameter("@UpdateTime", entity.UpdateTime) { DbType = DbType.DateTime },
                new SqlParameter("@MaxExp", entity.MaxExp) { DbType = DbType.Int32 },
                new SqlParameter("@RetMsg", SqlDbType.NVarChar, 255),
                new SqlParameter("@ReturnValue", SqlDbType.Int)
            };
        return parameters;
    }
    #endregion

    #region GetEntitySelfProperty 封装对象
    /// <summary>
    /// 封装对象
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    protected override RoleEntity GetEntitySelfProperty(IDataReader reader, DataTable table)
    {
        RoleEntity entity = new RoleEntity();
        foreach (DataRow row in table.Rows)
        {
            var colName = row.Field<string>(0);
            if (reader[colName] is DBNull)
                continue;
            switch (colName.ToLower())
            {
                case "id":
                    if (!(reader["Id"] is DBNull))
                        entity.Id = Convert.ToInt32(reader["Id"]);
                    break;
                case "status":
                    if (!(reader["Status"] is DBNull))
                        entity.Status = (EnumEntityStatus)Convert.ToInt32(reader["Status"]);
                    break;
                case "accountid":
                    if (!(reader["AccountId"] is DBNull))
                        entity.AccountId = Convert.ToInt32(reader["AccountId"]);
                    break;
                case "jobid":
                    if (!(reader["JobId"] is DBNull))
                        entity.JobId = Convert.ToInt32(reader["JobId"]);
                    break;
                case "nickname":
                    if (!(reader["NickName"] is DBNull))
                        entity.NickName = Convert.ToString(reader["NickName"]);
                    break;
                case "sex":
                    if (!(reader["Sex"] is DBNull))
                        entity.Sex = Convert.ToByte(reader["Sex"]);
                    break;
                case "level":
                    if (!(reader["Level"] is DBNull))
                        entity.Level = Convert.ToInt32(reader["Level"]);
                    break;
                case "money":
                    if (!(reader["Money"] is DBNull))
                        entity.Money = Convert.ToInt32(reader["Money"]);
                    break;
                case "gold":
                    if (!(reader["Gold"] is DBNull))
                        entity.Gold = Convert.ToInt32(reader["Gold"]);
                    break;
                case "exp":
                    if (!(reader["Exp"] is DBNull))
                        entity.Exp = Convert.ToInt32(reader["Exp"]);
                    break;
                case "maxhp":
                    if (!(reader["MaxHP"] is DBNull))
                        entity.MaxHP = Convert.ToInt32(reader["MaxHP"]);
                    break;
                case "currhp":
                    if (!(reader["CurrHP"] is DBNull))
                        entity.CurrHP = Convert.ToInt32(reader["CurrHP"]);
                    break;
                case "maxmp":
                    if (!(reader["MaxMP"] is DBNull))
                        entity.MaxMP = Convert.ToInt32(reader["MaxMP"]);
                    break;
                case "currmp":
                    if (!(reader["CurrMP"] is DBNull))
                        entity.CurrMP = Convert.ToInt32(reader["CurrMP"]);
                    break;
                case "attack":
                    if (!(reader["Attack"] is DBNull))
                        entity.Attack = Convert.ToInt32(reader["Attack"]);
                    break;
                case "attackaddition":
                    if (!(reader["AttackAddition"] is DBNull))
                        entity.AttackAddition = Convert.ToInt32(reader["AttackAddition"]);
                    break;
                case "defense":
                    if (!(reader["Defense"] is DBNull))
                        entity.Defense = Convert.ToInt32(reader["Defense"]);
                    break;
                case "defenseaddition":
                    if (!(reader["DefenseAddition"] is DBNull))
                        entity.DefenseAddition = Convert.ToInt32(reader["DefenseAddition"]);
                    break;
                case "res":
                    if (!(reader["Res"] is DBNull))
                        entity.Res = Convert.ToInt32(reader["Res"]);
                    break;
                case "resaddition":
                    if (!(reader["ResAddition"] is DBNull))
                        entity.ResAddition = Convert.ToInt32(reader["ResAddition"]);
                    break;
                case "hit":
                    if (!(reader["Hit"] is DBNull))
                        entity.Hit = Convert.ToInt32(reader["Hit"]);
                    break;
                case "hitaddition":
                    if (!(reader["HitAddition"] is DBNull))
                        entity.HitAddition = Convert.ToInt32(reader["HitAddition"]);
                    break;
                case "dodge":
                    if (!(reader["Dodge"] is DBNull))
                        entity.Dodge = Convert.ToInt32(reader["Dodge"]);
                    break;
                case "dodgeaddition":
                    if (!(reader["DodgeAddition"] is DBNull))
                        entity.DodgeAddition = Convert.ToInt32(reader["DodgeAddition"]);
                    break;
                case "cri":
                    if (!(reader["Cri"] is DBNull))
                        entity.Cri = Convert.ToInt32(reader["Cri"]);
                    break;
                case "criaddition":
                    if (!(reader["CriAddition"] is DBNull))
                        entity.CriAddition = Convert.ToInt32(reader["CriAddition"]);
                    break;
                case "fighting":
                    if (!(reader["Fighting"] is DBNull))
                        entity.Fighting = Convert.ToInt32(reader["Fighting"]);
                    break;
                case "fightingaddition":
                    if (!(reader["FightingAddition"] is DBNull))
                        entity.FightingAddition = Convert.ToInt32(reader["FightingAddition"]);
                    break;
                case "lastpassgamelevelid":
                    if (!(reader["LastPassGameLevelId"] is DBNull))
                        entity.LastPassGameLevelId = Convert.ToInt32(reader["LastPassGameLevelId"]);
                    break;
                case "lastinworldmapid":
                    if (!(reader["LastInWorldMapId"] is DBNull))
                        entity.LastInWorldMapId = Convert.ToInt32(reader["LastInWorldMapId"]);
                    break;
                case "lastinworldmappos":
                    if (!(reader["LastInWorldMapPos"] is DBNull))
                        entity.LastInWorldMapPos = Convert.ToString(reader["LastInWorldMapPos"]);
                    break;
                case "createtime":
                    if (!(reader["CreateTime"] is DBNull))
                        entity.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    break;
                case "updatetime":
                    if (!(reader["UpdateTime"] is DBNull))
                        entity.UpdateTime = Convert.ToDateTime(reader["UpdateTime"]);
                    break;
                case "maxexp":
                    if (!(reader["MaxExp"] is DBNull))
                        entity.MaxExp = Convert.ToInt32(reader["MaxExp"]);
                    break;
            }
        }
        return entity;
    }
    #endregion

    #endregion
}
