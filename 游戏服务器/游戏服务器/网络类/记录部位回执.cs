namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 326, 长度 = 52, 注释 = "记录部位回执")]
	public class 记录部位回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 记录序号;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 记录部位;

		[封包字段描述(下标 = 4, 长度 = 48)]
		public byte[] 数据;

		public override ushort 封包编号 => 326;
	}
}
