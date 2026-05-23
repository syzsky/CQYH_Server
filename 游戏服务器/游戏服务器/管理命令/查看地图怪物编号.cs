using System;
using System.Collections.Generic;
using System.Linq;
using _001D_000F_0007_0013_0011_0015;
using 游戏服务器.地图类;

namespace 游戏服务器.管理命令
{
	public sealed class 查看地图怪物编号 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public int 地图编号;

		[字段描述(0, 排序 = 0)]
		public int 怪物编号;

		[字段描述(0, 排序 = 1, 可选 = true)]
		public bool 过滤死亡;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			try
			{
				if (this.地图编号 <= 0)
				{
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 地图编号不能为空");
					return;
				}
				new List<string>();
				if (!地图处理网关.地图实例表.TryGetValue(this.地图编号 * 16 + 1, out var 地图))
				{
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 地图实例编号未找到");
					return;
				}
				Dictionary<string, List<怪物实例>> source;
				source = (from m in 地图处理网关.怪物对象表.Values.ToList()
					where m.当前地图 == 地图 && m.对象模板.怪物编号 == this.怪物编号 && (!this.过滤死亡 || !m.对象死亡)
					group m by m.对象模板.怪物名字).ToDictionary((IGrouping<string, 怪物实例> k) => k.Key, (IGrouping<string, 怪物实例> v) => v.ToList());
				if (!source.Any())
				{
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 没有找到怪物");
					return;
				}
				主程.添加命令日志(string.Concat(str3: string.Join("\r\n", source.Select((KeyValuePair<string, List<怪物实例>> d) => d.Key + "    " + string.Join(",", d.Value.Select((怪物实例 m) => m.地图编号)))), str0: "<= @", str1: base.GetType().Name, str2: " 命令执行成功, \r\n"));
			}
			catch (Exception value)
			{
				主程.添加命令日志("<= @" + base.GetType().Name + $" 命令执行成功, {value}");
			}
		}
	}
}
