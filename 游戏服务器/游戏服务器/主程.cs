using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoBattle;
using _001D_000F_0007_0013_0011_0015;
using 游戏服务器.地图类;
using 游戏服务器.管理命令;
using 游戏服务器.模板类;
using 游戏服务器.数据类;
using 游戏服务器.网络类;
using Newtonsoft.Json;

namespace 游戏服务器
{
    public static class 主程
    {
        public class 创角请求
        {
            public string 账号;

            public string 推荐人;

            public string 名字;

            public 游戏对象职业 职业;

            public 游戏对象性别 性别;

            public 对象发色分类 发色;

            public 对象发型分类 发型;

            public 对象脸型分类 脸型;

            public bool 成功;

            public override string ToString()
            {
                return $"{this.账号},{this.推荐人},{this.名字},{this.职业},{this.性别},{this.发色},{this.发型},{this.脸型}";
            }
        }

        public sealed class 创角返回
        {
            public int code;

            public string message;

            public int timestamp;

            public string traceID;
        }

        public static DateTime 上次保存时间;

        public static DateTime 当前时间;

        public static DateTime 每秒计时;

        public static ConcurrentQueue<GM命令> 外部命令;

        public static uint 循环计数;

        public static bool 已经启动;

        public static bool 正在保存;

        public static Thread 主线程;

        public static Random 随机数;

        public static ConcurrentQueue<Action> 重载任务列表;

        public static DateTime 下次重载任务时间;

        public static Dictionary<int, NPCScript> Scripts;

        public static int ScriptIndex;

        public static NPCScript DefaultNPC;

        public static NPCScript MonsterNPC;

        public static NPCScript RobotNPC;

        public static int DefaultNPCID;

        public static DateTime 每日执行时间;

        public static ConcurrentQueue<string> DisplayLogs;

        public static ConcurrentQueue<string> Logs;

        public static bool UseLogConsole;

        public static bool OldForm;

        public static ConcurrentQueue<string> DisplayChatLogs;

        public static ConcurrentQueue<string> ChatLogs;

        public static ConcurrentQueue<string> GameLogs;

        public static ConcurrentQueue<string> ItemLogs;

        public static ConcurrentQueue<string> CurrencyLogs;

        public static ConcurrentQueue<string> WebLogs;

        private static ConcurrentQueue<LogData> WebLogDatas;

        public static ConcurrentQueue<string> DisplayCommandLogs;

        public static ConcurrentQueue<创角请求> 创角请求列表;

        public static ConcurrentQueue<创角请求> 创角返回列表;

        private static HttpClient Http;

        public static string LogWebSite;

        public static ConcurrentQueue<WebData> WebEvent;

        public static bool 自动保存中;

        public static int 开区天数 => 计算类.日期转换(主程.当前时间) - 计算类.日期转换(Settings.开服日期);

        public static int 开区节点
        {
            get
            {
                if (主程.开区天数 >= Settings.龙耀雪山节点开放天数)
                {
                    return 7;
                }
                if (主程.开区天数 >= Settings.苍月惊变节点开放天数)
                {
                    return 6;
                }
                if (主程.开区天数 >= Settings.魔龙之城节点开放天数)
                {
                    return 4;
                }
                if (主程.开区天数 >= Settings.白日赤月节点开放天数)
                {
                    return 3;
                }
                if (主程.开区天数 >= Settings.幽冥海底节点开放天数)
                {
                    return 1;
                }
                return 0;
            }
        }

        public static void 添加系统日志(string log, bool hardLog = true, bool showDiag = true)
        {
            if (主程.OldForm)
            {
                主窗口.添加系统日志(log);
            }
            if (主程.UseLogConsole)
            {
                log = $"[{主程.当前时间:F}]: {log}";
                Console.WriteLine(log);
                return;
            }
            log = $"[{主程.当前时间:F}]: {log}";
            if (主程.DisplayLogs.Count < 100 && showDiag)
            {
                主程.DisplayLogs.Enqueue(log);
            }
            if (hardLog && 主程.Logs.Count < 1000)
            {
                主程.Logs.Enqueue(log);
            }
        }

        public static void 添加聊天日志(string 前缀, byte[] 内容)
        {
            if (主程.OldForm)
            {
                主窗口.添加聊天日志(前缀, 内容);
                return;
            }
            string item;
            item = $"[{主程.当前时间:F}]: {前缀 + Encoding.UTF8.GetString(内容).Trim('\0')}";
            if (主程.DisplayChatLogs.Count < 500)
            {
                主程.DisplayChatLogs.Enqueue(item);
            }
            if (主程.ChatLogs.Count < 1000)
            {
                主程.ChatLogs.Enqueue(item);
            }
        }

        public static void 添加命令日志(string 文本)
        {
            if (主程.OldForm)
            {
                主窗口.添加系统日志(文本);
                return;
            }
            文本 = $"[{主程.当前时间:F}]: {文本}";
            if (主程.DisplayCommandLogs.Count < 500)
            {
                主程.DisplayCommandLogs.Enqueue(文本);
            }
            主程.Logs.Enqueue(文本);
        }

