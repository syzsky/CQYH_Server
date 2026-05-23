namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 262, 长度 = 10, 注释 = "勋章洗练回执")]
	public sealed class 勋章洗练回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 属性一;

		[封包字段描述(下标 = 4, 长度 = 2)]
		public ushort 属性二;

		[封包字段描述(下标 = 6, 长度 = 2)]
		public ushort 属性三;

		[封包字段描述(下标 = 8, 长度 = 2)]
		public ushort 属性四;

		public override ushort 封包编号 => 262;
	}
}
