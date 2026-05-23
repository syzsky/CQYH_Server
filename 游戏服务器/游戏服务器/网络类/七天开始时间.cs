namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 358, 长度 = 10, 注释 = "g2c_seven_days_start_time")]
	public class 七天开始时间 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 开始时间;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 参数二;

		public override ushort 封包编号 => 358;
	}
}