        internal static void WriteLogs()
        {
            string text;
            text = Settings.游戏数据目录 + "\\Log";
            List<string> list;
            list = new List<string>();
            while (!主程.Logs.IsEmpty)
            {
                if (主程.Logs.TryDequeue(out var result))
                {
                    list.Add(result);
                }
            }
            if (!Directory.Exists(text + "\\SystemLog"))
            {
                Directory.CreateDirectory(text + "\\SystemLog");
            }
            if (list.Count > 0)
            {
                File.AppendAllLines($"{text}\\SystemLog\\{DateTime.Now:yyyy-MM-dd HH 00 00}.txt", list);
            }
            list.Clear();
            while (!主程.ChatLogs.IsEmpty)
            {
                if (主程.ChatLogs.TryDequeue(out var result2))
                {
                    list.Add(result2);
                }
            }
            if (!Directory.Exists(text + "\\ChatLogs"))
            {
                Directory.CreateDirectory(text + "\\ChatLogs");
            }
            if (list.Count > 0)
            {
                File.AppendAllLines($"{text}\\ChatLogs\\{DateTime.Now:yyyy-MM-dd HH 00 00}.txt", list);
            }
            list.Clear();
            while (!主程.GameLogs.IsEmpty)
            {
                if (主程.GameLogs.TryDequeue(out var result3))
                {
                    list.Add(result3);
                }
            }
            if (!Directory.Exists(text + "\\GameLogs"))
            {
                Directory.CreateDirectory(text + "\\GameLogs");
            }
            if (list.Count > 0)
            {
                File.AppendAllLines($"{text}\\GameLogs\\{DateTime.Now:yyyy-MM-dd HH 00 00}.txt", list);
            }
            list.Clear();
            while (!主程.ItemLogs.IsEmpty)
            {
                if (主程.ItemLogs.TryDequeue(out var result4))
                {
                    list.Add(result4);
                }
            }
            if (!Directory.Exists(text + "\\ItemLogs"))
            {
                Directory.CreateDirectory(text + "\\ItemLogs");
            }
            if (list.Count > 0)
            {
                File.AppendAllLines($"{text}\\ItemLogs\\{DateTime.Now:yyyy-MM-dd HH 00 00}.txt", list);
            }
            list.Clear();
            while (!主程.CurrencyLogs.IsEmpty)
            {
                if (主程.CurrencyLogs.TryDequeue(out var result5))
                {
                    list.Add(result5);
                }
            }
            if (!Directory.Exists(text + "\\CurrencyLogs"))
            {
                Directory.CreateDirectory(text + "\\CurrencyLogs");
            }
            if (list.Count > 0)
            {
                File.AppendAllLines($"{text}\\CurrencyLogs\\{DateTime.Now:yyyy-MM-dd HH 00 00}.txt", list);
            }
            list.Clear();
            /*
            while (!主程.WebLogs.IsEmpty)
            {
                if (主程.WebLogs.TryDequeue(out var result6))
                {
                    list.Add(result6);
                }
            }
            
            if (!Directory.Exists(text + "\\WebLogs"))
            {
                Directory.CreateDirectory(text + "\\WebLogs");
            }
            
            if (list.Count > 0)
            {
                File.AppendAllLines($"{text}\\WebLogs\\{DateTime.Now:yyyy-MM-dd HH 00 00}.txt", list);
            }
            */
            list.Clear();
            list.Clear();
        }

        public static void 请求创建角色(创角请求 Q)
        {
            主程.创角请求列表.Enqueue(Q);
        }
        /*
        public static void 返回创建角色(创角请求 Q)
        {
            主程.创角返回列表.Enqueue(Q);
        }
        
        public static int HttpPost2(string url, string sendData, out string reslut)
        {
            reslut = "";
            try
            {
                byte[] bytes;
                bytes = Encoding.UTF8.GetBytes(sendData);
                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Proxy = null;
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = bytes.Length;
                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                using Stream stream2 = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream();
                using StreamReader streamReader = new StreamReader(stream2, Encoding.UTF8);
                reslut = streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                reslut = ex.Message;
                return -1;
            }
            return 0;
        }

        public static async Task<string> HttpPostAsync(string url, string sendData)
        {
            _ = string.Empty;
            string result;
            try
            {
                Encoding.UTF8.GetBytes(sendData);
                if (主程.Http == null)
                {
                    主程.Http = new HttpClient
                    {
                        Timeout = TimeSpan.FromSeconds(30.0)
                    };
                }
                HttpResponseMessage obj;
                obj = await 主程.Http.PostAsync(url, new StringContent(sendData));
                obj.EnsureSuccessStatusCode();
                result = await obj.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result;
        }
        */
        //public static async Task 处理创建角色请求()
        public static Task 处理创建角色请求() //async
        {
            if (创角请求列表.IsEmpty)
            {
                Thread.Sleep(500);
                return Task.CompletedTask;
            }
            string result;
            result = "";
            try
            {
                if (创角请求列表.TryDequeue(out var Q) && Q != null)
                {
                    添加系统日志($"创建角色请求:{Q}", hardLog: true, showDiag: false);
                    Q.成功 = true;
                    /*
                    result = await 主程.HttpPostAsync("https://pay.tengcanol.com/admin/site/clientRegister", JsonConvert.SerializeObject(new
                    {
                        inviteCode = Q.推荐人,
                        account = Q.账号,
                        areaname = Settings.游戏区服名称
                    }));
                    */
                    添加系统日志("创建角色请求:clientRegister = " + result, hardLog: true, showDiag: false);
                    /*
                    result = await 主程.HttpPostAsync("https://pay.tengcanol.com/admin/site/clientRegisterRole", JsonConvert.SerializeObject(new
                    {
                        uuid = Settings.统计UUID代码,
                        areaname = Settings.游戏区服名称,
                        account = Q.账号,
                        roleName = Q.名字
                    }));
                    */
                    添加系统日志("创建角色请求:clientRegisterRole = " + result, hardLog: true, showDiag: false);
                    /*
                    创角返回 创角返回;
                    
                    创角返回 = JsonConvert.DeserializeObject<创角返回>(result, new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.Auto,
                        Formatting = Formatting.Indented
                    });
                    
                    添加系统日志($"创建角色请求:rc.code = {创角返回.code}", hardLog: true, showDiag: false);
                    */
                }
            }
            catch (Exception ex)
            {
                添加系统日志("创建角色请求 Error:" + result + " " + ex.Message + "\r\n" + ex.StackTrace);
            }

            return Task.CompletedTask;
        }

