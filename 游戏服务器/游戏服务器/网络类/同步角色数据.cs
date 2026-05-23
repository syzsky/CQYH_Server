using System.Drawing;

namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 12, 长度 = 176, 注释 = "同步角色数据(地图.坐标.职业.性别.等级...)")]
	public sealed class 同步角色数据 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 觉醒之力经验;

		[封包字段描述(下标 = 10, 长度 = 8)]
		public long 当前经验;

		[封包字段描述(下标 = 18, 长度 = 8)]
		public long 所需经验;

		[封包字段描述(下标 = 26, 长度 = 4)]
		public int 觉醒经验上限;

		[封包字段描述(下标 = 34, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 38, 长度 = 4)]
		public int 当前地图;

		[封包字段描述(下标 = 58, 长度 = 4)]
		public int 经验上限 = int.MaxValue;

		[封包字段描述(下标 = 62, 长度 = 4)]
		public int 双倍经验;

		[封包字段描述(下标 = 70, 长度 = 4)]
		public int PK值惩罚;

		[封包字段描述(下标 = 90, 长度 = 4)]
		public int 当前时间;

		[封包字段描述(下标 = 110, 长度 = 2)]
		public ushort 鸿运点数;

		[封包字段描述(下标 = 114, 长度 = 4)]
		public Point 对象坐标;

		[封包字段描述(下标 = 118, 长度 = 2)]
		public ushort 对象高度;

		[封包字段描述(下标 = 120, 长度 = 2)]
		public ushort 对象朝向;

		[封包字段描述(下标 = 124, 长度 = 2)]
		public ushort 开放等级;

		[封包字段描述(下标 = 128, 长度 = 2)]
		public ushort 特修折扣;

		[封包字段描述(下标 = 130, 长度 = 1)]
		public byte 对象职业;

		[封包字段描述(下标 = 131, 长度 = 1)]
		public byte 对象性别;

		[封包字段描述(下标 = 132, 长度 = 1)]
		public byte 对象等级;

		[封包字段描述(下标 = 138, 长度 = 1)]
		public byte Distance = 1;

		[封包字段描述(下标 = 139, 长度 = 1)]
		public byte 开启自动战斗 = 2;

		[封包字段描述(下标 = 140, 长度 = 1)]
		public byte 攻击模式;

		[封包字段描述(下标 = 141, 长度 = 1)]
		public byte 未知参数139 = 1;

		[封包字段描述(下标 = 143, 长度 = 1)]
		public bool 是否灰名;

		public override ushort 封包编号 => 12;
	}
}
