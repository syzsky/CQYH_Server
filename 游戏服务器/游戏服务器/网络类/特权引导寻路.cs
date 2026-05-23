namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 650, 长度 = 10, 注释 = "特权引导寻路")]
	public sealed class 特权引导寻路 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 地图编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 对象编号;

		public override ushort 封包编号 => 650;
	}
}
