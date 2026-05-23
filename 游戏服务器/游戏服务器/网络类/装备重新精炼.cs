namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 282, 长度 = 8, 注释 = "装备重新精炼")]
	public sealed class 装备重新精炼 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 背包类型;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 背包位置;

		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 材料类型;

		[封包字段描述(下标 = 5, 长度 = 1)]
		public byte 材料位置;

		[封包字段描述(下标 = 6, 长度 = 2)]
		public ushort 特殊标记;

		public override ushort 封包编号 => 282;
	}
}
