using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using 游戏服务器.工具类;

namespace 游戏服务器.模板类
{
	public sealed class C_04_计算目标诱惑 : 技能任务
	{
		public bool 检查铭文技能 { get; set; }

		public int 检查铭文编号 { get; set; }

		public ushort 瘫痪状态编号 { get; set; }

		public ushort 狂暴状态编号 { get; set; }

		public byte[] 基础诱惑数量 { get; set; }

		public byte 额外诱惑数量 { get; set; }

		public int 额外诱惑时长 { get; set; }

		public float 额外诱惑概率 { get; set; }

		public byte[] 初始宠物等级 { get; set; }

		[Editor(typeof(HashSetEditor), typeof(UITypeEditor))]
		public HashSet<string> 特定诱惑列表 { get; set; }

		public float 特定诱惑概率 { get; set; }

		public C_04_计算目标诱惑()
		{
			this.基础诱惑数量 = new byte[4];
			this.初始宠物等级 = new byte[4];
			this.特定诱惑列表 = new HashSet<string>();
		}
	}
}
