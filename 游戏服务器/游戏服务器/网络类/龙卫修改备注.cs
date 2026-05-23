namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 233, 长度 = 19, 注释 = "龙卫修改备注")]
	public sealed class 龙卫修改备注 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 记录序号;

		[封包字段描述(下标 = 3, 长度 = 16)]
		public byte[] 文本信息;

		public override ushort 封包编号 => 233;
	}
}
