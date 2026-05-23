using 游戏服务器.模板类;
using 游戏服务器.数据类;
using 游戏服务器.网络类;

namespace 游戏服务器.管理命令
{
	public sealed class 制造物品 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名字;

		[字段描述(0, 排序 = 1)]
		public string 物品名字;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			if (游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value) && value is 角色数据 角色数据)
			{
				if (!游戏物品.检索表.TryGetValue(this.物品名字, out var value2))
				{
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败，物品不存在");
					return;
				}
				if (角色数据.角色背包.Count >= 角色数据.背包大小.V)
				{
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败，角色的背包已满");
					return;
				}
				if (value2.物品持久 == 0)
				{
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败，无法执行 AddItemsCommand");
					return;
				}
				if (角色数据.尝试获取背包空余格子(out var location))
				{
					if (value2 is 游戏装备 模板)
					{
						角色数据.角色背包[location] = new 装备数据(模板, 角色数据, 1, location, 随机生成: true, 绑定: false, 角色数据.角色名字.V + "-@制造物品");
					}
					else if (value2.持久类型 == 物品持久分类.容器)
					{
						角色数据.角色背包[location] = new 物品数据(value2, 角色数据, 1, location, 0, 绑定: false, 角色数据.角色名字.V + "-@制造物品");
					}
					else if (value2.持久类型 == 物品持久分类.堆叠)
					{
						角色数据.角色背包[location] = new 物品数据(value2, 角色数据, 1, location, 1, 绑定: false, 角色数据.角色名字.V + "-@制造物品");
					}
					else
					{
						角色数据.角色背包[location] = new 物品数据(value2, 角色数据, 1, location, value2.物品持久, 绑定: false, 角色数据.角色名字.V + "-@制造物品");
					}
					客户网络 网络连接;
					网络连接 = 角色数据.网络连接;
					if (网络连接 != null)
					{
						网络连接?.发送封包(new 玩家物品变动
						{
							物品描述 = 角色数据.角色背包[location].字节描述()
						});
					}
					主程.添加命令日志("<= @" + base.GetType().Name + " 命令已执行，物品已添加到角色背包中。");
					return;
				}
			}
			主程.添加命令日志("<= @" + base.GetType().Name + " 命令执行失败，角色不存在");
		}
	}
}
