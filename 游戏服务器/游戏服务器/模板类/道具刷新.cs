using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace 游戏服务器.模板类
{
	public class 道具刷新
	{
		public static HashSet<道具刷新> 数据表;

		public int 模板编号 { get; set; }

		public int 地图编号 { get; set; }

		public HashSet<Point> 范围坐标 { get; set; }

		public int 刷新数量 { get; set; }

		public 游戏方向 所处方向 { get; set; }

		public static void 载入数据()
		{
			string text;
			text = Settings.游戏数据目录 + "\\System\\游戏地图\\道具刷新\\";
			if (Directory.Exists(text))
			{
				道具刷新.数据表 = new HashSet<道具刷新>(序列化类.反序列化<道具刷新>(text));
			}
			else
			{
				道具刷新.数据表 = new HashSet<道具刷新>();
			}
		}
	}
}
