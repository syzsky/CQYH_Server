using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace 游戏服务器.模板类
{
	public sealed class 守卫刷新
	{
		public static HashSet<守卫刷新> 数据表;

		public ushort 守卫编号 { get; set; }

		public byte 所处地图 { get; set; }

		public string 所处地名 { get; set; }

		public Point 所处坐标 { get; set; }

		public 游戏方向 所处方向 { get; set; }

		public string 区域名字 { get; set; }

		public bool 禁止刷新 { get; set; }

		public static void 载入数据()
		{
			守卫刷新.数据表 = new HashSet<守卫刷新>();
			string text;
			text = Settings.游戏数据目录 + "\\System\\游戏地图\\守卫刷新\\";
			if (Directory.Exists(text))
			{
				object[] array;
				array = 序列化类.反序列化(text, typeof(守卫刷新));
				foreach (object obj in array)
				{
					守卫刷新.数据表.Add((守卫刷新)obj);
				}
			}
		}

		public override string ToString()
		{
			return $"{this.所处地图}-{this.守卫编号}-{this.区域名字}";
		}

		internal static void 保存数据()
		{
			string text;
			text = Settings.游戏数据目录 + "\\System\\游戏地图\\守卫刷新\\";
			if (!Directory.Exists(text))
			{
				return;
			}
			FileInfo[] files;
			files = new DirectoryInfo(text).GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
			foreach (守卫刷新 item in 守卫刷新.数据表)
			{
				StreamWriter streamWriter;
				streamWriter = File.CreateText($"{text}{item.所处地图}-{item.守卫编号}-{item.区域名字}.txt");
				streamWriter.Write(JsonConvert.SerializeObject(item, Formatting.Indented, 序列化类.全局设置));
				streamWriter.Close();
			}
		}
	}
}
