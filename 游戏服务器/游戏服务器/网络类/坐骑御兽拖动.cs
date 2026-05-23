namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 219, 长度 = 5, 注释 = "坐骑御兽拖动")]
	public sealed class 坐骑御兽拖动 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 御兽栏位;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 坐骑编号;

		public override ushort 封包编号 => 219;
	}
}
