namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 200, 长度 = 6, 注释 = "灵符兑换历练点")]
	public sealed class 灵符兑换历练 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 获得历练点数;

		public override ushort 封包编号 => 200;
	}
}
