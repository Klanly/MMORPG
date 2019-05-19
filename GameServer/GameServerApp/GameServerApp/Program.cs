using GameServerApp.Controllder;
using Mmcoy.Framework.AbstractBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServerApp
{
    class Program
    {
        private static string m_ServerIP = "127.0.0.1";
        private static int m_Port = 9092;
        private static Socket m_ServerSocket;

        static void InitAllController()
        {
            RoleController.Instance.Init();
        }
        static void Main(string[] args)
        {
            ProtoEventListener.Instance.Init();
            InitAllController();
            //实例化Socket
            m_ServerSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            //向操作系统申请一个可用的ip和端口号用来通讯
            m_ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(m_ServerIP), m_Port));
            m_ServerSocket.Listen(3000);//最多3000个排队链接请求
            Console.WriteLine("启动监听{0}成功", m_ServerSocket.LocalEndPoint.ToString());
            Thread mThread = new Thread(ListenClientCallBack);
            mThread.Start();
            //======================
            //增
            //RoleEntity entity = new RoleEntity();
            //entity.Status = EnumEntityStatus.Released;
            //entity.NickName = "一页书";
            //entity.JobId = 1;
            //entity.CreateTime = DateTime.Now;
            //entity.UpdateTime = DateTime.Now;
            //RoleCacheModel.Instance.Create(entity);

            //改1
            //RoleEntity entity = new RoleEntity();
            //entity.Id = 1;
            //entity.Status = EnumEntityStatus.Released;
            //entity.NickName = "素还真";
            //entity.JobId = 1;
            //entity.CreateTime = DateTime.Now;
            //entity.UpdateTime = DateTime.Now;
            //RoleCacheModel.Instance.Update(entity);
            //2
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic["@NickName"] = "叶小钗";
            //dic["@Id"] = 1;
            //RoleCacheModel.Instance.Update("NickName=@NickName", "Id=@Id", dic);

            //查
            //RoleEntity entity = RoleCacheModel.Instance.GetEntity(1);
            //Console.WriteLine(entity.NickName);
            //删
            //RoleCacheModel.Instance.Delete(1);
            //======================

            Console.ReadLine();

        }

        private static void ListenClientCallBack()
        {
            while (true)
            {
                //接收客户端请求
                Socket socket = m_ServerSocket.Accept();
                Console.WriteLine("客户端{0}成功", socket.RemoteEndPoint.ToString());
                //一个角色就相当月一个客户端
                Role role = new Role();
                ClientSocket clientSocket = new ClientSocket(socket,role);
                RoleMgr.Instance.AllRole.Add(role);
            }
        }
    }
}
