namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 260, 长度 = 22, 注释 = "玩家合无相石")]
	public sealed class 玩家合无相石 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 物品位置;

		[封包字段描述(下标 = 18, 长度 = 4)]
		public int 一键合成;

		public override ushort 封包编号 => 260;
	}
}
