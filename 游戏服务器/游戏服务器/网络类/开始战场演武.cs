namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 247, 长度 = 10, 注释 = "开始战场演武")]
	public sealed class 开始战场演武 : 游戏封包
	{
		public override ushort 封包编号 => 247;
	}
}
