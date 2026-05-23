namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 223, 长度 = 4, 注释 = "龙卫恢复部位")]
	public sealed class 龙卫恢复部位 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 记录部位;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 记录序号;

		public override ushort 封包编号 => 223;
	}
}
