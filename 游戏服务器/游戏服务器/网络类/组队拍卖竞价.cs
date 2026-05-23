namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 141, 长度 = 10, 注释 = "队友拍卖竞价")]
	public sealed class 组队拍卖竞价 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 拍卖顺序;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 当前竞价;

		public override ushort 封包编号 => 141;
	}
}
