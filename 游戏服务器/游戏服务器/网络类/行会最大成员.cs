namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 692, 长度 = 16, 注释 = "m2c_guild_max_member_setting")]
	public class 行会最大成员 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort U1;

		[封包字段描述(下标 = 4, 长度 = 2)]
		public ushort U2;

		[封包字段描述(下标 = 6, 长度 = 2)]
		public ushort U3;

		[封包字段描述(下标 = 8, 长度 = 2)]
		public ushort U4;

		[封包字段描述(下标 = 10, 长度 = 2)]
		public ushort U5;

		[封包字段描述(下标 = 12, 长度 = 2)]
		public ushort U6;

		[封包字段描述(下标 = 14, 长度 = 2)]
		public ushort U7;

		public override ushort 封包编号 => 692;
	}
}
