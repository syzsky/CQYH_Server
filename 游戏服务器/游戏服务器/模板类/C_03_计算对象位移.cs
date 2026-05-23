using System.ComponentModel;
using System.Drawing.Design;
using 游戏服务器.工具类;

namespace 游戏服务器.模板类
{
	public sealed class C_03_计算对象位移 : 技能任务
	{
		public bool 角色自身位移 { get; set; }

		public bool 允许超出锚点 { get; set; }

		public bool 锚点反向位移 { get; set; }

		public bool 位移增加经验 { get; set; }

		public bool 多段位移通知 { get; set; }

		public bool 能否穿越障碍 { get; set; }

		public ushort 自身位移耗时 { get; set; }

		public ushort 自身硬直时间 { get; set; }

		public byte[] 自身位移次数 { get; set; }

		public byte[] 自身位移距离 { get; set; }

		public int 成功减少节点 { get; set; }

		public ushort 每格减少时间 { get; set; }

		public ushort 成功Buff编号 { get; set; }

		public float 成功Buff概率 { get; set; }

		public int 失败触发节点 { get; set; }

		public ushort 失败Buff编号 { get; set; }

		public float 失败Buff概率 { get; set; }

		public bool 推动目标位移 { get; set; }

		public bool 反向推动目标 { get; set; }

		public bool 推动增加经验 { get; set; }

		public float 推动目标概率 { get; set; }

		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 指定目标类型 推动目标类型 { get; set; }

		public byte 连续推动数量 { get; set; }

		public ushort 目标位移耗时 { get; set; }

		public byte[] 目标位移距离 { get; set; }

		public ushort 目标硬直时间 { get; set; }

		public ushort 目标位移编号 { get; set; }

		public float 位移Buff概率 { get; set; }

		public ushort 目标附加编号 { get; set; }

		[Editor(typeof(FlagEnumUIEditor), typeof(UITypeEditor))]
		public 指定目标类型 限定附加类型 { get; set; }

		public float 附加Buff概率 { get; set; }

		public byte 角色位移方式 { get; set; }

		public bool 互换目标坐标 { get; set; }

		public byte 互换最大距离 { get; set; }

		public C_03_计算对象位移()
		{
			this.自身位移次数 = new byte[4];
			this.自身位移距离 = new byte[4];
			this.目标位移距离 = new byte[4];
		}
	}
}
