using System.Drawing;
using System.Linq;
using 游戏服务器.地图类;
using 游戏服务器.模板类;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public sealed class 测试命令 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 角色名称;

		public override 执行方式 执行方式 => 执行方式.前台立即执行;

		public override void 执行命令()
		{
			foreach (游戏数据 value2 in 游戏数据网关.角色数据表.数据表.Values)
			{
				角色数据 角色数据;
				角色数据 = value2 as 角色数据;
				if (角色数据.当前等级.V >= 30 && !角色数据.管理员角色.V && 角色数据.网络连接 == null)
				{
					玩家实例 玩家实例;
					玩家实例 = null;
					玩家实例 = ((!地图处理网关.地图对象表.TryGetValue(角色数据.数据索引.V, out var value)) ? new 玩家实例(角色数据, null, 自动战斗: true) : (value as 玩家实例));
					玩家实例.当前地图 = 地图处理网关.已分配地图(153 + 主程.随机数.Next(1, 10));
					玩家实例.当前坐标 = 玩家实例.当前地图.地图区域.First().随机坐标;
					玩家实例.玩家进入场景();
					玩家实例.释放技能 = 玩家实例.获取最优技能();
					玩家实例.自动战斗 = true;
					玩家实例.移除Buff时处理(4501);
					玩家实例.无敌模式 = true;
					if (玩家实例.角色背包.TryGetValue(0, out var v))
					{
						new 寄售数据(玩家实例.角色数据, v, 10000, 172800);
						玩家实例.角色背包.Remove(0);
					}
					if (玩家实例.角色背包.TryGetValue(1, out var v2))
					{
						new 寄售数据(玩家实例.角色数据, v2, 10000, 172800);
						玩家实例.角色背包.Remove(1);
					}
					if (玩家实例.角色背包.TryGetValue(2, out var v3))
					{
						new 寄售数据(玩家实例.角色数据, v3, 10000, 172800);
						玩家实例.角色背包.Remove(2);
					}
					Point point;
					point = 玩家实例.当前地图.随机传送(玩家实例.当前坐标);
					if (point != default(Point))
					{
						玩家实例.玩家切换地图(玩家实例.当前地图, 地图区域类型.未知区域, point);
					}
				}
			}
		}
	}
}
