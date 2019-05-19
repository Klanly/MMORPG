using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProtoEventListener:Singleton<ProtoEventListener>
{
    public void Init()
    {
        AccountCacheModel.Instance.Init();
        GameServerCacheModel.Instance.Init();
    }
}
