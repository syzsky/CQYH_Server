namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 241, 长度 = 6, 注释 = "角色防具升级")]
	public sealed class 角色防具升级 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 装备部位;

		public override ushort 封包编号 => 241;
	}
}
