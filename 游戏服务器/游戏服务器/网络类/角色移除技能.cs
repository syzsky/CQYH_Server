namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 90, 长度 = 4, 注释 = "角色移除技能")]
	public sealed class 角色移除技能 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 技能编号;

		public override ushort 封包编号 => 90;
	}
}
