namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 249, 长度 = 6, 注释 = "进入演武战场")]
	public sealed class 进入演武战场 : 游戏封包
	{
		public override ushort 封包编号 => 249;
	}
}
