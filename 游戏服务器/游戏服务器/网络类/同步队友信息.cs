namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 194, 长度 = 32, 注释 = "同步队友信息")]
	public sealed class 同步队友信息 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 对象等级;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int 最大体力;

		[封包字段描述(下标 = 14, 长度 = 4)]
		public int 最大魔力;

		[封包字段描述(下标 = 18, 长度 = 4)]
		public int 当前体力;

		[封包字段描述(下标 = 22, 长度 = 4)]
		public int 当前魔力;

		[封包字段描述(下标 = 26, 长度 = 2)]
		public ushort 横向坐标;

		[封包字段描述(下标 = 28, 长度 = 2)]
		public ushort 纵向坐标;

		[封包字段描述(下标 = 30, 长度 = 2)]
		public ushort 坐标高度;

		public override ushort 封包编号 => 194;
	}
}
