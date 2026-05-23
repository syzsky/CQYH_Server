namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 96, 长度 = 12, 注释 = "玩家装备雕色")]
	public sealed class 玩家装备雕色 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 背包类型;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 物品位置;

		[封包字段描述(下标 = 4, 长度 = 4)]
		public int 孔洞位置;

		[封包字段描述(下标 = 8, 长度 = 4)]
		public int 孔洞颜色;

		public override ushort 封包编号 => 96;
	}
}