        public static void 返回客户端创建角色()
        {
        }

        private static async Task WebPostLoop()
        {
            while (主程.已经启动)
            {
                await 主程.处理创建角色请求();
            }
        }

        private static void WriteLogsLoop()
        {
            DateTime dateTime;
            dateTime = 主程.当前时间.AddSeconds(10.0);
            DateTime dateTime2;
            dateTime2 = 主程.当前时间.AddMinutes(10.0);
            while (主程.已经启动)
            {
                //主程.ProcessWebLog();
                if (主程.当前时间 > dateTime2)
                {
                    long num;
                    num = 0L;
                    long num2;
                    num2 = 0L;
                    long num3;
                    num3 = 0L;
                    long num4;
                    num4 = 0L;
                    long num5;
                    num5 = 0L;
                    long num6;
                    num6 = 0L;
                    foreach (KeyValuePair<int, 游戏数据> item in 游戏数据网关.角色数据表.数据表)
                    {
                        角色数据 角色数据;
                        角色数据 = item.Value as 角色数据;
                        if (角色数据.商人角色.V)
                        {
                            num4 += 角色数据.金币数量;
                            num6 += 角色数据.银币数量;
                        }
                        else
                        {
                            num += 角色数据.金币数量;
                            num3 += 角色数据.银币数量;
                        }
                    }
                    foreach (KeyValuePair<int, 游戏数据> item2 in 游戏数据网关.账号数据表.数据表)
                    {
                        账号数据 账号数据;
                        账号数据 = item2.Value as 账号数据;
                        if (账号数据.角色列表.FirstOrDefault((角色数据 x) => x.商人角色.V) != null)
                        {
                            num5 += 账号数据.元宝数量.V;
                        }
                        else
                        {
                            num2 += 账号数据.元宝数量.V;
                        }
                    }
                    uint 已登录连接数;
                    已登录连接数 = 网络服务网关.已登录连接数;
                    int num7;
                    num7 = 游戏数据网关.账号数据表.数据表.Values.Select((游戏数据 x) => (x as 账号数据).网络连接?.物理地址).Distinct().Count();
                    //主程.WebLog(LogDataType.ProxyDataUp, Settings.统计UUID代码, Settings.游戏区服名称, 已登录连接数.ToString(), num7.ToString(), num2.ToString(), num.ToString(), num3.ToString(), num5.ToString(), num4.ToString(), num6.ToString());
                    dateTime2 = 主程.当前时间.AddMinutes(10.0);
                }
                if (主程.当前时间 < dateTime)
                {
                    Thread.Sleep(1);
                    continue;
                }

                try
                {
                    主程.WriteLogs();
                }
                catch (Exception ex)
                {
                    主程.添加系统日志("WriteLogs Error:" + ex.Message + "\r\n" + ex.StackTrace);
                }

                dateTime = 主程.当前时间.AddSeconds(10.0);

            }
        }
        /*
        public static void ProcessWebLog()
        {
            if (!主程.WebLogDatas.IsEmpty && 主程.WebLogDatas.TryDequeue(out var result))
            {
                int num;
                num = 0;
                string reslut;
                if (result is OutputLog value)
                {
                    num = 主程.HttpPost("base/OutputLog", JsonConvert.SerializeObject(value), out reslut);
                }
                else if (result is ProxyDataUp value2)
                {
                    num = 主程.HttpPost("base/ProxyDataUp", JsonConvert.SerializeObject(value2), out reslut);
                }
                else if (result is CommandSendingLog value3)
                {
                    num = 主程.HttpPost("base/CommandSendingLog", JsonConvert.SerializeObject(value3), out reslut);
                }
                else if (result is MerchantLog value4)
                {
                    num = 主程.HttpPost("base/MerchantLog", JsonConvert.SerializeObject(value4), out reslut);
                }
                else if (result is WebsiteRechargeLog value5)
                {
                    num = 主程.HttpPost("base/WebsiteRechargeLog", JsonConvert.SerializeObject(value5), out reslut);
                }
                else if (result is ConsumptionLog value6)
                {
                    num = 主程.HttpPost("base/ConsumptionLog", JsonConvert.SerializeObject(value6), out reslut);
                }
                else if (result is MerchantRecyLog value7)
                {
                    num = 主程.HttpPost("base/MerchantRecyLog", JsonConvert.SerializeObject(value7), out reslut);
                }
                else if (result is ScriptLog value8)
                {
                    num = 主程.HttpPost("base/ScriptLog", JsonConvert.SerializeObject(value8), out reslut);
                }
                else if (result is MakederLog value9)
                {
                    num = 主程.HttpPost("base/MakederLog", JsonConvert.SerializeObject(value9), out reslut);
                }
                else if (result is CreateGameRole value10)
                {
                    num = 主程.HttpPost("base/CreateGameRole", JsonConvert.SerializeObject(value10), out reslut);
                }
                else if (result is LoginLogoutLog value11)
                {
                    num = 主程.HttpPost("base/ClientLoginout", JsonConvert.SerializeObject(value11), out reslut);
                }
                if (num != 0 && result.retry < 10)
                {
                    result.retry++;
                    主程.WebLogDatas.Enqueue(result);
                }
            }
        }

        public static void WebLog(LogDataType type, params string[] param)
        {
            LogData logData;
            logData = null;
            uint result;
            result = 0u;
            int result2;
            result2 = 0;
            long result3;
            result3 = 0L;
            主程.WebLogs.Enqueue("[" + type.ToString() + "] " + string.Join(",", param));
            switch (type)
            {
                case LogDataType.OutputLog:
                    logData = new OutputLog
                    {
                        uuid = param[0],
                        areaName = param[1],
                        playName = param[2],
                        playActive = param[3],
                        ccNumber = (uint.TryParse(param[4], out result) ? result : 0u),
                        remake = param[5],
                        typeMoney = param[6]
                    };
                    break;
                case LogDataType.ProxyDataUp:
                    logData = new ProxyDataUp
                    {
                        uuid = param[0],
                        areaName = param[1],
                        userNumbers = (uint.TryParse(param[2], out result) ? result : 0u),
                        deviceNumber = (uint.TryParse(param[3], out result) ? result : 0u),
                        ybNumber = (long.TryParse(param[4], out result3) ? result3 : 0L),
                        jbNumber = (long.TryParse(param[5], out result3) ? result3 : 0L),
                        yinbNumber = (long.TryParse(param[6], out result3) ? result3 : 0L),
                        srYbNumber = (long.TryParse(param[7], out result3) ? result3 : 0L),
                        srJbNumber = (long.TryParse(param[8], out result3) ? result3 : 0L),
                        srYinbNumber = (long.TryParse(param[9], out result3) ? result3 : 0L)
                    };
                    break;
                case LogDataType.CommandSendingLog:
                    logData = new CommandSendingLog
                    {
                        uuid = param[0],
                        areaName = param[1],
                        formUser = param[2],
                        playName = param[3],
                        playActive = param[4],
                        playNum = (uint.TryParse(param[5], out result) ? result : 0u),
                        typeMoney = param[6]
                    };
                    break;
                case LogDataType.MerchantLog:
                    logData = new MerchantLog
                    {
                        uuid = param[0],
                        areaName = param[1],
                        formUser = param[2],
                        playName = param[3],
                        playActive = param[4],
                        playNum = (uint.TryParse(param[5], out result) ? result : 0u),
                        syNumbers = (uint.TryParse(param[6], out result) ? result : 0u),
                        typeMoney = param[7]
                    };
                    break;
                case LogDataType.WebsiteRechargeLog:
                    logData = new WebsiteRechargeLog
                    {
                        uuid = param[0],
                        areaName = param[1],
                        formUser = param[2],
                        playName = param[3],
                        playActive = param[4],
                        playNum = (uint.TryParse(param[5], out result) ? result : 0u),
                        formMoney = (uint.TryParse(param[6], out result) ? result : 0u),
                        typeMoney = param[7]
                    };
                    break;
                case LogDataType.ConsumptionLog:
                    logData = new ConsumptionLog
                    {
                        uuid = param[0],
                        areaName = param[1],
                        playName = param[2],
                        playActive = param[3],
                        xhNum = (int.TryParse(param[4], out result2) ? result2 : 0),
                        remake = param[5],
                        typeMoney = param[6]
                    };
                    break;
                case LogDataType.MerchantRecyLog:
                    logData = new MerchantRecyLog
                    {
                        uuid = param[0],
                        areaName = param[1],
                        merchantName = param[2],
                        playName = param[3],
                        playActive = param[4],
                        recyNum = (uint.TryParse(param[5], out result) ? result : 0u),
                        recyResiduNum = (uint.TryParse(param[6], out result) ? result : 0u),
                        typeMoney = param[7]
                    };
                    break;
                case LogDataType.MakederLog:
                    logData = new MakederLog
                    {
                        uuid = param[0],
                        areaName = param[1],
                        makeName = param[2],
                        makeGoodName = param[3],
                        makeNum = (uint.TryParse(param[4], out result) ? result : 0u)
                    };
                    break;
                case LogDataType.CreateGameRole:
                    logData = new CreateGameRole
                    {
                        uuid = param[0],
                        areaName = param[1],
                        invitationCode = param[2],
                        gameAccount = param[3],
                        roleName = param[4]
                    };
                    break;
                case LogDataType.LoginLog:
                    logData = new LoginLogoutLog
                    {
                        uuid = param[0],
                        userAcount = param[1],
                        si = param[2],
                        bt = 1
                    };
                    break;
                case LogDataType.OfflineLog:
                    logData = new LoginLogoutLog
                    {
                        uuid = param[0],
                        userAcount = param[1],
                        si = param[2],
                        bt = 0
                    };
                    break;
                case LogDataType.ScriptLog:
                    logData = new ScriptLog
                    {
                        uuid = param[0],
                        areaName = param[1],
                        formUser = param[2],
                        playName = param[3],
                        playActive = param[4],
                        playNum = (uint.TryParse(param[5], out result) ? result : 0u),
                        typeMoney = param[6]
                    };
                    break;
            }
            if (logData != null)
            {
                主程.WebLogDatas.Enqueue(logData);
            }
        }

        public static int HttpPost(string url, string sendData, out string reslut)
        {
            reslut = "";
            try
            {
                byte[] bytes;
                bytes = Encoding.UTF8.GetBytes(sendData);
                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)WebRequest.Create(主程.LogWebSite + url);
                httpWebRequest.Proxy = null;
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = bytes.Length;
                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                using Stream stream2 = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream();
                using StreamReader streamReader = new StreamReader(stream2, Encoding.UTF8);
                reslut = streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                reslut = ex.Message;
                return -1;
            }
            return 0;
        }

        public static int HttpPostYY(string sendData, out string reslut)
        {
            string requestUriString;
            requestUriString = "https://pay.tengcanol.com/admin/site/gameRechargeTemp";
            reslut = "";
            try
            {
                byte[] bytes;
                bytes = Encoding.UTF8.GetBytes(sendData);
                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
                httpWebRequest.Proxy = null;
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = bytes.Length;
                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                using Stream stream2 = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream();
                using StreamReader streamReader = new StreamReader(stream2, Encoding.UTF8);
                reslut = streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                reslut = ex.Message;
                return -1;
            }
            return 0;
        }

        public static void AddWebEvent(WebDataType type, Dictionary<string, string> data, HttpListenerResponse respons)
        {
            主程.WebEvent.Enqueue(new WebData
            {
                Type = type,
                Data = data,
                Respons = respons
            });
        }
        */

