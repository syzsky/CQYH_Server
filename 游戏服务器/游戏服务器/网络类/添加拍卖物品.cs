namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 198, 长度 = 0, 注释 = "添加拍卖物品")]
	public sealed class 添加拍卖物品 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 4)]
		public int 拍卖顺序;

		[封包字段描述(下标 = 8, 长度 = 0)]
		public byte[] 物品描述;

		public override ushort 封包编号 => 198;
	}
}
