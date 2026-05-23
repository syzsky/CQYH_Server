namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 265, 长度 = 25, 注释 = "开始自动战斗")]
	public sealed class 开始自动战斗 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public bool 自动战斗;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 战斗范围;

		[封包字段描述(下标 = 4, 长度 = 1)]
		public bool 开启空闲使用道具;

		[封包字段描述(下标 = 5, 长度 = 2)]
		public short 空闲时间;

		[封包字段描述(下标 = 7, 长度 = 4)]
		public int 道具ID;

		[封包字段描述(下标 = 11, 长度 = 1)]
		public byte Unk1;

		[封包字段描述(下标 = 12, 长度 = 4)]
		public int 技能ID;

		[封包字段描述(下标 = 16, 长度 = 1)]
		public bool 开启自动拾取;

		[封包字段描述(下标 = 17, 长度 = 1)]
		public byte 拾取范围;

		[封包字段描述(下标 = 18, 长度 = 1)]
		public bool 开启预留背包;

		[封包字段描述(下标 = 19, 长度 = 1)]
		public byte 预留格数;

		[封包字段描述(下标 = 20, 长度 = 1)]
		public bool 优先战斗;

		[封包字段描述(下标 = 21, 长度 = 1)]
		public bool 不捡取他人装备;

		[封包字段描述(下标 = 22, 长度 = 1)]
		public bool 不抢怪;

		[封包字段描述(下标 = 23, 长度 = 1)]
		public bool 优先拾取极品道具;

		[封包字段描述(下标 = 24, 长度 = 1)]
		public bool Unk2;

		public override ushort 封包编号 => 265;
	}
}