        public static void 启动服务()
        {
            主程.DefaultNPCID = 主程.随机数.Next(1000000, 1999999);
            主程.DefaultNPC = NPCScript.GetOrAdd(主程.DefaultNPCID, Settings.DefaultNPCFilename, NPCScriptType.AutoPlayer);
            主程.上次保存时间 = DateTime.Now;
            if (!主程.已经启动)
            {
                Thread obj;
                obj = new Thread(服务循环)
                {
                    IsBackground = true
                };
                主程.主线程 = obj;
                obj.Start();
            }
        }

        public static void 添加重铸日志(角色数据 角色数据, 装备数据 关联物品, 列表监视器<随机属性> 随机属性)
        {
            foreach (随机属性 item2 in 随机属性)
            {
                string item;
                item = $"{主程.当前时间:F} {角色数据.角色名字.V}\t玩家洗练装备\t{关联物品.物品名字}\t{item2.属性编号.ToString()}\t{item2.属性数值.ToString()}\t{item2.属性描述}";
                主程.GameLogs.Enqueue(item);
            }
        }

        public static void 添加物品日志(玩家实例 玩家对象, string 动作名称, 物品数据 关联物品, int 物品数量, string remark = null)
        {
            主程.添加物品日志(玩家对象.角色数据, 动作名称, 关联物品, 物品数量, remark);
        }

