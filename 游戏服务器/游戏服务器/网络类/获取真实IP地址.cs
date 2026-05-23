namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 8, 长度 = 8, 注释 = "获取真实IP地址")]
	public sealed class 获取真实IP地址 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 网络地址一;

		[封包字段描述(下标 = 5, 长度 = 1)]
		public byte 网络地址二;

		[封包字段描述(下标 = 6, 长度 = 1)]
		public byte 网络地址三;

		[封包字段描述(下标 = 7, 长度 = 1)]
		public byte 网络地址四;

		public override ushort 封包编号 => 8;
	}
}
