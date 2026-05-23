namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 663, 长度 = 10, 注释 = "同步占领行会")]
	public sealed class 同步占领行会 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 之前行会;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 现在行会;

		public override ushort 封包编号 => 663;
	}
}
