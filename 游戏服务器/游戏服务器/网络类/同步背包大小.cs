namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 15, 长度 = 11, 注释 = "同步背包大小 仓库 背包 资源包...")]
	public sealed class 同步背包大小 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 穿戴大小;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 背包大小;

		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 仓库大小;

		[封包字段描述(下标 = 5, 长度 = 1)]
		public byte u1;

		[封包字段描述(下标 = 6, 长度 = 1)]
		public byte u2;

		[封包字段描述(下标 = 7, 长度 = 1)]
		public byte u3;

		[封包字段描述(下标 = 8, 长度 = 1)]
		public byte u4;

		[封包字段描述(下标 = 9, 长度 = 1)]
		public byte 资源包大小;

		[封包字段描述(下标 = 10, 长度 = 1)]
		public byte u5;

		public override ushort 封包编号 => 15;
	}
}
