namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 184, 长度 = 0, 注释 = "同步场景变量")]
	public sealed class 同步场景变量 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 6)]
		public byte[] Data = new byte[6] { 20, 0, 19, 18, 228, 98 };

		public override ushort 封包编号 => 184;
	}
}
