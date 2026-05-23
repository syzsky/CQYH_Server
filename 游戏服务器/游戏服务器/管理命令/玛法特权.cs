using System.Collections.Generic;
using 游戏服务器.地图类;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public sealed class 玛法特权 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1)]
		public byte 特权类型;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			if (地图处理网关.玩家对象表 == null)
			{
				if (游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value) && value is 角色数据 角色数据)
				{
					角色数据.本期特权.V = this.特权类型;
					角色数据.本期记录.V = uint.MaxValue;
					角色数据.本期日期.V = 主程.当前时间;
					主程.添加命令日志($"<= @{base.GetType().Name} 命令已经执行, 本期特权: {角色数据.本期特权.V}");
				}
				else
				{
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 角色不存在");
				}
				return;
			}
			foreach (KeyValuePair<int, 玩家实例> item in 地图处理网关.玩家对象表)
			{
				if (item.Value.角色数据.角色名字.V == this.角色名字)
				{
					item.Value.获得玛法特权(this.特权类型);
					主程.添加命令日志($"<= @{base.GetType().Name} 命令已经执行, 本期特权: {item.Value.本期特权}");
					return;
				}
			}
			主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败, 角色不存在");
		}
	}
}
