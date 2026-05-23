namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 665, 长度 = 226, 注释 = "树立城主雕像")]
	public sealed class 树立城主雕像 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 224)]
		public byte[] 字节描述;

		public override ushort 封包编号 => 665;
	}
}
