namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 253, 长度 = 7, 注释 = "领取七天奖励")]
	public sealed class 领取七天奖励 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 未知参数;

		[封包字段描述(下标 = 3, 长度 = 4)]
		public int 领取编号;

		public override ushort 封包编号 => 253;
	}
}
