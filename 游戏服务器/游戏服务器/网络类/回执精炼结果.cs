namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 380, 长度 = 16, 注释 = "回执精炼结果")]
	public class 回执精炼结果 : 游戏封包
	{
		[封包字段描述(下标 = 10, 长度 = 2)]
		public ushort 结果值一;

		[封包字段描述(下标 = 12, 长度 = 2)]
		public ushort 结果值二;

		[封包字段描述(下标 = 14, 长度 = 2)]
		public ushort 结果值三;

		public override ushort 封包编号 => 380;
	}
}
