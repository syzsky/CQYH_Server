using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using 游戏服务器.工具类;

namespace 游戏服务器.模板类
{
	public sealed class C_06_计算宠物召唤 : 技能任务
	{
		public string 召唤宠物名字 { get; set; }

		public byte[] 召唤宠物数量 { get; set; }

		public byte[] 宠物等级上限 { get; set; }

		public bool 增加技能经验 { get; set; }

		public ushort 经验技能编号 { get; set; }

		public bool 宠物绑定武器 { get; set; }

		public ushort 宠物绑定BUFF { get; set; }

		public bool 检查技能铭文 { get; set; }

		public int 宠物存活时间 { get; set; }

		public string[] 忽略宠物列表 { get; set; }

		[Category("范围召唤")]
		public bool 启用范围召唤 { get; set; }

		[Category("范围召唤")]
		public bool 计算对象方向 { get; set; }

		[Category("范围召唤")]
		[Editor(typeof(PointArrayEditor), typeof(UITypeEditor))]
		public Dictionary<游戏方向, List<Point>> 范围召唤怪物 { get; set; }

		[Category("召唤怪物")]
		public bool 怪物召唤同伴 { get; set; }

		[Category("召唤怪物")]
		public bool 死亡同伴消失 { get; set; }

		[Category("召唤怪物")]
		public ushort 同伴添加BUFF { get; set; }

		public C_06_计算宠物召唤()
		{
			this.召唤宠物数量 = new byte[4];
			this.宠物等级上限 = new byte[4];
			this.范围召唤怪物 = new Dictionary<游戏方向, List<Point>>();
			foreach (object value in Enum.GetValues(typeof(游戏方向)))
			{
				this.范围召唤怪物.Add((游戏方向)value, new List<Point>());
			}
		}
	}
}
