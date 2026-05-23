namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 610, 长度 = 9, 注释 = "验证动态密码")]
	public sealed class 验证动态密码 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 7)]
		public string 动态密码;

		public override ushort 封包编号 => 610;
	}
}
