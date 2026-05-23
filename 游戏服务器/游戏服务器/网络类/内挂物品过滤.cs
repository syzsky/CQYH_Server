namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 267, 长度 = 282, 注释 = "内挂物品过滤")]
	public sealed class 内挂物品过滤 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 280)]
		public byte[] 字节描述;

		public override ushort 封包编号 => 267;
	}
}
