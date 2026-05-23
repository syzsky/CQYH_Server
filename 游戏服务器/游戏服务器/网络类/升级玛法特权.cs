namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 286, 长度 = 6, 注释 = "升级玛法特权")]
	public sealed class 升级玛法特权 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 特权编号;

		public override ushort 封包编号 => 286;
	}
}
