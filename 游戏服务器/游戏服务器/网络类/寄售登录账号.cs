namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 676, 长度 = 68, 注释 = "同步珍宝数量")]
	public sealed class 寄售登录账号 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 消息类型;

		[封包字段描述(下标 = 3, 长度 = 65)]
		public string 登录账号;

		public override ushort 封包编号 => 676;
	}
}
