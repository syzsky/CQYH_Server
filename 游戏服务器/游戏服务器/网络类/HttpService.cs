using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace 游戏服务器.网络类
{
    internal abstract class HttpService
    {
        protected string Host;

        private HttpListener 监听端口;

        private bool _000C_0008_000D_0007_0007_0008_0008_0005 = true;

        private DateTime _0010_000B_0003_0012_0002_0005_0013_0002;

        private HttpListenerResponse _0006_0007_0009_0003_0009_0003;

        public static ConcurrentQueue<WebReturnData> WebReturn = new ConcurrentQueue<WebReturnData>();

        protected HttpListenerResponse waitResponse
        {
            get
            {
                return this._0006_0007_0009_0003_0009_0003;
            }
            set
            {
                this._0006_0007_0009_0003_0009_0003 = value;
                this._0010_000B_0003_0012_0002_0005_0013_0002 = DateTime.Now.AddSeconds(2.0);
            }
        }

        public static void Return(HttpListenerResponse Respons, string Message)
        {
            HttpService.WebReturn.Enqueue(new WebReturnData
            {
                Respons = Respons,
                Message = Message
            });
        }

        public void Listen()
        {
            if (!HttpListener.IsSupported)
            {
                throw new InvalidOperationException("To use HttpListener the operating system must be Windows XP SP2 or Server 2003 or higher.");
            }
            string[] array;
            array = new string[1] { this.Host };
            this.监听端口 = new HttpListener();
            try
            {
                string[] array2;
                array2 = array;
                foreach (string uriPrefix in array2)
                {
                    this.监听端口.Prefixes.Add(uriPrefix);
                }
                this.监听端口.Start();
                主程.添加系统日志("Http服务已启动...");
            }
            catch (Exception ex)
            {
                主程.添加系统日志("Http服务启动失败! 错误:" + ex);
                return;
            }
            while (this._000C_0008_000D_0007_0007_0008_0008_0005)
            {
                try
                {
                    if (this.waitResponse != null)
                    {
                        continue;
                    }
                    HttpListenerContext context;
                    context = this.监听端口.GetContext();
                    HttpListenerRequest request;
                    request = context.Request;
                    Console.WriteLine("{0} {1} HTTP/1.1", request.HttpMethod, request.RawUrl);
                    Console.WriteLine("User-Agent: {0}", request.UserAgent);
                    Console.WriteLine("Accept-Encoding: {0}", request.Headers["Accept-Encoding"]);
                    Console.WriteLine("Connection: {0}", request.KeepAlive ? "Keep-Alive" : "close");
                    Console.WriteLine("Host: {0}", request.UserHostName);
                    HttpListenerResponse response;
                    response = context.Response;
                    if (context.Request.RemoteEndPoint != null)
                    {
                        context.Request.RemoteEndPoint.Address.ToString();
                    }
                    if (request.HttpMethod == "GET")
                    {
                        this.OnGetRequest(request, response);
                    }
                    else
                    {
                        this.OnPostRequest(request, response);
                    }
                    while (this.waitResponse != null)
                    {
                        this.ProcessReturn();
                        if (DateTime.Now > this._0010_000B_0003_0012_0002_0005_0013_0002)
                        {
                            break;
                        }
                    }
                    Thread.Sleep(1);
                }
                catch
                {
                }
            }
            Thread.Sleep(1);
        }

        public void ProcessReturn()
        {
            if (!HttpService.WebReturn.IsEmpty && HttpService.WebReturn.TryDequeue(out var result))
            {
                if (this.waitResponse == result.Respons)
                {
                    this.waitResponse = null;
                }
                this.WriteResponse(result.Respons, result.Message);
            }
        }

        public void Stop()
        {
            this._000C_0008_000D_0007_0007_0008_0008_0005 = false;
            if (this.监听端口 != null && this.监听端口.IsListening)
            {
                this.监听端口.Stop();
                主程.添加系统日志("Http服务已停止");
            }
        }

        public abstract void OnGetRequest(HttpListenerRequest request, HttpListenerResponse response);

        public abstract void OnPostRequest(HttpListenerRequest request, HttpListenerResponse response);

        public void WriteResponse(HttpListenerResponse response, string responseString)
        {
            try
            {
                response.ContentLength64 = Encoding.UTF8.GetByteCount(responseString);
                response.ContentType = "text/html; charset=UTF-8";
            }
            finally
            {
                StreamWriter streamWriter;
                streamWriter = new StreamWriter(response.OutputStream);
                streamWriter.Write(responseString);
                streamWriter.Close();
                response.Close();
            }
        }
    }
}
