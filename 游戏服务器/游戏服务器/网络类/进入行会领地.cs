namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 172, 长度 = 7, 注释 = "进入行会领地")]
	public sealed class 进入行会领地 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 地图类型;

		[封包字段描述(下标 = 3, 长度 = 4)]
		public int 行会编号;

		public override ushort 封包编号 => 172;
	}
}
