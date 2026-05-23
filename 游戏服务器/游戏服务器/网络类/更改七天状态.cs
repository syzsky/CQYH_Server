namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 357, 长度 = 12, 注释 = "g2c_seven_days_task_state")]
	public class 更改七天状态 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 状态标志;

		[封包字段描述(下标 = 4, 长度 = 4)]
		public int 任务编号;

		[封包字段描述(下标 = 8, 长度 = 4)]
		public int 活动积分;

		public override ushort 封包编号 => 357;
	}
}
