using System.Collections.Generic;
using System.Linq;
using 游戏服务器.地图类;
using 游戏服务器.模板类;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.管理命令
{
	public sealed class 添加龙卫 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1)]
		public byte 装备位置;

		[字段描述(0, 排序 = 2)]
		public ushort 龙卫编号;

		public override 执行方式 执行方式 => 执行方式.前台立即执行;

		public override void 执行命令()
		{
			foreach (KeyValuePair<int, 玩家实例> item in 地图处理网关.玩家对象表)
			{
				if (!(item.Value.角色数据.角色名字.V == this.角色名字))
				{
					continue;
				}
				for (int num = item.Value.角色数据.龙卫属性.Count - 1; num >= 0; num--)
				{
					龙卫数据 龙卫数据;
					龙卫数据 = item.Value.角色数据.龙卫属性.ElementAt(num);
					if (龙卫数据.装备位置 == this.装备位置)
					{
						item.Value.角色数据.龙卫属性.Remove(龙卫数据);
						item.Value.属性加成.Remove(龙卫数据);
						龙卫数据.删除数据();
					}
				}
				if (龙卫模板.数据表.TryGetValue(this.龙卫编号, out var value))
				{
					for (int i = 1; i < 4; i++)
					{
						item.Value.角色数据.龙卫属性.Add(new 龙卫数据(value, this.装备位置, (byte)((value.词缀类型 == 龙卫词缀类型.攻击) ? i : (i + 3)), 龙卫品质.魔龙));
					}
				}
				item.Value.发送封包(new 龙卫觉醒回执
				{
					属性位置 = this.装备位置,
					数据 = item.Value.角色数据.获取龙卫属性封包数据(this.装备位置)
				});
				item.Value.刷新龙卫激活状态(是否更新属性: true);
				item.Value.发送封包(new 龙卫属性激活状态
				{
					属性位置 = this.装备位置,
					数据 = item.Value.角色数据.获取龙卫激活封包数据(this.装备位置)
				});
			}
		}
	}
}
