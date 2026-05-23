namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 400, 长度 = 14, 注释 = "找回奖励物品")]
	public sealed class 找回奖励物品 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 找回结果;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 日程编号;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int 剩余次数;

		public override ushort 封包编号 => 340;
	}
}
