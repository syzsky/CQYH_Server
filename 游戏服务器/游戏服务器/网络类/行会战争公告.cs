namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 638, 长度 = 7, 注释 = "行会战争公告")]
	public sealed class 行会战争公告 : 游戏封包
	{
		public override ushort 封包编号 => 638;
	}
}
