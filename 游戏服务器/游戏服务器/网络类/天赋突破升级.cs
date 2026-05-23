namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 258, 长度 = 6, 注释 = "天赋突破升级")]
	public sealed class 天赋突破升级 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 天赋位置;

		public override ushort 封包编号 => 258;
	}
}
