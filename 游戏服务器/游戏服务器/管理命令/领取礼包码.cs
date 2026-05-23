using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using 游戏服务器.地图类;

namespace 游戏服务器.管理命令
{
    public class 领取礼包码 : 玩家命令
    {
        [字段描述(0)]
        public string 礼包码;

        private static Regex RedeemCodeReg = new Regex("^[\\da-zA-z]{12}$");

        public override 执行方式 执行方式 => 执行方式.优先后台执行;

        public override void 执行命令()
        {
        }

        public override void 执行命令(玩家实例 玩家)
        {
            string name;
            name = base.GetType().Name;
            if (玩家 != null && 玩家.网络连接 != null)
            {
                try
                {
                    if (!领取礼包码.RedeemCodeReg.IsMatch(this.礼包码))
                    {
                        玩家.发送顶部公告("礼包码长度或格式不正确");
                    }
                    else
                    {
                        主程.添加系统日志($"[{name}]{玩家.角色数据.角色名字.V} 领取礼包码 {this.礼包码}");
                        string post;
                        post = $"{{\"account\": \"{玩家.所属账号.账号名字.V}\",\"roleName\": \"{玩家.角色数据.角色名字.V}\",\"redeemcode\": \"{this.礼包码}\"}}";
                        /*
						Task.Run(delegate
						{
							int value;
							value = 主程.HttpPost2("https://pay.tengcanol.com/admin/site/gameVerifyRedeemCode", post, out var reslut);
							主程.AddWebEvent(WebDataType.UseCmd, new Dictionary<string, string> { 
							{
								"cmd",
								$"@下发礼包码 {玩家.对象名字} {this.礼包码} {value} {Convert.ToBase64String(Encoding.UTF8.GetBytes(reslut))}"
							} }, null);
						});
						*/
                    }
                    return;
                }
                catch (Exception ex)
                {
                    玩家.发送顶部公告("领取礼包码错误,请联系管理员");
                    主程.添加系统日志($"[{name}]{this.礼包码} 礼包领取异常:{ex.Message}");
                    return;
                }
            }
            主程.添加系统日志("[" + name + "]玩家连接异常");
        }
    }
}
