using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace 游戏服务器.模板类
{
	public class 怪物刷新
	{
		public static HashSet<怪物刷新> 数据表;

		public byte 所处地图 { get; set; }

		public string 所处地名 { get; set; }

		public Point 所处坐标 { get; set; }

		public string 区域名字 { get; set; }

		public int 区域半径 { get; set; }

		public 刷新信息[] 刷新列表 { get; set; }

		public HashSet<Point> 范围坐标 { get; set; }

		public static void 载入数据()
		{
			怪物刷新.数据表 = new HashSet<怪物刷新>();
			string text;
			text = Settings.游戏数据目录 + "\\System\\游戏地图\\怪物刷新\\";
			if (Directory.Exists(text))
			{
				object[] array;
				array = 序列化类.反序列化(text, typeof(怪物刷新));
				foreach (object obj in array)
				{
					怪物刷新.数据表.Add((怪物刷新)obj);
				}
			}
		}

		public Point[] 获取刷新范围()
		{
			return this.范围坐标.ToArray();
		}

		public override string ToString()
		{
			return $"{this.所处地图}-{this.区域名字}-{((this.刷新列表 == null || this.刷新列表.Length == 0) ? "0" : (this.刷新列表.Length + " " + this.刷新列表[0].怪物名字))}";
		}
	}
}
