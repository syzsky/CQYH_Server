namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 620, 长度 = 6, 注释 = "行会工资变动")]
	public sealed class 行会工资变动 : 游戏封包
	{
		public override ushort 封包编号 => 620;
	}
}
