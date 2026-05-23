namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 281, 长度 = 4, 注释 = "g2c_activity_reward")]
	public class 同步日程奖励 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 奖励挡位;

		public override ushort 封包编号 => 281;
	}
}
