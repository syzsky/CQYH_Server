using System;
using System.Linq;
using System.Text;
using 游戏服务器.地图类;
using 游戏服务器.模板类;
using 游戏服务器.数据类;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace 游戏服务器.管理命令
{
	public class 下发礼包码 : 玩家命令
	{
		[字段描述(0)]
		public string 角色名;

		[字段描述(1)]
		public string 礼包码;

		[字段描述(2)]
		public int HTTPStaute;

		[字段描述(3)]
		public string HTTPResult;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			string name;
			name = base.GetType().Name;
			try
			{
				if (!游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名, out var value))
				{
					主程.添加系统日志($"[{name}]角色 {this.角色名} 不存在 {this.礼包码} ");
					return;
				}
				玩家实例 玩家实例;
				玩家实例 = (value as 角色数据).网络连接?.绑定角色;
				if (玩家实例 == null)
				{
					主程.添加系统日志($"[{name}]{this.角色名} {this.礼包码} 玩家不在线");
					return;
				}
				string @string;
				@string = Encoding.UTF8.GetString(Convert.FromBase64String(this.HTTPResult));
				if (this.HTTPStaute != 0)
				{
					主程.添加系统日志($"[{name}]{this.角色名} {this.礼包码} 礼包查询返回值错误:{@string}");
					玩家实例?.发送顶部公告("礼包码验证失败,请联系管理员");
				}
				if (string.IsNullOrEmpty(@string))
				{
					主程.添加系统日志($"[{name}]{this.角色名} {this.礼包码} 礼包请求返回值为空");
					玩家实例.发送顶部公告("礼包码验证失败,请联系管理员");
					return;
				}
				int result;
				result = -1;
				try
				{
					HttpResult_RedeemCode httpResult_RedeemCode;
					httpResult_RedeemCode = JsonConvert.DeserializeObject<HttpResult_RedeemCode>(@string);
					if (httpResult_RedeemCode == null)
					{
						主程.添加系统日志($"[{name}]{this.角色名} {this.礼包码} 礼包返回值反序列化失败:{@string}");
						玩家实例.发送顶部公告("礼包码验证失败,请联系管理员");
						return;
					}
					if (httpResult_RedeemCode.code != 0)
					{
						主程.添加系统日志($"[{name}]{this.角色名} {this.礼包码} 礼包返回Code不为0:{@string}");
						玩家实例.发送顶部公告(httpResult_RedeemCode.message ?? "礼包码验证失败,请联系管理员");
						return;
					}
					result = httpResult_RedeemCode.data.typeid;
				}
				catch (Exception ex)
				{
					主程.添加系统日志($"[{name}]{this.角色名} {this.礼包码} 礼包返回值反序列化异常:{ex.Message}");
				}
				if (result == -1)
				{
					try
					{
						JObject jObject;
						jObject = (JObject)JsonConvert.DeserializeObject(@string);
						if (jObject == null)
						{
							主程.添加系统日志($"[{name}]{this.角色名}领取{this.礼包码}失败,查询返回值反序列化JObject失败:{@string}");
							玩家实例.发送顶部公告("礼包码验证失败,请联系管理员");
							return;
						}
						JProperty iJProperty;
						iJProperty = jObject.Properties().FirstOrDefault((JProperty p) => p.Name.Equals("code", StringComparison.OrdinalIgnoreCase));
						if (iJProperty == null)
						{
							主程.添加系统日志($"[{name}]{this.角色名}领取{this.礼包码}失败,查询返回值反序列化JObject失败:{@string}");
							玩家实例.发送顶部公告("礼包码验证失败,请联系管理员");
							return;
						}
						if (iJProperty.Value.ToString() != "0")
						{
							主程.添加系统日志($"[{name}]{this.角色名}领取{this.礼包码}失败,查询返回Code不为0:{@string}");
							玩家实例.发送顶部公告("礼包码验证失败,请联系管理员");
							return;
						}
						JProperty iJProperty2;
						iJProperty2 = (jObject.Properties().FirstOrDefault((JProperty p) => p.Name.Equals("data", StringComparison.OrdinalIgnoreCase)) as JObject)?.Properties().FirstOrDefault((JProperty p) => p.Name.Equals("typeid", StringComparison.OrdinalIgnoreCase));
						if (iJProperty2 == null)
						{
							主程.添加系统日志($"[{name}]{this.角色名}领取{this.礼包码}失败,查询返回值data字段错误,未找到typeid:{@string}");
							玩家实例.发送顶部公告("礼包码验证失败,请联系管理员");
							return;
						}
						if (!int.TryParse(iJProperty2.Value.ToString(), out result))
						{
							主程.添加系统日志($"[{name}]{this.角色名}领取{this.礼包码}失败,查询返回值typeid格式错误:{@string}");
							玩家实例.发送顶部公告("礼包码验证失败,请联系管理员");
							return;
						}
					}
					catch (Exception ex2)
					{
						主程.添加系统日志($"[{name}]{this.角色名}领取{this.礼包码}失败,查询返回值JObject反序列化异常:{ex2.Message}");
					}
				}
				玩家实例.CallDefaultNPC(DefaultNPCType.RedeemCode, true, result);
				主程.添加系统日志($"[{name}]{this.角色名} 下发礼包码 {this.礼包码} 成功");
			}
			catch (Exception ex3)
			{
				主程.添加系统日志($"[{name}]{this.角色名} {this.礼包码} 礼包领取异常:{ex3.Message}");
			}
		}

		public override void 执行命令(玩家实例 玩家)
		{
		}
	}
}
