namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 92, 长度 = 5, 注释 = "角色删除技能")]
	public sealed class 角色删除技能 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 技能编号;

		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 未知参数;

		public override ushort 封包编号 => 92;
	}
}
