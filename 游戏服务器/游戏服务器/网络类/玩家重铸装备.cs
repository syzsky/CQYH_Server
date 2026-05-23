namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 293, 长度 = 14, 注释 = "玩家重铸装备")]
	public sealed class 玩家重铸装备 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 通知结果;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 返回编号;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int 未知参数;

		public override ushort 封包编号 => 293;
	}
}
