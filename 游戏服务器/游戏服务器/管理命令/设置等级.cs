using System.Collections.Generic;
using 游戏服务器.地图类;

namespace 游戏服务器.管理命令
{
	public sealed class 设置等级 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1)]
		public byte 等级;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			foreach (KeyValuePair<int, 玩家实例> item in 地图处理网关.玩家对象表)
			{
				if (item.Value.角色数据.角色名字.V == this.角色名字)
				{
					item.Value.当前等级 = this.等级;
					item.Value.玩家升级处理();
				}
			}
		}
	}
}
