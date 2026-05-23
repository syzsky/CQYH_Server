namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 218, 长度 = 10, 注释 = "g2c_complete_achievement")]
	public class 成就完成通知 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int U1;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int U2;

		public override ushort 封包编号 => 218;
	}
}
