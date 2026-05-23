namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 220, 长度 = 10, 注释 = "龙卫传承觉醒")]
	public sealed class 龙卫传承觉醒 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 属性位置;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 当前阶段;

		public override ushort 封包编号 => 220;
	}
}
