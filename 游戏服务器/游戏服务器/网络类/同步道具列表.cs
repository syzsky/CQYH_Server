using System.Drawing;

namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 153, 长度 = 0, 注释 = "同步道具列表")]
	public sealed class 同步道具列表 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 4)]
		public int ObjectId;

		[封包字段描述(下标 = 8, 长度 = 4)]
		public int NPCTemplateId;

		[封包字段描述(下标 = 12, 长度 = 4)]
		public Point Position;

		[封包字段描述(下标 = 16, 长度 = 2)]
		public ushort Altitude;

		[封包字段描述(下标 = 18, 长度 = 2)]
		public ushort Direction;

		[封包字段描述(下标 = 20, 长度 = 1)]
		public byte openCount = 1;

		public override ushort 封包编号 => 153;
	}
}
