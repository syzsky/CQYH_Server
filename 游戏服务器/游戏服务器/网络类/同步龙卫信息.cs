namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 323, 长度 = 0, 注释 = "同步龙卫信息")]
	public class 同步龙卫信息 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 未知 = 1;

		[封包字段描述(下标 = 5, 长度 = 1)]
		public byte 格子数量 = 8;

		[封包字段描述(下标 = 6, 长度 = 1)]
		public byte 记录数量;

		[封包字段描述(下标 = 7, 长度 = 1)]
		public byte 可用记录;

		[封包字段描述(下标 = 8, 长度 = 0)]
		public byte[] 描述信息;

		public override ushort 封包编号 => 323;
	}
}
