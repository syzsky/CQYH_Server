namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 657, 长度 = 6, 注释 = "同步元宝数量")]
	public sealed class 同步元宝数量 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public uint 元宝数量;

		public override ushort 封包编号 => 657;
	}
}
