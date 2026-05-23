namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 616, 长度 = 4, 注释 = "仓库转出应答")]
	public sealed class 仓库转出应答 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 仓库页面;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 仓库位置;

		public override ushort 封包编号 => 616;
	}
}
