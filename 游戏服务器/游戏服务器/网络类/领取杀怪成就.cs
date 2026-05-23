namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 130, 长度 = 5, 注释 = "领取杀怪成就")]
	public sealed class 领取杀怪成就 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 成就编号;

		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 进度编号;

		public override ushort 封包编号 => 130;
	}
}
