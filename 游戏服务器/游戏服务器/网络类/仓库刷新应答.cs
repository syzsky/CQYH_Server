namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 613, 长度 = 0, 注释 = "仓库刷新应答")]
	public sealed class 仓库刷新应答 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 0)]
		public byte[] 字节数据;

		public override ushort 封包编号 => 613;
	}
}
