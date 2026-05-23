namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 355, 长度 = 679, 注释 = "g2c_seven_days_info")]
	public class 同步七天信息 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 677)]
		public byte[] 字节描述;

		public override ushort 封包编号 => 355;
	}
}