        public static void 添加物品日志(角色数据 角色数据, string 动作名称, 物品数据 关联物品, int 物品数量, string remark = null)
        {
            string item;
            item = $"{主程.当前时间:F} {角色数据.角色名字.V}\t{动作名称.ToString()}\t{关联物品.物品名字}\t{关联物品.物品编号}\t{关联物品.数据索引}\t{物品数量.ToString()}\t{((!关联物品.能否堆叠) ? 1 : 关联物品.当前持久.V)}\t{remark}";
            主程.ItemLogs.Enqueue(item);
        }

        public static void 添加货币日志(玩家实例 玩家对象, string 动作名称, 游戏货币 货币名字, uint 货币数量)
        {
            主程.添加货币日志(玩家对象.角色数据, 动作名称, 货币名字, (int)货币数量);
        }

        public static void 添加货币日志(玩家实例 玩家对象, string 动作名称, 游戏货币 货币名字, int 货币数量)
        {
            主程.添加货币日志(玩家对象.角色数据, 动作名称, 货币名字, 货币数量);
        }

        public static void 添加货币日志(角色数据 角色数据, string 动作名称, 游戏货币 货币名字, uint 货币数量)
        {
            主程.添加货币日志(角色数据, 动作名称, 货币名字, (int)货币数量);
        }

        public static void 添加货币日志(角色数据 角色数据, string 动作名称, 游戏货币 货币名字, int 货币数量)
        {
            uint num;
            num = 0u;
            uint v;
            if (货币名字 == 游戏货币.元宝)
            {
                num = 角色数据.元宝数量;
            }
            else if (角色数据.角色货币.TryGetValue(货币名字, out v))
            {
                num = v;
            }
            string item;
            item = $"{主程.当前时间:F} {角色数据.角色名字.V}\t{动作名称}\t{货币名字.ToString()}\t{货币数量.ToString()}\t{num.ToString()}";
            主程.CurrencyLogs.Enqueue(item);
            if (货币数量 > 0)
            {
                //主程.WebLog(LogDataType.OutputLog, Settings.统计UUID代码, Settings.游戏区服名称, 角色数据.角色名字.V, 角色数据.所属账号.V.账号名字.V, 货币数量.ToString(), 动作名称, 货币名字.ToString());
            }
            if (货币数量 < 0)
            {
                //主程.WebLog(LogDataType.ConsumptionLog, Settings.统计UUID代码, Settings.游戏区服名称, 角色数据.角色名字.V, 角色数据.所属账号.V.账号名字.V, (-货币数量).ToString(), 动作名称, 货币名字.ToString());
            }
        }

