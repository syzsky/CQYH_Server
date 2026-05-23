namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 1001, 长度 = 164, 注释 = "客户登录")]
	public sealed class 客户账号登录 : 游戏封包
	{
		[封包字段描述(下标 = 72, 长度 = 38)]
		public string 登录门票;

		[封包字段描述(下标 = 136, 长度 = 17)]
		public string 物理地址;

		[封包字段描述(下标 = 162, 长度 = 2)]
		public ushort 登录版本;

		public override ushort 封包编号 => 1001;

		public override bool 是否加密 { get; set; }

		public 客户账号登录()
		{
			this.是否加密 = false;
		}
	}
}
