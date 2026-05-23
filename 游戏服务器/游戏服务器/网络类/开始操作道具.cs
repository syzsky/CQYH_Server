namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 154, 长度 = 12, 注释 = "开始操作道具")]
	public sealed class 开始操作道具 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 玩家编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 10, 长度 = 2)]
		public ushort Duration = 16;

		public override ushort 封包编号 => 154;
	}
}
