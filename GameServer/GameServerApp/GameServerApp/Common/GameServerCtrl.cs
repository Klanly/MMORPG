using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class GameServerCtrl:Singleton<GameServerCtrl>
{

    public void CreateServer()
    {
        for (int i = 0; i < 35; i++)
        {
            GameServerEntity entity = new GameServerEntity();
            entity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
            entity.RunStatus = 1;
            entity.IsCommand = false;
            entity.IsNew = false;
            entity.Name = "琅嬛福地" + (i + 1);
            entity.Ip = "";
            entity.Port = 1000 + (i + 1);
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;
            GameServerCacheModel.Instance.Create(entity);
        }
    }

}
