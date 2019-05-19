using System;
using System.Collections;
using System.Collections.Generic;

namespace GameServerApp.Common
{
    public class EventDispatcher : Singleton<EventDispatcher>
    {

        //委托原型
        public delegate void OnActionHandler(Role role, byte[] buffer);

        private Dictionary<ushort, List<OnActionHandler>> dic = new Dictionary<ushort, List<OnActionHandler>>();

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="protoCode">协议编号</param>
        /// <param name="handler">委托</param>
        public void AddEventListener(ushort protoCode, OnActionHandler handler)
        {
            if (dic.ContainsKey(protoCode))
            {
                dic[protoCode].Add(handler);
            }
            else
            {
                List<OnActionHandler> lstHandler = new List<OnActionHandler>();
                lstHandler.Add(handler);
                dic[protoCode] = lstHandler;
            }
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="protoCode">协议编号</param>
        /// <param name="handler">监听</param>
        public void RemoveEventListener(ushort protoCode, OnActionHandler handler)
        {
            if (dic.ContainsKey(protoCode))
            {
                List<OnActionHandler> lstHandler = dic[protoCode];
                lstHandler.Remove(handler);
                if (lstHandler.Count == 0)
                {
                    dic.Remove(protoCode);
                }
            }
        }

        /// <summary>
        /// 派发协议
        /// </summary>
        /// <param name="protoCode">协议编号</param>
        /// <param name="role">角色</param>
        /// <param name="buffer">消息体</param>
        public void Dispatcher(ushort protoCode,Role role, byte[] buffer)
        {
            if (dic.ContainsKey(protoCode))
            {
                List<OnActionHandler> lstHandler = dic[protoCode];
                if (lstHandler != null && lstHandler.Count > 0)
                {
                    for (int i = 0; i < lstHandler.Count; i++)
                    {
                        if (lstHandler[i] != null)
                        {
                            lstHandler[i](role,buffer);
                        }
                    }
                }
            }
        }
    }
}
