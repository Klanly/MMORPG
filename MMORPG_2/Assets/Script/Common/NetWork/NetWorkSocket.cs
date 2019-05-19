using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetWorkSocket : SingletonMono<NetWorkSocket> {
    
    private Socket m_Client;

    //private byte[] buffer = new byte[10240];
    #region ----- 发送消息队列 -----
    //发消息队列
    private Queue<byte[]> m_SendQueue = new Queue<byte[]>();
    //检查消息队列的委托
    private Action m_CheckSendQueue;

    //压缩标识
    private const int m_CompressLen = 200;
    #endregion

    #region ----- 接收消息所需变量
    //接收数据缓冲区数据
    private byte[] m_ReceiveBuffer = new byte[10240];
    //接收到缓冲区数据流
    MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();
    //接收消息队列
    private Queue<byte[]> m_ReceiveQueue = new Queue<byte[]>();
    private int m_ReceiveCount = 0;
    #endregion

    public Action m_OnConnectOk;
    // Use this for initialization
    protected override void OnStart()
    {
        base.OnStart();
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        base.OnUpdate();
        while (true)
        {

            if (m_ReceiveCount <= 5)
            {
                m_ReceiveCount++;
                lock (m_ReceiveQueue)
                {
                    if (m_ReceiveQueue.Count > 0)
                    {
                        //得到队列中的数据包
                        byte[] buffer = m_ReceiveQueue.Dequeue();
                        //异或后的数据
                        byte[] bufferNew = new byte[buffer.Length - 3];
                        bool isCompress = false;
                        ushort crc = 0;
                        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
                        {
                            isCompress = ms.ReadBool();
                            crc = ms.ReadUShort();
                            ms.Read(bufferNew, 0, bufferNew.Length);
                        }
                        //1.crc校验
                        int newCrc = Crc16.CalculateCrc16(bufferNew);
                        if (newCrc == crc)
                        {
                            //2.异或得到原始数据
                            bufferNew = SecurityUtil.Xor(bufferNew);
                            if (isCompress)
                                bufferNew = ZlibHelper.DeCompressBytes(bufferNew);
                            ushort protoCode = 0;
                            byte[] protoContent = new byte[bufferNew.Length - 2];
                            using (MMO_MemoryStream ms = new MMO_MemoryStream(bufferNew))
                            {
                                //协议编号
                                protoCode = ms.ReadUShort();
                                ms.Read(protoContent, 0, protoContent.Length);
                                SocketDispatcher.Instance.Dispatcher(protoCode, protoContent);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                        break;
                }
            }
            else
            {
                m_ReceiveCount = 0;
                break;
            }
        }
	}

    protected override void BeforeOnDestroy()
    {
        if (m_Client!=null && m_Client.Connected)
        {
            m_Client.Shutdown(SocketShutdown.Both);
            m_Client.Close();
        }
    }

    #region ----- Connect 连接到Socket服务器 -----
    public void ConnectByIP(string ip, int port, Action onConnectCallBack)
    {
        if (m_Client != null && m_Client.Connected) return;

        m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            m_Client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            m_CheckSendQueue = OnCheckSendQueueCallBack;
            ReceiveMsg();
            m_OnConnectOk = onConnectCallBack;
            if (m_OnConnectOk != null)
                m_OnConnectOk();
            Debuger.Log("连接成功");
        }
        catch (System.Exception ex)
        {
            Debuger.LogError("连接失败 = " + ex.Message);
        }
    }

    public void ConnectByHost(string host, int port, Action onConnectCallBack)
    {
        if (m_Client != null && m_Client.Connected) return;

        m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            m_Client.Connect(host, port);
            m_CheckSendQueue = OnCheckSendQueueCallBack;
            ReceiveMsg();
            m_OnConnectOk = onConnectCallBack;
            if (m_OnConnectOk != null)
                m_OnConnectOk();
            Debuger.Log("连接成功");
        }
        catch (System.Exception ex)
        {
            Debuger.LogError("连接失败 = " + ex.Message);
        }
    }

    #endregion

    #region ----- OnCheckSendQueueCallBack 检查消息队列的委托回调 -----
    /// <summary> 检查消息队列的委托回调</summary>
    private void OnCheckSendQueueCallBack()
    {
        lock (m_SendQueue)
        {
            if (m_SendQueue.Count>0)
            {
                Send(m_SendQueue.Dequeue());
            }
        }
    }
    #endregion

    #region ----- MarkData 封装数据包 -----
    /// <summary> 封装数据包 </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private byte[] MarkData(byte[] data)
    {
        byte[] retBuffer = null;
        //1.如果数据包的长度大于m_CompressLen 则进行压缩
        bool isCompress = data.Length > m_CompressLen ? true : false;
        if (isCompress)
        {
            data = ZlibHelper.CompressBytes(data);
        }
        //2.异或
        data = SecurityUtil.Xor(data);
        //3.Crc校验 压缩后的
        ushort crc = Crc16.CalculateCrc16(data);
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort((ushort)(data.Length+3));
            ms.WriteBool(isCompress);
            ms.WriteUShort(crc);
            ms.Write(data, 0, data.Length);
            retBuffer = ms.ToArray();
        }
        return retBuffer;
    }
    #endregion

    #region ----- SendMsg 发送消息  把消息加入队列-----
    public void SendMsg(byte[] buffer)
    {
        //得到封装后的包
        byte[] sendBuffer = MarkData(buffer);
        lock (m_SendQueue)
        {
            //把数据包添加到发送队列
            m_SendQueue.Enqueue(sendBuffer);
            //执行委托
            m_CheckSendQueue.BeginInvoke(null, null);
        }
    }
    #endregion

    #region ----- Send 发送消息 发送数据包到服务器 -----

    /// <summary> 发送消息 发送数据包到服务器 </summary>
    /// <param name="buffer"></param>
    private void Send(byte[] buffer)
    {
        m_Client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallBack, m_Client);
    }
    #endregion

    #region ----- SendCallBack 发送数据包回调 -----

    /// <summary> 发送数据包回调 </summary>
    /// <param name="ar"></param>
    private void SendCallBack(IAsyncResult ar)
    {
        m_Client.EndSend(ar);
        //继续检查队列
        OnCheckSendQueueCallBack();
    }
    #endregion

    //============================================================================


    #region ----- 接收数据 -----
    /// <summary> 接收数据 </summary>
    private void ReceiveMsg()
    {
        //异步接收数据
        m_Client.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallBack, m_Client);
    }
    #endregion

    #region ------ 接收数据回调 -----
    /// <summary> 接收数据回调 </summary>
    /// <param name="ar"></param>
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            int len = m_Client.EndReceive(ar);
            if (len > 0)
            {
                //把接收到的数据写入缓冲数据流的尾部
                m_ReceiveMS.Position = m_ReceiveMS.Length;
                //把制定长度的字节写入数据流
                m_ReceiveMS.Write(m_ReceiveBuffer, 0, len);
                //如果缓存数据流的长度>2,说明至少有一个不完整的包
                //客户端封装数据包的时候用的ushort 长度为2
                if (m_ReceiveMS.Length > 2)
                {
                    //循环拆分数据包
                    while (true)
                    {
                        //把数据流指针位置放在0处
                        m_ReceiveMS.Position = 0;
                        //currMsgLen = 包体的长度
                        int currMsgLen = m_ReceiveMS.ReadUShort();
                        int currFullMsgLen = 2 + currMsgLen;//整包的长度
                        //如果数据流长度>=整包的长度 说明至少收到了一个完整包
                        if (m_ReceiveMS.Length >= currFullMsgLen)
                        {
                            //进行拆包
                            byte[] buffer = new byte[currMsgLen];
                            //将数据指针设置到包体位置
                            m_ReceiveMS.Position = 2;
                            //把包体读到byte[]数组
                            m_ReceiveMS.Read(buffer, 0, currMsgLen);

                            lock (m_ReceiveQueue)
                            {
                                //buffer就是服务端发来的一条完整数据
                                m_ReceiveQueue.Enqueue(buffer);
                            }
                            //=============处理剩余字节数组==============
                            int remainLen = (int)m_ReceiveMS.Length - currFullMsgLen;
                            if (remainLen > 0)
                            {
                                //把指针放在第一个包的尾部
                                m_ReceiveMS.Position = currFullMsgLen;
                                byte[] remainBuffer = new byte[remainLen];
                                m_ReceiveMS.Read(remainBuffer, 0, remainLen);
                                //清空数据流
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);
                                //吧数据字节重新写入数据流
                                m_ReceiveMS.Write(remainBuffer, 0, remainBuffer.Length);
                                remainBuffer = null;
                            }
                            else
                            {
                                //没有剩余字节
                                //清空数据流
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);
                                break;
                            }
                        }
                        else
                        {
                            //还没收到完整包
                            break;
                        }
                    }
                }
                //进行下一次接收数据
                ReceiveMsg();
            }
            else
            {
                Debug.Log(string.Format("服务端{0}断开连接", m_Client.RemoteEndPoint.ToString()));
            }
        }
        catch
        {
            Debug.Log(string.Format("服务端{0}断开连接", m_Client.RemoteEndPoint.ToString()));
        }
    }
    #endregion

}
