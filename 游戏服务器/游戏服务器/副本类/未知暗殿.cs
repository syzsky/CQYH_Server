using 游戏服务器.地图类;

namespace 游戏服务器.副本类
{
	public static class 未知暗殿
	{
		public static void 执行(地图实例 地图)
		{
			if (主程.当前时间 > 地图.节点计时)
			{
				地图.地图公告(地图.副本节点.ToString() ?? "");
				if (地图.副本节点 == 1)
				{
					地图.关闭副本();
				}
				else if (地图.副本节点 == 0)
				{
					地图.副本节点++;
					地图.节点计时 = 主程.当前时间.AddMinutes(180.0);
				}
			}
		}
	}
}
