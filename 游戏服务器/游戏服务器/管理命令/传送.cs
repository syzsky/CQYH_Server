using System.Drawing;
using 游戏服务器.地图类;
using 游戏服务器.模板类;

namespace 游戏服务器.管理命令
{
	public class 传送 : GM在线命令
	{
		[字段描述(0)]
		public int MapX;

		[字段描述(1)]
		public int MapY;

		public override 执行方式 执行方式 => 执行方式.优先后台执行;

		public override void 执行命令()
		{
		}

		public override void 执行命令(玩家实例 管理员)
		{
			管理员.玩家切换地图(管理员.当前地图, 地图区域类型.未知区域, 计算类.游戏坐标转点阵坐标(new PointF(this.MapX * 100, this.MapY * 100)));
		}
	}
}
