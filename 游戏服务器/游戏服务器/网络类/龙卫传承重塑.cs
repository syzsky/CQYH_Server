namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 221, 长度 = 11, 注释 = "龙卫传承重塑")]
	public sealed class 龙卫传承重塑 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 属性位置;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 模式;

		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 附加;

		public override ushort 封包编号 => 221;
	}
}
