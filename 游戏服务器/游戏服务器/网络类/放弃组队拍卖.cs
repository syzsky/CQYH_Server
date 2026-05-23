namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 202, 长度 = 10, 注释 = "放弃组队拍卖")]
	public sealed class 放弃组队拍卖 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 拍卖顺序;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 对象编号;

		public override ushort 封包编号 => 202;
	}
}
