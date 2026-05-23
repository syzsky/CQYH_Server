namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 574, 长度 = 2, 注释 = "邮件发送成功")]
	public sealed class 成功发送邮件 : 游戏封包
	{
		public override ushort 封包编号 => 574;
	}
}
