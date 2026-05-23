using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using 游戏服务器.工具类;

namespace 游戏服务器.模板类
{
	public sealed class C_02_计算目标伤害 : 技能任务
	{
		[Category("BUFF联动")]
		public bool 点爆命中目标 { get; set; }

		[Category("BUFF联动")]
		public ushort[] 点爆标记编号 { get; set; }

		[Category("BUFF联动")]
		public byte 点爆需要层数 { get; set; }

		[Category("BUFF联动")]
		public float 点爆标记增伤 { get; set; }

		[Category("BUFF联动")]
		public bool 失败添加层数 { get; set; }

		[Category("技能伤害")]
		public int[] 技能伤害基数 { get; set; }

		[Category("技能伤害")]
		public float[] 技能伤害系数 { get; set; }

		[Category("技能伤害")]
		public 技能伤害类型 技能伤害类型 { get; set; }

		[Category("技能伤害")]
		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 指定目标类型 技能增伤类型 { get; set; }

		[Category("技能伤害")]
		public int 技能增伤基数 { get; set; }

		[Category("技能伤害")]
		public float 技能增伤系数 { get; set; }

		[Category("技能伤害")]
		public bool 数量衰减伤害 { get; set; }

		[Category("技能伤害")]
		public float 伤害衰减系数 { get; set; }

		[Category("技能伤害")]
		public float 伤害衰减下限 { get; set; }

		[Category("技能伤害")]
		public bool 伤害不计神圣 { get; set; }

		[Category("技能斩杀")]
		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 指定目标类型 技能斩杀类型 { get; set; }

		[Category("技能斩杀")]
		public float 技能斩杀概率 { get; set; }

		[Category("技能破防")]
		public float 技能破防概率 { get; set; }

		[Category("技能破防")]
		public int 技能破防基数 { get; set; }

		[Category("技能破防")]
		public float 技能破防系数 { get; set; }

		[Category("目标状态")]
		public int 目标硬直时间 { get; set; }

		[Category("目标死亡回复")]
		public bool 目标死亡回复 { get; set; }

		[Category("目标死亡回复")]
		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 指定目标类型 回复限定类型 { get; set; }

		[Category("目标死亡回复")]
		public int 体力回复基数 { get; set; }

		[Category("目标死亡回复")]
		public bool 等级差减回复 { get; set; }

		[Category("目标死亡回复")]
		public int 减回复等级差 { get; set; }

		[Category("目标死亡回复")]
		public int 零回复等级差 { get; set; }

		[Category("宠物联动")]
		public bool 增加宠物仇恨 { get; set; }

		[Category("技能冷却")]
		public bool 击杀减少冷却 { get; set; }

		[Category("技能冷却")]
		public bool 击杀概率减少 { get; set; }

		[Category("技能冷却")]
		public float 击杀减少概率 { get; set; }

		[Category("技能冷却")]
		public bool 命中减少冷却 { get; set; }

		[Category("技能冷却")]
		public bool 命中概率减少 { get; set; }

		[Category("技能冷却")]
		public float 命中减少概率 { get; set; }

		[Category("技能冷却")]
		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 指定目标类型 冷却减少类型 { get; set; }

		[Category("技能冷却")]
		public ushort 冷却减少技能 { get; set; }

		[Category("技能冷却")]
		public byte 冷却减少分组 { get; set; }

		[Category("技能冷却")]
		public ushort 冷却减少时间 { get; set; }

		[Category("武器联动")]
		public bool 扣除武器持久 { get; set; }

		[Category("技能经验")]
		public bool 增加技能经验 { get; set; }

		[Category("技能经验")]
		public ushort 经验技能编号 { get; set; }

		[Category("BUFF联动")]
		public bool 清除目标状态 { get; set; }

		[Category("清除状态几率")]
		public float 清除状态几率 { get; set; }

		[Category("BUFF联动")]
		[Editor(typeof(HashSetEditor), typeof(UITypeEditor))]
		public HashSet<ushort> 清除状态列表 { get; set; }

		[Category("吸血吸蓝")]
		public int[] 技能吸血基数 { get; set; }

		[Category("吸血吸蓝")]
		public float[] 技能吸血系数 { get; set; }

		[Category("吸血吸蓝")]
		public bool 不扣目标蓝量 { get; set; }

		[Category("吸血吸蓝")]
		public bool 不吸目标蓝量 { get; set; }

		[Category("吸血吸蓝")]
		public bool 蓝不足转伤害 { get; set; }

		[Category("吸血吸蓝")]
		public int[] 技能吸蓝基数 { get; set; }

		[Category("吸血吸蓝")]
		public float[] 技能吸蓝系数 { get; set; }

		[Category("附加特效")]
		public byte 附加特效编号 { get; set; }

		public C_02_计算目标伤害()
		{
			this.清除状态列表 = new HashSet<ushort>();
		}
	}
}
