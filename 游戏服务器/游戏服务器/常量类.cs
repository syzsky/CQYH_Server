using System.Collections.Generic;

namespace 游戏服务器
{
	public static class 常量类
	{
		public static Dictionary<byte, ushort[]> 普通节点铭文 = new Dictionary<byte, ushort[]>
		{
			{
				0,
				new ushort[6] { 10324, 10362, 10361, 10381, 10382, 10422 }
			},
			{
				1,
				new ushort[9] { 25331, 25332, 25363, 25372, 25401, 25402, 25353, 25392, 25503 }
			},
			{
				2,
				new ushort[6] { 15323, 15381, 15354, 15342, 15391, 15411 }
			},
			{
				3,
				new ushort[7] { 20441, 20482, 20502, 20511, 20513, 20442, 20563 }
			},
			{
				4,
				new ushort[8] { 30051, 30032, 30121, 30103, 30082, 30083, 30152, 30104 }
			},
			{
				5,
				new ushort[7] { 12042, 12071, 12081, 12082, 12083, 12092, 12113 }
			}
		};

		public static Dictionary<byte, ushort> 普通最终铭文 = new Dictionary<byte, ushort>
		{
			{ 0, 10361 },
			{ 1, 25363 },
			{ 2, 15381 },
			{ 3, 20482 },
			{ 4, 30103 },
			{ 5, 12081 }
		};

		public static Dictionary<byte, ushort[]> 高级节点铭文 = new Dictionary<byte, ushort[]>
		{
			{
				0,
				new ushort[6] { 10324, 10362, 10361, 10381, 10382, 10422 }
			},
			{
				1,
				new ushort[7] { 25331, 25332, 25363, 25372, 25401, 25402, 25392 }
			},
			{
				2,
				new ushort[6] { 15323, 15381, 15354, 15342, 15391, 15411 }
			},
			{
				3,
				new ushort[7] { 20441, 20482, 20502, 20511, 20513, 20442, 20563 }
			},
			{
				4,
				new ushort[8] { 30051, 30032, 30121, 30103, 30082, 30083, 30152, 30104 }
			},
			{
				5,
				new ushort[7] { 12042, 12071, 12081, 12082, 12083, 12092, 12113 }
			}
		};

		public static Dictionary<byte, ushort[]> 高级最终铭文 = new Dictionary<byte, ushort[]>
		{
			{
				0,
				new ushort[3] { 10324, 10332, 10362 }
			},
			{
				1,
				new ushort[5] { 25331, 25342, 25362, 25402, 25353 }
			},
			{
				2,
				new ushort[4] { 15312, 15352, 15411, 15391 }
			},
			{
				3,
				new ushort[5] { 20431, 20451, 20472, 20502, 20513 }
			},
			{
				4,
				new ushort[4] { 30041, 30033, 30082, 30081 }
			},
			{
				5,
				new ushort[5] { 12042, 12054, 12082, 12092, 12113 }
			}
		};

		public static Dictionary<ushort, ushort> 找回奖励字典 = new Dictionary<ushort, ushort>
		{
			{ 1, 7 },
			{ 2, 35 },
			{ 7, 4 },
			{ 9, 70 },
			{ 11, 3 },
			{ 14, 7 },
			{ 20, 7 },
			{ 21, 7 },
			{ 27, 4 },
			{ 31, 4 }
		};

		public static Dictionary<ushort, ushort> 找回奖励费用 = new Dictionary<ushort, ushort>
		{
			{ 1, 6 },
			{ 2, 1 },
			{ 7, 12 },
			{ 9, 1 },
			{ 11, 5 },
			{ 14, 7 },
			{ 20, 24 },
			{ 21, 8 },
			{ 27, 6 },
			{ 31, 20 }
		};

		public static Dictionary<ushort, ushort> 找回奖励物品1 = new Dictionary<ushort, ushort>
		{
			{ 1, 2316 },
			{ 2, 2319 },
			{ 7, 2322 },
			{ 9, 2331 },
			{ 11, 2324 },
			{ 14, 2327 },
			{ 20, 2317 },
			{ 21, 2320 },
			{ 27, 2333 },
			{ 31, 2360 }
		};

		public static Dictionary<ushort, ushort> 七天最大进度表 = new Dictionary<ushort, ushort>
		{
			{ 36, 25 },
			{ 37, 1 },
			{ 38, 1 },
			{ 39, 3 },
			{ 40, 2 },
			{ 41, 30 },
			{ 42, 3 },
			{ 43, 2 },
			{ 44, 1 },
			{ 45, 2 },
			{ 46, 33 },
			{ 47, 1 },
			{ 48, 2 },
			{ 49, 5 },
			{ 50, 5 },
			{ 51, 35 },
			{ 52, 10 },
			{ 53, 1 },
			{ 54, 5 },
			{ 55, 10 },
			{ 56, 37 },
			{ 57, 10 },
			{ 58, 2 },
			{ 59, 500 },
			{ 60, 3 },
			{ 61, 39 },
			{ 62, 1 },
			{ 63, 2 },
			{ 64, 1 },
			{ 65, 12 },
			{ 66, 40 },
			{ 67, 5 },
			{ 68, 2 },
			{ 69, 1000 },
			{ 70, 15 }
		};
	}
}
