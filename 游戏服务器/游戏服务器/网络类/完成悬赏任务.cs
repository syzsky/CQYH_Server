namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 129, 长度 = 18, 注释 = "完成悬赏任务")]
	public sealed class 完成悬赏任务 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 物品编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 物品容器;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int 物品位置;

		[封包字段描述(下标 = 14, 长度 = 4)]
		public int 任务编号;

		public override ushort 封包编号 => 129;
	}
}
