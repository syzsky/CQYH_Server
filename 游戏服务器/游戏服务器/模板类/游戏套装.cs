using System.Collections.Generic;
using System.IO;

namespace 游戏服务器.模板类
{
	public sealed class 游戏套装
	{
		public static Dictionary<ushort, 游戏套装> 数据表;

		public static Dictionary<string, 游戏套装> 检索表;

		public ushort 套装编号 { get; set; }

		public string 套装名称 { get; set; }

		public string[] 套装物品 { get; set; }

		public Dictionary<byte, string> 套装提示 { get; set; }

		public Dictionary<byte, Dictionary<游戏对象属性, int>> 套装属性 { get; set; }

		public Dictionary<byte, ushort> 套装BUFF { get; set; }

		public 游戏套装()
		{
			this.套装属性 = new Dictionary<byte, Dictionary<游戏对象属性, int>>();
			this.套装BUFF = new Dictionary<byte, ushort>();
		}

		public static void 载入数据()
		{
			游戏套装.数据表 = new Dictionary<ushort, 游戏套装>();
			游戏套装.检索表 = new Dictionary<string, 游戏套装>();
			string text;
			text = Settings.游戏数据目录 + "\\System\\物品数据\\游戏套装\\";
			if (!Directory.Exists(text))
			{
				return;
			}
			object[] array;
			array = 序列化类.反序列化(text, typeof(游戏套装));
			for (int i = 0; i < array.Length; i++)
			{
				游戏套装 游戏套装2;
				游戏套装2 = (游戏套装)array[i];
				游戏套装.数据表.Add(游戏套装2.套装编号, 游戏套装2);
				string[] array2;
				array2 = 游戏套装2.套装物品;
				foreach (string key in array2)
				{
					if (游戏物品.检索表.TryGetValue(key, out var value) && value is 游戏装备 游戏装备2)
					{
						if (游戏装备2.参与套装 == null)
						{
							游戏装备2.参与套装 = new List<游戏套装>();
						}
						游戏装备2.参与套装.Add(游戏套装2);
					}
				}
			}
		}

		public override string ToString()
		{
			return $"{this.套装编号}-{this.套装名称}";
		}
	}
}
