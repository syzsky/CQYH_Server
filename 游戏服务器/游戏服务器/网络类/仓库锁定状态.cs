namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 98, 长度 = 3, 注释 = "仓库锁定状态")]
	public sealed class 仓库锁定状态 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public bool 锁定状态;

		public override ushort 封包编号 => 98;
	}
}
