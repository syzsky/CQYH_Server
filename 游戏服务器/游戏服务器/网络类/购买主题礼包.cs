namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 278, 长度 = 22, 注释 = "购买主题礼包")]
	public class 购买主题礼包 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 日期序号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 物品A_ID;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int 物品B_ID;

		[封包字段描述(下标 = 14, 长度 = 4)]
		public int 物品C_ID;

		[封包字段描述(下标 = 18, 长度 = 4)]
		public int 物品D_ID;

		public override ushort 封包编号 => 278;
	}
}
