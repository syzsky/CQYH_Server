namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 651, 长度 = 0, 注释 = "敏感字符过滤")]
	public sealed class 敏感字符过滤 : 游戏封包
	{
		public override ushort 封包编号 => 651;
	}
}
