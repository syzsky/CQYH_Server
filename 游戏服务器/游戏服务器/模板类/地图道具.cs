using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace 游戏服务器.模板类
{
	public class 地图道具
	{
		public static Dictionary<int, 地图道具> 数据表;

		public string 取怪物掉落;

		public 游戏怪物 掉落模板;

		public int 道具编号 { get; set; }

		public int 刷新时间 { get; set; }

		public string 道具名字 { get; set; }

		public 游戏宝箱[] 道具列表 { get; set; }

		public static void 载入数据()
		{
			string text;
			text = Settings.游戏数据目录 + "\\System\\Npc数据\\地图道具\\";
			if (Directory.Exists(text))
			{
				地图道具.数据表 = 序列化类.反序列化<地图道具>(text).ToLookup((地图道具 t) => t.道具编号, (地图道具 t) => t).ToDictionary((IGrouping<int, 地图道具> t) => t.Key, (IGrouping<int, 地图道具> t) => t.First());
			}
			else
			{
				地图道具.数据表 = new Dictionary<int, 地图道具>();
			}
			foreach (KeyValuePair<int, 地图道具> item in 地图道具.数据表)
			{
				if (item.Value.取怪物掉落 != null && item.Value.取怪物掉落 != "" && !游戏怪物.数据表.TryGetValue(item.Value.取怪物掉落, out item.Value.掉落模板))
				{
					item.Value.掉落模板 = null;
				}
			}
		}
	}
}
