namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 619, 长度 = 10, 注释 = "下架寄售物品")]
	public sealed class 下架寄售物品 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 8)]
		public uint 订单编号;

		public override ushort 封包编号 => 619;
	}
}
