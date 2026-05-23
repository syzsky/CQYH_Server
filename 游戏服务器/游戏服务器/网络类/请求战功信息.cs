namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 273, 长度 = 2, 注释 = "请求战功信息")]
	public sealed class 请求战功信息 : 游戏封包
	{
		public override ushort 封包编号 => 273;
	}
}
