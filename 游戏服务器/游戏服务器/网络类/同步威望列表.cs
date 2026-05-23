namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 22, 长度 = 338, 注释 = "同步威望列表(不同步会导致怪物不能主动攻击)")]
	public sealed class 同步威望列表 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 336)]
		public byte[] 字节数据;

		public override ushort 封包编号 => 22;
	}
}
