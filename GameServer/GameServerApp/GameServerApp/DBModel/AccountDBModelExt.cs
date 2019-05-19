using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class AccountDBModel
{
    public AccountRegisterResponseProto Register(string userName, string pwd,string chennelId,string deviceIdentifier,string deviceModel)
    {
        AccountRegisterResponseProto retProto = new AccountRegisterResponseProto();

        //1.判断用户名是否存在
        //2.如果存在添加数据
        using (SqlConnection conn = new SqlConnection(DBConn.DBAccount))
        {
            conn.Open();
            //开启事务 只打开一次数据库
            SqlTransaction trans = conn.BeginTransaction();
            List<AccountEntity> list = GetListWithTran(this.TableName, "Id", "UserName='" + userName + "'", trans: trans,isAutoStatus:false);
            Console.WriteLine(list.Count);
            //Console.WriteLine(list[0].Mobile + "   " + list[0].Mail+"  "+ list[0].Status);
            if (list == null || list.Count == 0)
            {
                //说明用户名不存在 可以添加数据
                AccountEntity entity = new AccountEntity();
                entity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                entity.UserName = userName;
                entity.Pwd = pwd;
                entity.ChannelId = chennelId;
                entity.LastLogOnServerTime = DateTime.Now;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.DeviceIdentifier = deviceIdentifier;
                entity.DeviceModel = deviceModel;

                MFReturnValue<object> ret = this.Create(trans, entity);
                if (!ret.HasError)
                {
                    retProto.IsSuccess = true;
                    retProto.UserId = (int)ret.OutputValues["Id"];
                    trans.Commit();
                }
                else
                {
                    retProto.IsSuccess = false;
                    retProto.MsgCode = 102;
                    trans.Rollback();
                }
            }
            else
            {
                retProto.IsSuccess = false;
                retProto.MsgCode = 102;
            }
        }

        return retProto;
    }

    public AccountLogOnResponseProto LogOn(string userName, string pwd,string deviceIdentifier,string deviceModel)
    {
        AccountLogOnResponseProto proto = new AccountLogOnResponseProto();
        string condition = string.Format("[UserName]='{0}'", userName);
        AccountEntity entity = this.GetEntity(condition);
        if (entity == null)
        {
            //账户不存在
            proto.IsSuccess = false;
            proto.MsgCode = 103;
        }
        else
        {
            string password = entity.Pwd;
            if (!password.Equals(pwd))
            {
                //密码不相同
                proto.IsSuccess = false;
                proto.MsgCode = 104;
            }
            else
            {
                entity.DeviceIdentifier = deviceIdentifier;
                entity.DeviceModel = deviceModel;
                entity.UpdateTime = DateTime.Now;
                entity.LastLogOnServerTime = DateTime.Now;
                this.Update(entity);
                proto.IsSuccess = true;
                proto.UserId = (int)entity.Id;
            }
        }
        return proto;
    }
}
