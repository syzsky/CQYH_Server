namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 276, 长度 = 2, 注释 = "购买战功军令")]
	public sealed class 购买战功军令 : 游戏封包
	{
		public override ushort 封包编号 => 276;
	}
}
