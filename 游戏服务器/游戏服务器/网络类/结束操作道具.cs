namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 155, 长度 = 11, 注释 = "结束操作道具")]
	public sealed class 结束操作道具 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 玩家编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 10, 长度 = 1)]
		public byte Unknown;

		public override ushort 封包编号 => 155;
	}
}
