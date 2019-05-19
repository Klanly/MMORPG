using GameServerApp.Common;
using Mmcoy.Framework;
using Mmcoy.Framework.AbstractBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp.Controllder
{
    public class RoleController : Singleton<RoleController>, IDisposable
    {
        public int ServerId = 36;
        public void Init()
        {
            //添加相关协议监听
            //客户端发送请求区服协议
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleLogOnGameServerRequest, OnLogOnGameServer);
            //客户端发送创建角色协议
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleCreateRequest, OnRoleCreate);
            //客户端发送请求角色信息协议
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleInfoRequest, OnRoleInfo);
            //客户端请求进入副本
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.GameLevelEnterRequest, OnGameLevelEnter);
            //客户端发送战斗胜利协议
            EventDispatcher.Instance.AddEventListener(ProtoCodeDef.GameLevelSuccessRequest, OnGameLevelSuccess);
        }

        /// <summary> 客户端请求区服列表 </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnLogOnGameServer(Role role, byte[] buffer)
        {
            RoleLogOnGameServerRequestProto proto = RoleLogOnGameServerRequestProto.GetProto(buffer);
            int accountId = proto.AccountId;
            role.AccountId = accountId;
            LogOnGameServerResponse(role, accountId);
        }

        /// <summary> 服务端返回区服列表 </summary>
        /// <param name="role"></param>
        /// <param name="accountId"></param>
        private void LogOnGameServerResponse(Role role, int accountId)
        {
            RoleLogOnGameServerResponseProto proto = new RoleLogOnGameServerResponseProto();
            List<RoleEntity> list = RoleCacheModel.Instance.GetList(condition: string.Format("[AccountId]={0}", accountId));
            if (list != null && list.Count > 0)
            {
                proto.RoleCount = list.Count;
                proto.RoleList = new List<RoleLogOnGameServerResponseProto.RoleItem>();
                for (int i = 0; i < list.Count; i++)
                {
                    proto.RoleList.Add(new RoleLogOnGameServerResponseProto.RoleItem()
                    {
                        RoleId = list[i].Id.Value,
                        RoleNickName = list[i].NickName,
                        RoleJob = (byte)list[i].JobId,
                        RoleLevel = list[i].Level
                    });
                }
            }
            else
            {
                proto.RoleCount = 0;
            }
            role.ClientSocket.SendMsg(proto.ToArray());
        }
        /// <summary> 客户端请求创建角色 </summary>
        /// <param name="role"></param>
        /// <param name="buffer"></param>
        private void OnRoleCreate(Role role, byte[] buffer)
        {
            RoleCreateRequestProto proto = RoleCreateRequestProto.GetProto(buffer);
            //查询昵称是否存在
            int count = RoleCacheModel.Instance.GetCount(string.Format("[NickName]='{0}'", proto.RoleName));
            MFReturnValue<object> retValue = null;
            if (count > 0)
            {
                //角色名已经存
                retValue = new MFReturnValue<object>();
                retValue.HasError = true;
            }
            else
            {
                RoleEntity entity = new RoleEntity();
                entity.JobId = proto.JobId;
                entity.NickName = proto.RoleName;
                entity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                entity.AccountId = role.AccountId;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.Level = 1;
                //给角色战斗相关的属性赋值(根据 职业 等级)

                JobEntity jobEntity = JobDBModel.Instance.Get(entity.JobId);
                JobLevelEntity jobLevelEntity = JobLevelDBModel.Instance.Get(entity.Level);

                entity.Attack = (int)Math.Round(jobLevelEntity.Attack * (jobEntity.AttackCoefficient *  0.01f));
                entity.AttackAddition = 0;
                entity.Defense = (int)Math.Round(jobLevelEntity.Defense * jobEntity.DefenseCoefficient * 0.01f);
                entity.DefenseAddition = 0;
                entity.Res = (int)Math.Round(jobEntity.ResCoefficient * jobLevelEntity.Res * 0.01f);
                entity.ResAddition = 0;
                entity.Hit = (int)Math.Round(jobEntity.HitCoefficient * jobLevelEntity.Hit * 0.01f);
                entity.HitAddition = 0;
                entity.Cri = (int)Math.Round(jobEntity.CriCoefficient * jobLevelEntity.Cri * 0.01f);
                entity.CriAddition = 0;
                entity.Dodge = (int)Math.Round(jobEntity.DodgeCoefficient * jobLevelEntity.Dodge * 0.01f);
                entity.DodgeAddition = 0;
                entity.CurrHP = entity.MaxHP = jobLevelEntity.HP;
                entity.CurrMP = entity.MaxMP = jobLevelEntity.MP;
                entity.Fighting = FightingUtil.GetRoleFighting(entity.MaxHP, entity.Attack, 0, entity.Defense, 0, entity.Res, 0, entity.Hit, 0, entity.Dodge, 0, entity.Cri, 0);
                entity.FightingAddition = 0;
                entity.LastInWorldMapId = 1;
                retValue = RoleCacheModel.Instance.Create(entity);
                Console.WriteLine(retValue.ReturnCode +"  "+retValue.OutputValues["Id"] +"   "+retValue.Message);
            }
            OnRoleCreateResponse(role, retValue);
        }

        /// <summary> 服务器返回创建角色 </summary>
        /// <param name="role"></param>
        /// <param name="retValue"></param>
        private void OnRoleCreateResponse(Role role, MFReturnValue<object> retValue)
        {
            RoleCreateResponseProto proto = new RoleCreateResponseProto();
            if (!retValue.HasError)
            {
                proto.IsSuccess = true;
                //初始化角色技能
                RoleSkillEntity entity1 = new RoleSkillEntity();
                entity1.Status = EnumEntityStatus.Released;
                entity1.RoleId = (int)retValue.OutputValues["Id"];
                entity1.SkillId = 106;
                entity1.SkillLevel = 1;
                entity1.SlotsNode = 1;
                entity1.CreateTime = DateTime.Now;
                entity1.UpdateTime = DateTime.Now;
                RoleSkillDBModel.Instance.Create(entity1);
                RoleSkillEntity entity2 = new RoleSkillEntity();
                entity2.Status = EnumEntityStatus.Released;
                entity2.RoleId = (int)retValue.OutputValues["Id"];
                entity2.SkillId = 107;
                entity2.SkillLevel = 1;
                entity2.SlotsNode = 2;
                entity2.CreateTime = DateTime.Now;
                entity2.UpdateTime = DateTime.Now;
                RoleSkillDBModel.Instance.Create(entity2);
            }
            else
            {
                proto.IsSuccess = false;
                proto.MsgCode = 1000305;
            }
            role.ClientSocket.SendMsg(proto.ToArray());
        }


        private void OnRoleInfo(Role role, byte[] buffer)
        {
            RoleInfoRequestProto proto = RoleInfoRequestProto.GetProto(buffer);
            RoleEntity entity = RoleCacheModel.Instance.GetEntity(proto.RoleId);
            role.RoleId = proto.RoleId;
            role.RoleName = entity.NickName;
            OnRoleInfoResponse(role, entity);
            OnRoleSkillResponse(role);
        }
        private void OnRoleInfoResponse(Role role, RoleEntity entity)
        {
            RoleInfoResponseProto proto = new RoleInfoResponseProto();
            if (entity == null)
            {
                proto.IsSuccess = false;
                proto.MsgCode = 1000307;
            }
            else
            {
                proto.IsSuccess = true;
                proto.RoleId = entity.Id.ToInt();
                proto.JobId = entity.JobId;
                proto.NickName = entity.NickName;
                proto.Sex = entity.Sex;
                proto.Level = entity.Level;
                proto.Money = entity.Money;
                proto.Gold = entity.Gold;
                proto.Exp = entity.Exp;
                proto.MaxHP = entity.MaxHP;
                proto.CurrHP = entity.CurrHP;
                proto.MaxMP = entity.MaxMP;
                proto.CurrMP = entity.CurrMP;
                proto.Attack = entity.Attack;
                proto.AttackAddition = entity.AttackAddition;
                proto.Defense = entity.Defense;
                proto.DefenseAddition = entity.DefenseAddition;
                proto.Res = entity.Res;
                proto.ResAddition = entity.ResAddition;
                proto.Hit = entity.Hit;
                proto.HitAddition = entity.HitAddition;
                proto.Dodge = entity.Dodge;
                proto.DodgeAddition = entity.DodgeAddition;
                proto.Cri = entity.Cri;
                proto.CriAddition = entity.CriAddition;
                proto.Fighting = entity.Fighting;
                proto.FightingAddition = entity.FightingAddition;
                proto.LastInWorldMapId = entity.LastInWorldMapId;
            }
            role.ClientSocket.SendMsg(proto.ToArray());
        }

        private void OnRoleSkillResponse(Role role)
        {
            RoleSkillDataResponseProto proto = new RoleSkillDataResponseProto();
            List<RoleSkillEntity> list = RoleSkillDBModel.Instance.GetList(condition: "[RoleId]=" + role.RoleId);
            if (list != null)
            {
                proto.SkillCount = (byte)list.Count;
                proto.CurrSkillDataList = new List<RoleSkillDataResponseProto.SkillData>();
                for (int i = 0; i < list.Count; i++)
                {
                    proto.CurrSkillDataList.Add(new RoleSkillDataResponseProto.SkillData()
                    {
                        SkillId = list[i].SkillId,
                        SkillLevel = list[i].SkillLevel,
                        SlotsNode = list[i].SlotsNode
                    });
                }
            }
            role.ClientSocket.SendMsg(proto.ToArray());
        }

        private void OnGameLevelEnter(Role role, byte[] buffer)
        {
            GameLevelEnterRequestProto proto = GameLevelEnterRequestProto.GetProto(buffer);
            ChapterLevelEntity entity = ChapterLevelDBModel.Instance.GetList(proto.GameChapterId)[proto.GameLevelId - 1];
            Console.WriteLine(string.Format("玩家:{0} 请求进入{1}-{2}副本，难度等级为{3},世界Id={4}",
                role.RoleName,proto.GameChapterId,proto.GameLevelId,proto.Grade, entity.WorldMapId));
            OnGameLevelEnterResponse(role,entity);
        }
        private void OnGameLevelEnterResponse(Role role,ChapterLevelEntity entity)
        {
            GameLevelEnterResponseProto proto = new GameLevelEnterResponseProto();
            if (entity.WorldMapId == 5)
            {
                proto.IsSuccess = true;
            }
            else
            {
                proto.IsSuccess = false;
                proto.MsgCode = 1000402;
            }
            
            role.ClientSocket.SendMsg(proto.ToArray());
        }
        private void OnGameLevelSuccess(Role role, byte[] buffer)
        {
            GameLevelSuccessRequestProto proto = GameLevelSuccessRequestProto.GetProto(buffer);
            Console.WriteLine(proto.ChapterId + "   " + proto.GameLevelId);
            Console.WriteLine(proto.Exp + "   " + proto.Gold);
            for (int i = 0; i < proto.KillMonsterList.Count; i++)
            {
                Console.WriteLine("怪物Id："+ proto.KillMonsterList[i].MonsterId + "  怪物数量=" + proto.KillMonsterList[i].MonsterCount);
            }
            for (int i = 0; i < proto.GetGoodsList.Count; i++)
            {
                Console.WriteLine("物品Id：" + proto.GetGoodsList[i].GoodsId + "  物品类型 = " + (proto.GetGoodsList[i].GoodsType==1?"装备":"材料")+"   数量="+proto.GetGoodsList[i].GoodsCount);
            }

            for (int i = 0; i < proto.GetGoodsList.Count; i++)
            {
                Role_BackpackEntity entity = new Role_BackpackEntity();
                entity.Status = EnumEntityStatus.Released;
                entity.RoleId = role.RoleId;
                entity.GoodsType = proto.GetGoodsList[i].GoodsType;
                entity.GoodsId = proto.GetGoodsList[i].GoodsId;
                entity.GoodsCount = proto.GetGoodsList[i].GoodsCount;
                entity.GoodsSvrId = ServerId;
                entity.CreateTime = DateTime.Now;
                Role_BackpackDBModel.Instance.Create(entity);
            }
            UpdateRoleInfo(role, proto.Exp, proto.Gold);
        }


        public void UpdateRoleInfo(Role role,int exp,int coin)
        {
            RoleEntity roleEntity = RoleDBModel.Instance.GetEntity(role.RoleId);
            int[] roleLevelExp = GetRoleLevel(roleEntity, exp);
            Console.WriteLine(roleLevelExp[0]+"   "+roleLevelExp[1]);
            roleEntity.Level = roleLevelExp[0];
            roleEntity.Exp = roleLevelExp[1];
            roleEntity.Gold = coin;
            JobEntity jobEntity = JobDBModel.Instance.Get(roleEntity.JobId);
            JobLevelEntity jobLevelEntity = JobLevelDBModel.Instance.Get(roleEntity.Level);
            roleEntity.Attack = (int)Math.Round(jobLevelEntity.Attack * (jobEntity.AttackCoefficient * 0.01f));
            roleEntity.AttackAddition = 0;
            roleEntity.Defense = (int)Math.Round(jobLevelEntity.Defense * jobEntity.DefenseCoefficient * 0.01f);
            roleEntity.DefenseAddition = 0;
            roleEntity.Res = (int)Math.Round(jobEntity.ResCoefficient * jobLevelEntity.Res * 0.01f);
            roleEntity.ResAddition = 0;
            roleEntity.Hit = (int)Math.Round(jobEntity.HitCoefficient * jobLevelEntity.Hit * 0.01f);
            roleEntity.HitAddition = 0;
            roleEntity.Cri = (int)Math.Round(jobEntity.CriCoefficient * jobLevelEntity.Cri * 0.01f);
            roleEntity.CriAddition = 0;
            roleEntity.Dodge = (int)Math.Round(jobEntity.DodgeCoefficient * jobLevelEntity.Dodge * 0.01f);
            roleEntity.DodgeAddition = 0;
            roleEntity.CurrHP = roleEntity.MaxHP = jobLevelEntity.HP;
            roleEntity.CurrMP = roleEntity.MaxMP = jobLevelEntity.MP;
            roleEntity.Fighting = FightingUtil.GetRoleFighting(roleEntity.MaxHP, roleEntity.Attack, 0, roleEntity.Defense, 0, roleEntity.Res, 0, roleEntity.Hit, 0, roleEntity.Dodge, 0, roleEntity.Cri, 0);
            RoleDBModel.Instance.Update(roleEntity);
            OnRoleInfoResponse(role, roleEntity);
        }

        private int[] GetRoleLevel(RoleEntity roleEntity, int exp)
        {
            int[] levelExpArray = new int[2];
            int level = roleEntity.Level;
            exp += roleEntity.Exp;
            while (exp >= JobLevelDBModel.Instance.Get(level).NeedExp)
            {
                exp -= JobLevelDBModel.Instance.Get(level).NeedExp;
                level++;
            }
            levelExpArray[0] = level;
            levelExpArray[1] = exp;
            return levelExpArray;
        }

        public override void Dispose()
        {
            base.Dispose();
            //移除监听
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleLogOnGameServerRequest, OnLogOnGameServer);
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleCreateRequest, OnRoleCreate);
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleInfoRequest, OnRoleInfo);
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.GameLevelEnterRequest, OnGameLevelEnter);
            EventDispatcher.Instance.RemoveEventListener(ProtoCodeDef.GameLevelSuccessRequest, OnGameLevelSuccess);
        }
    }
}
