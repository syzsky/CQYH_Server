namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 661, 长度 = 0, 注释 = "查询出售信息")]
	public sealed class 同步出售信息 : 游戏封包
	{
		public override ushort 封包编号 => 661;
	}
}
