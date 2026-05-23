namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 252, 长度 = 3, 注释 = "请求七天详情")]
	public sealed class 请求七天详情 : 游戏封包
	{
		public override ushort 封包编号 => 252;
	}
}
