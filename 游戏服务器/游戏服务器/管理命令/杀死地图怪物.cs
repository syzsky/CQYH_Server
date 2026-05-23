using System;
using System.Collections.Generic;
using System.Linq;
using 游戏服务器.地图类;
using 游戏服务器.模板类;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public sealed class 杀死地图怪物 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public int 地图实例编号;

		[字段描述(0, 排序 = 1, 可选 = true)]
		public byte 是否副本;

		[字段描述(0, 排序 = 2, 可选 = true)]
		public int 禁止复活;

		[字段描述(0, 排序 = 3, 可选 = true)]
		public string 怪物名字;

		[字段描述(0, 排序 = 4, 可选 = true)]
		public string 归属玩家名字;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			try
			{
				if (this.地图实例编号 <= 0)
				{
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 地图实例编号不能为空");
					return;
				}
				玩家实例 value;
				value = null;
				if (!string.IsNullOrWhiteSpace(this.归属玩家名字))
				{
					if (!游戏数据网关.角色数据表.检索表.TryGetValue(this.归属玩家名字, out var value2))
					{
						主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 玩家不存在");
						return;
					}
					if (!地图处理网关.玩家对象表.TryGetValue(value2.数据索引.V, out value))
					{
						主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 玩家不在线");
						return;
					}
				}
				if (!地图处理网关.地图实例表.TryGetValue(this.地图实例编号, out var value3) && this.是否副本 == 0)
				{
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 地图实例编号未找到");
					return;
				}
				if (this.是否副本 > 0)
				{
					if (this.地图实例编号 >= 地图处理网关.副本实例表.Count)
					{
						主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 地图实例编号超过副本地图索引");
						return;
					}
					value3 = 地图处理网关.副本实例表.ToList()[this.地图实例编号];
				}
				if (!string.IsNullOrWhiteSpace(this.怪物名字) && this.怪物名字 != "*" && !游戏怪物.数据表.TryGetValue(this.怪物名字, out var _))
				{
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 怪物名字未找到,省略或填 * 杀死全部");
					return;
				}
				Dictionary<string, int> dictionary;
				dictionary = new Dictionary<string, int>();
				foreach (怪物实例 item in 地图处理网关.怪物对象表.Values.ToList())
				{
					if (!item.对象死亡 && item.当前地图 == value3 && (!(this.怪物名字 != "*") || !(item.对象名字 != this.怪物名字)))
					{
						item.禁止复活 = this.禁止复活 > 0;
						if (value != null)
						{
							item.对象仇恨.当前目标 = value;
							item.对象仇恨.仇恨列表[value] = new 对象仇恨.仇恨详情(主程.当前时间.AddMilliseconds(100000.0), 99999999);
						}
						else
						{
							item.对象仇恨.仇恨列表.Clear();
							item.对象仇恨.当前目标 = null;
						}
						item.自身死亡处理(null, 技能击杀: false, 脚本击杀: true);
						if (dictionary.ContainsKey(item.对象名字))
						{
							dictionary[item.对象名字]++;
						}
						else
						{
							dictionary.Add(item.对象名字, 1);
						}
					}
				}
				主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行成功, \r\n" + string.Join("\r\n", dictionary.Select((KeyValuePair<string, int> d) => $"{d.Key}\t{d.Value}")));
			}
			catch (Exception value5)
			{
				主程.添加命令日志("<= @" + base.GetType().Name + $" 命令执行异常, {value5}");
			}
		}
	}
}
