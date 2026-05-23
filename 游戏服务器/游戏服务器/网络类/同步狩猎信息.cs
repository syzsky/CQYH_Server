namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 301, 长度 = 14, 注释 = "g2c_sync_advanced_exercise")]
	public class 同步狩猎信息 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 未知标志;

		[封包字段描述(下标 = 4, 长度 = 2)]
		public ushort 未知参数;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 狩猎编号;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int 剩余秒数;

		public override ushort 封包编号 => 301;
	}
}
