namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 100, 长度 = 2, 注释 = "角色武器铸魂")]
	public sealed class 角色武器铸魂 : 游戏封包
	{
		public override ushort 封包编号 => 100;
	}
}
