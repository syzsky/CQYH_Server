namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 615, 长度 = 6, 注释 = "查询平台商品")]
	public sealed class 查询平台商品 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 过滤筛选;

		public override ushort 封包编号 => 615;
	}
}
