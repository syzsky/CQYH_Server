namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 317, 长度 = 3, 注释 = "坐骑面板回执")]
	public class 坐骑面板回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 编号;

		public override ushort 封包编号 => 317;
	}
}
