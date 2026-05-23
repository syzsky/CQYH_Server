namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 344, 长度 = 3, 注释 = "修复最大持久")]
	public sealed class 修复最大持久 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public bool 修复失败;

		public override ushort 封包编号 => 344;
	}
}
