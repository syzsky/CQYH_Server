namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 288, 长度 = 5, 注释 = "同步签到信息")]
	public sealed class 同步签到信息 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 签到次数;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public bool 能否签到;

		[封包字段描述(下标 = 4, 长度 = 1)]
		public bool 开启签到;

		public override ushort 封包编号 => 288;
	}
}
