using System.Collections.Generic;
using 游戏服务器.地图类;
using 游戏服务器.模板类;
using 游戏服务器.网络类;

namespace 游戏服务器.管理命令
{
	public sealed class 添加铭文 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1)]
		public byte 装备位置;

		[字段描述(0, 排序 = 2)]
		public ushort 铭文编号;

		[字段描述(0, 排序 = 3)]
		public ushort 铭文序号;

		public override 执行方式 执行方式 => 执行方式.前台立即执行;

		public override void 执行命令()
		{
			foreach (KeyValuePair<int, 玩家实例> item in 地图处理网关.玩家对象表)
			{
				if (!(item.Value.角色数据.角色名字.V == this.角色名字) || !item.Value.角色装备.TryGetValue(this.装备位置, out var v) || !铭文技能.数据表.TryGetValue(this.铭文编号, out var value))
				{
					continue;
				}
				if (this.铭文序号 == 1)
				{
					if (v.第一铭文 != null)
					{
						item.Value.玩家装卸铭文(v.第一铭文.技能编号, 0);
					}
					v.第一铭文 = value;
					item.Value.玩家装卸铭文(v.第一铭文.技能编号, v.第一铭文.铭文编号);
					item.Value.网络连接?.发送封包(new 玩家物品变动
					{
						物品描述 = v.字节描述()
					});
				}
				else if (this.铭文序号 == 2 && v.第二铭文 != null)
				{
					item.Value.玩家装卸铭文(v.第二铭文.技能编号, 0);
					v.第二铭文 = value;
					item.Value.玩家装卸铭文(v.第二铭文.技能编号, v.第二铭文.铭文编号);
					item.Value.网络连接?.发送封包(new 玩家物品变动
					{
						物品描述 = v.字节描述()
					});
				}
			}
		}
	}
}
