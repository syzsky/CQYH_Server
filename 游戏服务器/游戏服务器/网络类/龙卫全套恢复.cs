namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 239, 长度 = 4, 注释 = "龙卫全套恢复")]
	public sealed class 龙卫全套恢复 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 恢复模式;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 记录序号;

		public override ushort 封包编号 => 239;
	}
}