        public static void 停止服务()
        {
            主程.已经启动 = false;
            网络服务网关.结束服务();
        }

        public static void 保存数据库()
        {
            if (主程.自动保存中)
            {
                return;
            }
            主程.自动保存中 = true;
            Task.Run(delegate
            {
                Stopwatch stopwatch;
                stopwatch = Stopwatch.StartNew();
                try
                {
                    主程.添加系统日志("正在保存客户数据到磁盘...");
                    游戏数据网关.导出数据();
                    stopwatch.Stop();
                    主程.添加系统日志($"客户数据保存完毕 , 耗时:{stopwatch.ElapsedMilliseconds} 线程ID:{Thread.CurrentThread.ManagedThreadId}");
                }
                catch (Exception ex)
                {
                    主程.添加系统日志($"客户数据保存异常 , 耗时:{stopwatch.ElapsedMilliseconds} 线程ID:{Thread.CurrentThread.ManagedThreadId} e:{ex.Message} {((ex.InnerException != null) ? (".IE:" + ex.InnerException.Message) : "...")}");
                }
                主程.自动保存中 = false;
            });
        }

        private static void 服务循环()
        {
            主程.外部命令 = new ConcurrentQueue<GM命令>();
            主程.添加系统日志("正在生成地图元素...");
            地图处理网关.开启地图();
            主程.添加系统日志("正在启动网络服务...");
            网络服务网关.启动服务();
            主程.添加系统日志("服务器已成功开启");
            主程.添加系统日志($"逻辑线程ID: {Thread.CurrentThread.ManagedThreadId}");
            主程.已经启动 = true;
            主窗口.服务启动回调();
            主程.每日执行时间 = 主程.当前时间;
            Thread thread;
            thread = new Thread(WriteLogsLoop);
            thread.IsBackground = true;
            thread.Start();

            Task.Run(async () =>
            {
                await 主程.WebPostLoop();
            });

            AutoBattleManager.Start();

            机器人.初始化();
            while (主程.已经启动 || 网络服务网关.网络连接表.Count != 0)
            {
                try
                {
                    Thread.Sleep(1);
                    主程.当前时间 = DateTime.Now;
                    if (主窗口.暂停界面更新)
                    {
                        continue;
                    }
                    if (主程.当前时间 > 主程.每秒计时)
                    {
                        if (!主程.自动保存中)
                        {
                            游戏数据网关.保存数据();
                        }
                        SMain.UpdateDelay(地图处理网关.激活对象表.Count, 地图处理网关.次要对象表.Count, 地图处理网关.地图对象表.Count);
                        SMain.UpdateTick(主程.循环计数);
                        主程.循环计数 = 0u;
                        主程.每秒计时 = 主程.当前时间.AddSeconds(1.0);
                    }
                    else
                    {
                        主程.循环计数++;
                    }
                    GM命令 result;
                    while (主程.外部命令.TryDequeue(out result))
                    {
                        result.执行命令();
                    }
                    网络服务网关.处理数据();
                    地图处理网关.处理数据();
                    游戏数据网关.处理数据();
                    系统公告.处理数据();
                    机器人.处理数据();
                    主程.处理重载任务();
                    //主程.处理网页事件();
                    if (主程.当前时间 > 主程.上次保存时间.AddMinutes((int)Settings.自动保存时间))
                    {
                        主程.上次保存时间 = 主程.当前时间;
                        游戏数据网关.保存数据();
                        主程.保存数据库();
                    }
                    if (主程.当前时间.Date != 主程.每日执行时间.Date)
                    {
                        主程.每日执行时间 = 主程.当前时间;
                        地图处理网关.处理在线玩家天数变更();
                    }
                    游戏脚本.垃圾收集();
                }
                catch (Exception ex)
                {
                    主程.添加系统日志("发生致命错误, 服务器即将停止");
                    if (!Directory.Exists("..\\Log\\Error"))
                    {
                        Directory.CreateDirectory("..\\Log\\Error");
                    }
                    File.WriteAllText($"..\\Log\\Error\\{DateTime.Now:yyyy-MM-dd--HH-mm-ss}.txt", "错误信息:\r\n" + ex.Message + "\r\n堆栈信息:\r\n" + ex.StackTrace);
                    主程.添加系统日志("错误信息已保存到日志, 请注意查看");
                }
            }
            主程.添加系统日志("正在清理物品数据...");
            地图处理网关.清理物品();
            Thread.Sleep(10000);
            主程.添加系统日志("正在保存客户数据...");
            游戏数据网关.检测物品数据(添加到数据表: true);
            游戏数据网关.强制保存();
            Thread.Sleep(10000);
            主程.添加系统日志("正在导出客户数据...");
            游戏数据网关.导出数据();
            主窗口.服务停止回调();
            网络服务网关.循环结束();
            主程.主线程 = null;
            主程.添加系统日志("服务器已成功关闭");

        }

