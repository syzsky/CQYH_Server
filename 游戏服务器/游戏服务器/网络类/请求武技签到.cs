namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 264, 长度 = 2, 注释 = "请求武技签到")]
	public sealed class 请求武技签到 : 游戏封包
	{
		public override ushort 封包编号 => 264;
	}
}
