namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 259, 长度 = 10, 注释 = "激活天赋刻印")]
	public sealed class 激活天赋刻印 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 天赋位置;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 刻印位置;

		public override ushort 封包编号 => 259;
	}
}
