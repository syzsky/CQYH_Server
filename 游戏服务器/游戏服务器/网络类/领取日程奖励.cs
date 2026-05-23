namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 182, 长度 = 6, 注释 = "领取日程奖励")]
	public sealed class 领取日程奖励 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 奖励进度;

		public override ushort 封包编号 => 182;
	}
}
