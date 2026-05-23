using System.ComponentModel;

namespace 游戏服务器.模板类
{
	public sealed class A_00_触发子类技能 : 技能任务
	{
		[Category("基本设置")]
		public 技能触发方式 技能触发方式 { get; set; }

		[Category("基本设置")]
		public string 触发技能名字 { get; set; }

		[Category("基本设置")]
		public bool 增加动作编号 { get; set; }

		[Category("基本设置")]
		public string 反手技能名字 { get; set; }

		[Category("基本设置")]
		public bool 触发成功结束 { get; set; }

		[Category("基本设置")]
		public bool 不发结束通知 { get; set; }

		[Category("触发概率")]
		public bool 计算触发概率 { get; set; }

		[Category("触发概率")]
		public bool 计算幸运概率 { get; set; }

		[Category("触发概率")]
		public float 技能触发概率 { get; set; }

		[Category("触发概率")]
		public ushort 增加概率Buff { get; set; }

		[Category("触发概率")]
		public float Buff增加系数 { get; set; }

		[Category("检测BUFF")]
		public bool 验证自身Buff { get; set; }

		[Category("检测BUFF")]
		public ushort 自身Buff编号 { get; set; }

		[Category("检测BUFF")]
		public byte 检测BUFF层数 { get; set; }

		[Category("检测BUFF")]
		public bool 触发成功移除 { get; set; }

		[Category("检测BUFF")]
		public bool 验证目标Buff { get; set; }

		[Category("检测BUFF")]
		public ushort 目标Buff编号 { get; set; }

		[Category("检测技能")]
		public bool 验证铭文技能 { get; set; }

		[Category("检测技能")]
		public ushort 所需铭文编号 { get; set; }

		[Category("检测技能")]
		public bool 同组铭文无效 { get; set; }

		[Category("检测技能")]
		public byte 所需技能等级 { get; set; }

		[Category("检测技能")]
		public bool 检测技能等级 { get; set; }

		[Category("检测武器")]
		public int 所需武器编号 { get; set; }

		[Category("检测武器")]
		public bool 检测武器编号 { get; set; }

		[Category("检测目标")]
		public bool 检测目标数量 { get; set; }

		[Category("检测目标")]
		public byte 限定目标数量 { get; set; }

		[Category("检测目标")]
		public byte 目标距离大于 { get; set; }

		[Category("检测目标")]
		public byte 目标距离小于 { get; set; }
	}
}
