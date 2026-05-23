namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 235, 长度 = 10, 注释 = "找回日程奖励")]
	public sealed class 找回日程奖励 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 日程编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 找回次数;

		public override ushort 封包编号 => 235;
	}
}
