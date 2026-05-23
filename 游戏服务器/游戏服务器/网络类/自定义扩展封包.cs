namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 1024, 长度 = 0, 注释 = "自定义扩展封包")]
	public sealed class 自定义扩展封包 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 0)]
		public byte[] 字节数据;

		public override ushort 封包编号 => 1024;

		public 自定义扩展封包()
		{
			this.是否加密 = false;
		}
	}
}
