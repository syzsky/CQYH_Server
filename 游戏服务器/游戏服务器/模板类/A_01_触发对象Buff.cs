using System.ComponentModel;
using System.Drawing.Design;
using 游戏服务器.工具类;

namespace 游戏服务器.模板类
{
	public sealed class A_01_触发对象Buff : 技能任务
	{
		public bool 角色自身添加 { get; set; }

		public ushort 触发Buff编号 { get; set; }

		public ushort 伴生Buff编号 { get; set; }

		public float Buff触发概率 { get; set; }

		public bool 验证铭文技能 { get; set; }

		public ushort 所需铭文编号 { get; set; }

		public bool 同组铭文无效 { get; set; }

		public bool 验证效果取反 { get; set; }

		public bool 验证自身Buff { get; set; }

		public ushort 自身Buff编号 { get; set; }

		public bool 触发成功移除 { get; set; }

		public bool 移除伴生Buff { get; set; }

		public ushort 移除伴生编号 { get; set; }

		public bool 验证分组Buff { get; set; }

		public ushort Buff分组编号 { get; set; }

		public bool 验证目标Buff { get; set; }

		public ushort 目标Buff编号 { get; set; }

		public byte 所需Buff层数 { get; set; }

		public bool 验证目标类型 { get; set; }

		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 指定目标类型 所需目标类型 { get; set; }

		public bool 增加技能经验 { get; set; }

		public ushort 经验技能编号 { get; set; }

		public ushort 增减目标BUFF { get; set; }

		public int 增减BUFF层数 { get; set; }
	}
}
