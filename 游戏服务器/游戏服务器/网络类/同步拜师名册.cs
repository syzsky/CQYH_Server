namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 546, 长度 = 0, 注释 = "查询拜师名册应答")]
	public sealed class 同步拜师名册 : 游戏封包
	{
		public override ushort 封包编号 => 546;
	}
}
