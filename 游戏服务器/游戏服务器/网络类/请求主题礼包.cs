namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 279, 长度 = 2, 注释 = "请求主题礼包")]
	public sealed class 请求主题礼包 : 游戏封包
	{
		public override ushort 封包编号 => 279;
	}
}
