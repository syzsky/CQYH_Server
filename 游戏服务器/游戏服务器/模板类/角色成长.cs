using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using _001D_000F_0007_0013_0011_0015;

namespace 游戏服务器.模板类
{
	public sealed class 角色成长
	{
		public static Dictionary<int, Dictionary<游戏对象属性, int>> 数据表;

		public static Dictionary<byte, long> 升级所需经验;

		public static Dictionary<byte, int> 升级增加战力;

		public static readonly ushort[] 宠物升级经验;

		static 角色成长()
		{
			角色成长.升级所需经验 = new Dictionary<byte, long>();
			角色成长.升级增加战力 = new Dictionary<byte, int>();
			角色成长.宠物升级经验 = new ushort[9] { 5, 10, 15, 20, 25, 30, 35, 40, 45 };
			角色成长.数据表 = new Dictionary<int, Dictionary<游戏对象属性, int>>();
			string[] array;
			array = Regex.Split(File.ReadAllText(Settings.游戏数据目录 + "\\System\\成长属性.txt").Trim('\r', '\n', '\r'), "\r\n", RegexOptions.IgnoreCase);
			string[] 属性名数组;
			属性名数组 = array[0].Split('\t');
			Dictionary<string, int> dictionary;
			dictionary = 属性名数组.ToDictionary( (string K) => K, (string V) => Array.IndexOf(属性名数组, V));
			for (int i = 1; i < array.Length; i++)
			{
				string[] array2;
				array2 = array[i].Split('\t');
				if (array2.Length <= 1)
				{
					continue;
				}
				Dictionary<游戏对象属性, int> dictionary2;
				dictionary2 = new Dictionary<游戏对象属性, int>();
				int key;
				key = (int)(游戏对象职业)Enum.Parse(typeof(游戏对象职业), array2[0]) * 256 + Convert.ToInt32(array2[1]);
				for (int j = 2; j < 属性名数组.Length; j++)
				{
					if (Enum.TryParse<游戏对象属性>(属性名数组[j], out var result) && Enum.IsDefined(typeof(游戏对象属性), result))
					{
						dictionary2[result] = Convert.ToInt32(array2[dictionary[result.ToString()]]);
					}
				}
				角色成长.数据表.Add(key, dictionary2);
			}
			array = Regex.Split(File.ReadAllText(Settings.游戏数据目录 + "\\System\\玩家升级经验.txt").Trim('\r', '\n', '\r'), "\r\n", RegexOptions.IgnoreCase);
			for (int k = 0; k < array.Length; k++)
			{
				string[] array3;
				array3 = array[k].Split('=');
				角色成长.升级所需经验.Add(byte.Parse(array3[0].Trim()), long.Parse(array3[1].Trim()));
			}
			array = Regex.Split(File.ReadAllText(Settings.游戏数据目录 + "\\System\\玩家升级战力.txt").Trim('\r', '\n', '\r'), "\r\n", RegexOptions.IgnoreCase);
			for (int l = 0; l < array.Length; l++)
			{
				string[] array4;
				array4 = array[l].Split('=');
				角色成长.升级增加战力.Add(byte.Parse(array4[0].Trim()), int.Parse(array4[1].Trim()));
			}
		}

		public static Dictionary<游戏对象属性, int> 获取数据(游戏对象职业 职业, byte 等级)
		{
			return 角色成长.数据表[(byte)职业 * 256 + Math.Max(等级, (byte)1)];
		}

		public static int 等级战力(byte 等级)
		{
			int num;
			num = 0;
			for (byte b = 1; b < 等级 + 1; b++)
			{
				num += 角色成长.升级增加战力[b];
			}
			return num;
		}
	}
}
