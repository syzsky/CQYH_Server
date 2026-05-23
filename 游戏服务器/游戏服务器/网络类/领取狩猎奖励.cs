namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 204, 长度 = 2, 注释 = "领取狩猎奖励")]
	public sealed class 领取狩猎奖励 : 游戏封包
	{
		public override ushort 封包编号 => 204;
	}
}
