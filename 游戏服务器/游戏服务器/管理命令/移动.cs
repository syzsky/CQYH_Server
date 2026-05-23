using System.Drawing;
using System.Linq;
using 游戏服务器.地图类;
using 游戏服务器.模板类;
using 游戏服务器.数据类;

namespace 游戏服务器.管理命令
{
	public class 移动 : GM命令
	{
		[字段描述(0)]
		public string 角色名字;

		[字段描述(1)]
		public byte 地图编号;

		[字段描述(2, 可选 = true)]
		public int? MapX;

		[字段描述(3, 可选 = true)]
		public int? MapY;

		[字段描述(3, 可选 = true)]
		public bool? TerrPos;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			if (!游戏数据网关.角色数据表.检索表.TryGetValue(this.角色名字, out var value))
			{
				主程.添加命令日志("<= @Move Command execution failed, character " + this.角色名字 + " does not exist");
				return;
			}
			if (!游戏地图.数据表.TryGetValue(this.地图编号, out var value2))
			{
				主程.添加命令日志($"<= @Move Command execution failed, map {this.地图编号} does not exist");
				return;
			}
			玩家实例 玩家实例;
			玩家实例 = (value as 角色数据)?.网络连接?.绑定角色;
			if (玩家实例 == null)
			{
				主程.添加命令日志("<= @Move Command execution failed, player " + this.角色名字 + " not connected");
				return;
			}
			地图实例 地图实例;
			地图实例 = 地图处理网关.已分配地图(value2.地图编号);
			地图区域 地图区域;
			地图区域 = 地图实例.传送区域 ?? 地图实例.地图区域.FirstOrDefault();
			if (this.TerrPos == true)
			{
				玩家实例.玩家切换地图(地图实例, 地图区域类型.未知区域, new Point(this.MapX.Value, this.MapY.Value));
				return;
			}
			Point 坐标;
			坐标 = ((this.MapX.HasValue && this.MapY.HasValue) ? 计算类.游戏坐标转点阵坐标(new PointF(this.MapX.Value * 100, this.MapY.Value * 100)) : (地图区域?.随机坐标 ?? Point.Empty));
			if (坐标.IsEmpty)
			{
				for (int i = 1; i < 地图实例.地图大小.X; i++)
				{
					for (int j = 1; j < 地图实例.地图大小.Y; j++)
					{
						if (地图实例.能否通行(new Point(i, j)))
						{
							坐标 = new Point(i, j);
							break;
						}
					}
				}
			}
			玩家实例.玩家切换地图(地图实例, 地图区域类型.未知区域, 坐标);
		}
	}
}
