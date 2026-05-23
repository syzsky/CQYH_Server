namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 67, 长度 = 10, 注释 = "玩家合成灵石")]
	public sealed class 玩家合成灵石 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 物品编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 幸运符数;

		public override ushort 封包编号 => 67;
	}
}
