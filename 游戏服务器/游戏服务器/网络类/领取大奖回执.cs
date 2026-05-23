namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 356, 长度 = 15, 注释 = "g2c_seven_days_next_score_award")]
	public class 领取大奖回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public bool 是否失败;

		[封包字段描述(下标 = 3, 长度 = 4)]
		public int 下次奖励;

		[封包字段描述(下标 = 11, 长度 = 4)]
		public int 奖励数量;

		public override ushort 封包编号 => 356;
	}
}