        public static void ReloadNPCs(int[] scriptIds = null)
        {
            if (scriptIds == null)
            {
                foreach (int key2 in 主程.Scripts.Keys)
                {
                    主程.Scripts[key2].Load();
                }
            }
            else
            {
                foreach (int key in scriptIds)
                {
                    if (主程.Scripts.TryGetValue(key, out var value))
                    {
                        value.Load();
                        主程.添加系统日志("NPC " + value.FileName + " 脚本重载成功...");
                    }
                }
            }
            游戏脚本.初始化脚本系统();
            主程.添加系统日志("NPC 脚本重载成功...");
        }

        private static void 处理重载任务()
        {
            if (主程.当前时间 > 主程.下次重载任务时间 && 主程.重载任务列表.TryDequeue(out var result))
            {
                result();
                if (主程.重载任务列表.Count != 0)
                {
                    主程.下次重载任务时间 = 主程.当前时间.AddMilliseconds(500.0);
                }
            }
        }

        static 主程()
        {
            主程.重载任务列表 = new ConcurrentQueue<Action>();
            主程.Scripts = new Dictionary<int, NPCScript>();
            主程.DisplayLogs = new ConcurrentQueue<string>();
            主程.Logs = new ConcurrentQueue<string>();
            主程.UseLogConsole = false;
            主程.OldForm = false;
            主程.DisplayChatLogs = new ConcurrentQueue<string>();
            主程.ChatLogs = new ConcurrentQueue<string>();
            主程.GameLogs = new ConcurrentQueue<string>();
            主程.ItemLogs = new ConcurrentQueue<string>();
            主程.CurrencyLogs = new ConcurrentQueue<string>();
            //主程.WebLogs = new ConcurrentQueue<string>();
            //主程.WebLogDatas = new ConcurrentQueue<LogData>();
            主程.DisplayCommandLogs = new ConcurrentQueue<string>();
            主程.创角请求列表 = new ConcurrentQueue<创角请求>();
            主程.创角返回列表 = new ConcurrentQueue<创角请求>();
            //主程.LogWebSite = "http://58.87.104.125:8081/";
            //主程.WebEvent = new ConcurrentQueue<WebData>();
            主程.当前时间 = DateTime.Now;
            主程.每秒计时 = DateTime.Now.AddSeconds(1.0);
            主程.随机数 = new Random();
        }


        private static void 处理网页事件()
        {
            if (!主程.WebEvent.IsEmpty && 主程.WebEvent.TryDequeue(out var result))
            {
                switch (result.Type)
                {
                    case WebDataType.PayMent:
                        主程.WebPayMent(result);
                        break;
                    case WebDataType.ModifyRole:
                        主程.WebModifyRole(result);
                        break;
                    case WebDataType.UseCmd:
                        主程.WebUseCmd(result);
                        break;
                }
            }
        }

        public static void WebModifyRole(WebData webData)
        {
            if (webData.Data.ContainsKey("roleId") && int.TryParse(webData.Data["roleId"], out var result))
            {
                if (webData.Data.ContainsKey("type") && int.TryParse(webData.Data["type"], out var _))
                {
                    游戏数据 value;
                    if (!webData.Data.ContainsKey("value"))
                    {
                        HttpService.Return(webData.Respons, "wrong param 'value'");
                    }
                    else if (游戏数据网关.角色数据表.数据表.TryGetValue(result, out value) && value is 角色数据)
                    {
                        HttpService.Return(webData.Respons, "wrong type");
                        HttpService.Return(webData.Respons, "success");
                    }
                    else
                    {
                        HttpService.Return(webData.Respons, "not find role");
                    }
                }
                else
                {
                    HttpService.Return(webData.Respons, "wrong param 'type'");
                }
            }
            else
            {
                HttpService.Return(webData.Respons, "wrong param 'roleId'");
            }
        }

