using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using 游戏服务器.工具类;

namespace 游戏服务器.模板类
{
	public sealed class C_01_计算命中目标 : 技能任务
	{
		public bool 清空命中列表 { get; set; }

		public bool 技能能否穿墙 { get; set; }

		public bool 技能能否招架 { get; set; }

		public 技能锁定类型 技能锁定方式 { get; set; }

		public 技能闪避类型 技能闪避方式 { get; set; }

		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 技能命中反馈 技能命中反馈 { get; set; }

		[Category("计算范围")]
		public 技能范围类型 技能范围类型 { get; set; }

		public bool 放空结束技能 { get; set; }

		public bool 发送中断通知 { get; set; }

		public bool 补发释放通知 { get; set; }

		public bool 技能命中通知 { get; set; }

		public bool 技能扩展通知 { get; set; }

		public bool 计算飞行耗时 { get; set; }

		public int 单格飞行耗时 { get; set; }

		public int 限定命中数量 { get; set; }

		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 游戏对象类型 限定目标类型 { get; set; }

		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 游戏对象关系 限定目标关系 { get; set; }

		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 指定目标类型 限定特定类型 { get; set; }

		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 指定目标类型 攻速提升类型 { get; set; }

		public int 攻速提升幅度 { get; set; }

		public bool 触发被动技能 { get; set; }

		public float 触发被动概率 { get; set; }

		[Category("增加经验")]
		public bool 增加技能经验 { get; set; }

		[Category("增加经验")]
		public ushort 经验技能编号 { get; set; }

		public bool 清除目标状态 { get; set; }

		public float 清除状态几率 { get; set; }

		[Editor(typeof(HashSetEditor), typeof(UITypeEditor))]
		public HashSet<ushort> 清除状态列表 { get; set; }

		[Category("计算范围")]
		public bool 启用个性范围 { get; set; }

		[Category("计算范围")]
		public bool 计算对象方向 { get; set; }

		[Category("计算范围")]
		public bool 计算锚点自身 { get; set; }

		[Category("计算范围")]
		[Editor(typeof(PointArrayEditor), typeof(UITypeEditor))]
		public Dictionary<游戏方向, List<Point>> 个性技能范围 { get; set; }

		[Category("宠物相关")]
		public ushort 宠物模板编号 { get; set; }

		public C_01_计算命中目标()
		{
			this.清除状态列表 = new HashSet<ushort>();
			this.个性技能范围 = new Dictionary<游戏方向, List<Point>>();
			foreach (object value in Enum.GetValues(typeof(游戏方向)))
			{
				this.个性技能范围.Add((游戏方向)value, new List<Point>());
			}
		}
	}
}
