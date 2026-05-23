namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 99, 长度 = 6, 注释 = "玩家孔色传承")]
	public sealed class 玩家孔色传承 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 来源未知;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 来源位置;

		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 传承未知;

		[封包字段描述(下标 = 5, 长度 = 1)]
		public byte 传承位置;

		public override ushort 封包编号 => 99;
	}
}
