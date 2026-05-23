namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 617, 长度 = 6, 注释 = "仓库移动应答")]
	public sealed class 仓库移动应答 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 来源页面;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 来源位置;

		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 目标页面;

		[封包字段描述(下标 = 5, 长度 = 1)]
		public byte 目标位置;

		public override ushort 封包编号 => 617;
	}
}
