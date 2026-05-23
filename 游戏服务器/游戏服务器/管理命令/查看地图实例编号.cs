using System;
using System.Collections.Generic;
using System.Linq;
using 游戏服务器.地图类;

namespace 游戏服务器.管理命令
{
	public sealed class 查看地图实例编号 : GM命令
	{
		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			try
			{
				Dictionary<int, 地图实例> source;
				source = 地图处理网关.地图实例表.ToDictionary((KeyValuePair<int, 地图实例> k) => k.Key, (KeyValuePair<int, 地图实例> v) => v.Value);
				List<地图实例> fbs;
				fbs = 地图处理网关.副本实例表.ToList();
				主程.添加命令日志(string.Concat(str3: string.Concat(str2: string.Join("\r\n", from d in fbs.ToDictionary((地图实例 k) => fbs.IndexOf(k), (地图实例 v) => v)
					select $"{d.Value.地图模板.地图名字}\t{d.Key}\t副本\t怪物数量:{d.Value.对象列表.Count((地图对象 v) => !v.对象死亡 && v.对象类型 == 游戏对象类型.怪物)}"), str0: string.Join("\r\n", source.Select((KeyValuePair<int, 地图实例> d) => $"{d.Value.地图模板.地图名字}\t{d.Key}\t怪物数量:{d.Value.对象列表.Count((地图对象 v) => !v.对象死亡 && v.对象类型 == 游戏对象类型.怪物)}")), str1: "\r\n"), str0: "<= @", str1: base.GetType().Name, str2: " 命令执行成功, \r\n"));
			}
			catch (Exception value)
			{
				主程.添加命令日志("<= @" + base.GetType().Name + $" 命令执行失败, {value}");
			}
		}
	}
}
