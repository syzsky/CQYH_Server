using System;
using System.Collections.Concurrent;
using System.Net;
using System.Text;

namespace 游戏服务器.网络类
{
    public class Http门票接收器
    {
        private ushort port;

        private ConcurrentQueue<string> ticketQueue;

        private HttpListener listener;

        private bool isClosed;

        public Http门票接收器(ushort port, ConcurrentQueue<string> ticketQueue)
        {
            this.port = port;
            this.ticketQueue = ticketQueue;
        }

        public void Start()
        {
            string text;
            text = $"http://*:{this.port}/";
            this.listener = new HttpListener();
            try
            {
                this.listener.Prefixes.Add(text);
                this.listener.Start();
                主程.添加系统日志("Http门票接受监听端口. " + this.port);
                this.listener.BeginGetContext(OnRequest, this);
                this.isClosed = false;
            }
            catch (Exception ex)
            {
                this.listener = null;
                主程.添加系统日志("http门票接收器启动失败,监听URL : " + text + " , 错误信息:" + ex.Message);
            }
        }

        public void Stop()
        {
            this.isClosed = true;
            try
            {
                this.listener?.Stop();
            }
            catch (Exception ex)
            {
                this.listener = null;
                主程.添加系统日志("http门票接收器停止失败, 错误信息:" + ex.Message);
            }
        }

        private static void OnRequest(IAsyncResult ar)
        {
            /*
            try
            {
                Http门票接收器 http门票接收器;
                http门票接收器 = (Http门票接收器)ar.AsyncState;
                if (http门票接收器.isClosed)
                {
                    return;
                }
                HttpListenerContext httpListenerContext;
                httpListenerContext = http门票接收器.listener.EndGetContext(ar);
                IPEndPoint remoteEndPoint;
                remoteEndPoint = httpListenerContext.Request.RemoteEndPoint;
                if (remoteEndPoint.Address.ToString() != Settings.指定账号服务器IP)
                {
                    主程.添加系统日志($"非法门票IP:{remoteEndPoint.Address.ToString()}:{remoteEndPoint.Port}", hardLog: false);
                    return;
                }
                HttpListenerRequest request;
                request = httpListenerContext.Request;
                HttpListenerResponse response;
                response = httpListenerContext.Response;
                string text;
                text = request.QueryString["ticket"];
                if (text != null)
                {
                    text = Encoding.UTF8.GetString(Convert.FromBase64String(text));
                    主程.添加系统日志("Http接收到登录门票. " + text);
                    http门票接收器.ticketQueue.Enqueue(text);
                }
                response.StatusCode = 200;
                response.OutputStream.Write(Encoding.UTF8.GetBytes("OK"));
                response.Close();
                http门票接收器.listener.BeginGetContext(OnRequest, http门票接收器);
            }
            catch (Exception ex)
            {
                主程.添加系统日志("Http门票接收器异常: " + ex.Message);
            }
            */
        }

    }
}
