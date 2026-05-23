using System.Collections.Generic;
using 游戏服务器.地图类;

namespace 游戏服务器.管理命令
{
	public sealed class 添加BUFF : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1)]
		public ushort BUFF编号;

		[字段描述(0, 排序 = 2, 可选 = true)]
		public uint 移除;

		public override 执行方式 执行方式 => 执行方式.前台立即执行;

		public override void 执行命令()
		{
			foreach (KeyValuePair<int, 玩家实例> item in 地图处理网关.玩家对象表)
			{
				if (item.Value.角色数据.角色名字.V == this.角色名字)
				{
					if (this.移除 != 0)
					{
						item.Value.移除Buff时处理(this.BUFF编号);
					}
					else
					{
						item.Value.添加Buff时处理(this.BUFF编号, item.Value);
					}
					break;
				}
			}
		}
	}
}
