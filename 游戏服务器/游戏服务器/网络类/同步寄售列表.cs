namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 677, 长度 = 0, 注释 = "同步寄售列表")]
	public sealed class 同步寄售列表 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 消息类型;

		[封包字段描述(下标 = 5, 长度 = 1)]
		public byte 道具数量;

		[封包字段描述(下标 = 6, 长度 = 0)]
		public byte[] 道具字节;

		public override ushort 封包编号 => 677;
	}
}
