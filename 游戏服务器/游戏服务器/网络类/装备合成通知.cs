namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 272, 长度 = 2, 注释 = "装备合成通知")]
	public sealed class 装备合成通知 : 游戏封包
	{
		public override ushort 封包编号 => 272;
	}
}
