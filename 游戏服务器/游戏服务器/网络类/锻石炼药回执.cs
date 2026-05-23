namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 367, 长度 = 6, 注释 = "锻石炼药回执")]
	public class 锻石炼药回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 回执编号;

		public override ushort 封包编号 => 367;
	}
}