        public static void WebUseCmd(WebData webData)
        {
            if (!webData.Data.ContainsKey("cmd"))
            {
                HttpService.Return(webData.Respons, "wrong param 'cmd'");
                return;
            }
            string text;
            text = webData.Data["cmd"];
            主程.添加命令日志("=> " + text);
            if (text[0] != '@')
            {
                HttpService.Return(webData.Respons, "<= 命令解析错误, GM命令必须以 '@' 开头. 输入 '@查看命令' 获取所有受支持的命令格式");
                主程.添加命令日志("<= 命令解析错误, GM命令必须以 '@' 开头. 输入 '@查看命令' 获取所有受支持的命令格式");
            }
            else if (text.Trim('@', ' ').Length == 0)
            {
                HttpService.Return(webData.Respons, "<= 命令解析错误, GM命令不能为空. 输入 '@查看命令' 获取所有受支持的命令格式");
                主程.添加命令日志("<= 命令解析错误, GM命令不能为空. 输入 '@查看命令' 获取所有受支持的命令格式");
            }
            else
            {
                if (!GM命令.解析命令(text, out var 命令))
                {
                    return;
                }
                if (命令.执行方式 == 执行方式.前台立即执行)
                {
                    命令.执行命令();
                    HttpService.Return(webData.Respons, "success");
                }
                else if (命令.执行方式 == 执行方式.优先后台执行)
                {
                    if (主程.已经启动)
                    {
                        主程.外部命令.Enqueue(命令);
                        HttpService.Return(webData.Respons, "success");
                    }
                    else
                    {
                        命令.执行命令();
                    }
                }
                else if (命令.执行方式 == 执行方式.只能后台执行)
                {
                    if (主程.已经启动)
                    {
                        主程.外部命令.Enqueue(命令);
                        HttpService.Return(webData.Respons, "success");
                    }
                    else
                    {
                        HttpService.Return(webData.Respons, "<= 命令执行失败, 当前命令只能在服务器运行时执行, 请先启动服务器");
                        主程.添加命令日志("<= 命令执行失败, 当前命令只能在服务器运行时执行, 请先启动服务器");
                    }
                }
                else if (命令.执行方式 == 执行方式.只能空闲执行)
                {
                    if (!主程.已经启动 && (主程.主线程 == null || !主程.主线程.IsAlive))
                    {
                        命令.执行命令();
                        HttpService.Return(webData.Respons, "success");
                    }
                    else
                    {
                        HttpService.Return(webData.Respons, "<= 命令执行失败, 当前命令只能在服务器未运行时执行, 请先关闭服务器");
                        主程.添加命令日志("<= 命令执行失败, 当前命令只能在服务器未运行时执行, 请先关闭服务器");
                    }
                }
            }
        }

        public static void WebPayMent(WebData webData)
        {
            int result;
            if (!webData.Data.ContainsKey("account"))
            {
                HttpService.Return(webData.Respons, "wrong param 'account'");
            }
            else if (webData.Data.ContainsKey("money") && int.TryParse(webData.Data["money"], out result))
            {
                if (webData.Data.ContainsKey("amount") && uint.TryParse(webData.Data["amount"], out var result2))
                {
                    int result3;
                    result3 = 0;
                    游戏数据 value2;
                    if (!webData.Data.TryGetValue("encourage", out var value) && !int.TryParse(value, out result3))
                    {
                        HttpService.Return(webData.Respons, "wrong param 'encourageStr'");
                    }
                    else if (游戏数据网关.角色数据表.检索表.TryGetValue(webData.Data["account"], out value2) && value2 is 角色数据 角色数据)
                    {
                        uint num;
                        num = 100u;
                        if (Settings.充值货币类型 == 0)
                        {
                            uint num2;
                            num2 = result2 * num;
                            角色数据.元宝数量 += num2;
                            角色数据.累计充值.V += result;
                            角色数据.今日充值.V += result;
                            主程.添加货币日志(角色数据, "玩家充值元宝", 游戏货币.元宝, num2);
                            角色数据.网络连接?.发送封包(new 同步元宝数量
                            {
                                元宝数量 = 角色数据.元宝数量
                            });
                            主程.添加系统日志($"{角色数据.角色名字} 充值[{result}元],赠送{result3}, 当前元宝: {角色数据.元宝数量}");
                            HttpService.Return(webData.Respons, "success");
                            //主程.WebLog(LogDataType.WebsiteRechargeLog, Settings.统计UUID代码, Settings.游戏区服名称, "", 角色数据.角色名字.V, 角色数据.所属账号.V.账号名字.V, result2.ToString(), result.ToString(), "元宝");
                            充值奖励.来钱了(角色数据, (uint)result);
                            if (Settings.充值公告 != "")
                            {
                                网络服务网关.发送公告(Settings.充值公告.Replace("%P%", 角色数据.角色名字.ToString()).Replace("%M%", num2.ToString()));
                            }
                        }
                        else if (Settings.充值货币类型 == 1)
                        {
                            uint num3;
                            num3 = ((num > 100) ? (result2 * num / 100) : result2);
                            角色数据.银币数量 += num3;
                            角色数据.累计充值.V += result;
                            角色数据.今日充值.V += result;
                            主程.添加货币日志(角色数据, "玩家充值银币", 游戏货币.银币, num3);
                            角色数据.网络连接?.发送封包(new 货币数量变动
                            {
                                货币类型 = 0,
                                货币数量 = 角色数据.银币数量
                            });
                            主程.添加系统日志($"{角色数据.角色名字} 充值[{result}元],赠送{result3}, 当前银币: {角色数据.银币数量}");
                            HttpService.Return(webData.Respons, "success");
                            //主程.WebLog(LogDataType.WebsiteRechargeLog, Settings.统计UUID代码, Settings.游戏区服名称, "", 角色数据.角色名字.V, 角色数据.所属账号.V.账号名字.V, result2.ToString(), result.ToString(), "银币");
                            充值奖励.来钱了(角色数据, (uint)result);
                            if (Settings.充值公告 != "")
                            {
                                网络服务网关.发送公告(Settings.充值公告.Replace("%P%", 角色数据.角色名字.ToString()).Replace("%M%", num3.ToString()));
                            }
                        }
                    }
                    else
                    {
                        主程.添加系统日志($"{webData.Data["account"]}尝试充值[{result}]元,赠送{result3}，但是没有找到此玩家");
                        HttpService.Return(webData.Respons, "not find role'");
                    }
                }
                else
                {
                    HttpService.Return(webData.Respons, "wrong param 'amount'");
                }
            }
            else
            {
                HttpService.Return(webData.Respons, "wrong param 'money'");
            }
        }
    }
}
