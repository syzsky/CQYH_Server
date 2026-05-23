namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 17, 长度 = 0, 注释 = "同步背包信息")]
	public sealed class 同步背包信息 : 游戏封包
	{
		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 未知标志;

		[封包字段描述(下标 = 10, 长度 = 0)]
		public byte[] 物品描述;

		public override ushort 封包编号 => 17;
	}
}
