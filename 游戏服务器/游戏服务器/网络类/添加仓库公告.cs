namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 619, 长度 = 203, 注释 = "添加仓库公告")]
	public sealed class 添加仓库公告 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 操作类型;

		[封包字段描述(下标 = 3, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 7, 长度 = 4)]
		public int 操作时间;

		[封包字段描述(下标 = 11, 长度 = 192)]
		public byte[] 物品描述;

		public override ushort 封包编号 => 619;
	}
}
