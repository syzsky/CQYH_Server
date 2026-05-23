namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 375, 长度 = 34, 注释 = "同步战功信息")]
	public class 同步战功信息 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 开始时间;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 结束时间;

		[封包字段描述(下标 = 10, 长度 = 2)]
		public ushort 战功进度;

		[封包字段描述(下标 = 12, 长度 = 2)]
		public ushort 购买战功;

		[封包字段描述(下标 = 14, 长度 = 2)]
		public ushort 战功奖励;

		[封包字段描述(下标 = 16, 长度 = 2)]
		public ushort 军机奖励;

		[封包字段描述(下标 = 18, 长度 = 4)]
		public int 战功状态;

		[封包字段描述(下标 = 22, 长度 = 2)]
		public ushort 购买次数;

		[封包字段描述(下标 = 24, 长度 = 2)]
		public ushort 未知参数二;

		[封包字段描述(下标 = 26, 长度 = 4)]
		public int 开始时间二;

		[封包字段描述(下标 = 30, 长度 = 4)]
		public int 未知参数三;

		public override ushort 封包编号 => 375;
	}
}
