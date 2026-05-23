namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 199, 长度 = 15, 注释 = "开始拍卖物品")]
	public sealed class 开始拍卖物品 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 拍卖顺序;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 起拍价格;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int 参与人数;

		public override ushort 封包编号 => 199;
	}
}
