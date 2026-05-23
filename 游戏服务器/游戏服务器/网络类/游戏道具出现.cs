using System.Drawing;

namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 152, 长度 = 19, 注释 = "游戏道具出现")]
	public sealed class 游戏道具出现 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 模板编号;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public Point 对象坐标;

		[封包字段描述(下标 = 14, 长度 = 2)]
		public ushort 对象高度;

		[封包字段描述(下标 = 16, 长度 = 2)]
		public ushort 对象方向;

		[封包字段描述(下标 = 18, 长度 = 1)]
		public byte 操作次数 = 1;

		public override ushort 封包编号 => 152;
	}
}
