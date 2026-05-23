namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 324, 长度 = 100, 注释 = "龙卫觉醒回执")]
	public class 龙卫觉醒回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 特效编号;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 属性位置;

		[封包字段描述(下标 = 28, 长度 = 72)]
		public byte[] 数据;

		public override ushort 封包编号 => 324;
	}
}
