using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using 游戏服务器.工具类;

namespace 游戏服务器.模板类
{
	public sealed class A_02_触发陷阱技能 : 技能任务
	{
		public string 触发陷阱技能 { get; set; }

		[Category("计算范围")]
		public 技能范围类型 触发陷阱数量 { get; set; }

		[Category("计算范围")]
		public bool 启用个性范围 { get; set; }

		[Category("计算范围")]
		public bool 计算对象方向 { get; set; }

		[Category("计算范围")]
		public bool 计算锚点自身 { get; set; }

		[Category("计算范围")]
		[Editor(typeof(PointArrayEditor), typeof(UITypeEditor))]
		public Dictionary<游戏方向, List<Point>> 个性技能范围 { get; set; }

		public bool 增加技能经验 { get; set; }

		public ushort 经验技能编号 { get; set; }

		[Category("概率触发")]
		public bool 计算触发概率 { get; set; }

		[Category("概率触发")]
		public float 陷阱触发概率 { get; set; }

		[Category("概率触发")]
		public bool 追踪命中目标 { get; set; }

		public bool 出生限定方向 { get; set; }

		public 游戏方向 陷阱出生方向 { get; set; }

		public A_02_触发陷阱技能()
		{
			this.个性技能范围 = new Dictionary<游戏方向, List<Point>>();
			foreach (object value in Enum.GetValues(typeof(游戏方向)))
			{
				this.个性技能范围.Add((游戏方向)value, new List<Point>());
			}
		}
	}
}
