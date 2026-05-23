using System;
using System.Drawing;
using 游戏服务器.地图类;
using 游戏服务器.模板类;

namespace 游戏服务器.管理命令
{
	public class 添加怪物 : GM命令
	{
		[字段描述(0, 排序 = 0)]
		public string 怪物名字;

		[字段描述(0, 排序 = 1)]
		public byte 地图编号;

		[字段描述(0, 排序 = 2)]
		public int X坐标;

		[字段描述(0, 排序 = 3)]
		public int Y坐标;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
			游戏地图 value2;
			if (!游戏怪物.数据表.TryGetValue(this.怪物名字, out var value))
			{
				主程.添加命令日志("<= @Mob Command execution failed, mob " + this.怪物名字 + " does not exist");
			}
			else if (!游戏地图.数据表.TryGetValue(this.地图编号, out value2))
			{
				主程.添加命令日志($"<= @Move Command execution failed, map {this.地图编号} does not exist");
			}
			else
			{
				new 怪物实例(出生地图: 地图处理网关.已分配地图(value2.地图编号), 对应模板: value, 复活间隔: int.MaxValue, 出生范围: new Point[1] { 计算类.游戏坐标转点阵坐标(new PointF(this.X坐标 * 100, this.Y坐标 * 100)) }, 禁止复活: true, 立即刷新: true).存活时间 = DateTime.MaxValue;
			}
		}
	}
}
