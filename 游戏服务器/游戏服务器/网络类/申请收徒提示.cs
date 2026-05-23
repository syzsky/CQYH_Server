namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 558, 长度 = 12, 注释 = "申请收徒提示")]
	public sealed class 申请收徒提示 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 面板类型;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public byte 对象等级;

		[封包字段描述(下标 = 4, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 8, 长度 = 4)]
		public uint 对象声望;

		public override ushort 封包编号 => 558;
	}
}
