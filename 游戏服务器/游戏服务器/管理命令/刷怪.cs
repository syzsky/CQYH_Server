using System;
using System.Drawing;
using 游戏服务器.地图类;
using 游戏服务器.模板类;

namespace 游戏服务器.管理命令
{
	public class 刷怪 : GM在线命令
	{
		[字段描述(0)]
		public string 怪物名字;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
		}

		public override void 执行命令(玩家实例 管理员)
		{
			try
			{
				地图守卫 value2;
				if (游戏怪物.数据表.TryGetValue(this.怪物名字, out var value))
				{
					Point point;
					point = 计算类.前方坐标(管理员.当前坐标, 管理员.当前方向, 1);
					怪物实例 怪物实例;
					怪物实例 = new 怪物实例(value, 管理员.当前地图, int.MaxValue, new Point[1] { point }, 禁止复活: true, 立即刷新: true)
					{
						存活时间 = DateTime.MaxValue
					};
					管理员.发送系统消息($"怪物已生成 {怪物实例.地图编号}");
				}
				else if (地图守卫.数据表.TryGetValue(Convert.ToUInt16(this.怪物名字), out value2))
				{
					new 守卫实例(出生坐标: 计算类.前方坐标(管理员.当前坐标, 管理员.当前方向, 1), 对应模板: value2, 出生地图: 管理员.当前地图, 出生方向: 游戏方向.下方);
					管理员.发送系统消息("守卫已生成");
				}
				else
				{
					管理员.发送系统消息("未找到怪物[" + this.怪物名字 + "]");
				}
			}
			catch (Exception)
			{
				管理员.发送系统消息("未找到怪物[" + this.怪物名字 + "]");
			}
		}
	}
}
