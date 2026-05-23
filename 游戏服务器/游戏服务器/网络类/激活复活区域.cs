namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 58, 长度 = 14, 注释 = "g2c_activate_revive_pt")]
	public sealed class 激活复活区域 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int X;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int Y;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int Z;

		public override ushort 封包编号 => 58;
	}
}
