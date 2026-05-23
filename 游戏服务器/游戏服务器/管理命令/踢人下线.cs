using System;
using System.Collections.Generic;
using System.Linq;
using 游戏服务器.地图类;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public sealed class 踢人下线 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1, 可选 = true)]
		public bool 踢整个网络地址;

		[字段描述(0, 排序 = 1, 可选 = true)]
		public bool 踢整个物理地址;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			if (游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value))
			{
				角色数据 角色数据;
				角色数据 = value as 角色数据;
				if (角色数据 != null)
				{
					List<角色数据> list;
					list = new List<角色数据> { 角色数据 };
					if (this.踢整个网络地址)
					{
						list.AddRange(from p in 地图处理网关.玩家对象表.Values
							where p.角色数据.网络地址.V == 角色数据.网络地址.V
							select p.角色数据);
					}
					if (this.踢整个物理地址)
					{
						list.AddRange(from p in 地图处理网关.玩家对象表.Values
							where p.角色数据.物理地址.V == 角色数据.物理地址.V
							select p.角色数据);
					}
					HashSet<角色数据> hashSet;
					hashSet = new HashSet<角色数据>(list);
					foreach (角色数据 item in hashSet)
					{
						item?.网络连接?.尝试断开连接(new Exception("手动踢下线"));
					}
					主程.添加命令日志($"<= @{base.GetType().Name} 命令已经执行, 共踢 {hashSet.Count} 人");
					return;
				}
			}
			主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 角色不存在");
		}
	}
}
