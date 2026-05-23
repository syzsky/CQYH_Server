namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 612, 长度 = 5, 注释 = "更改存取权限")]
	public sealed class 更改存取权限 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 行会职位;

		[封包字段描述(下标 = 3, 长度 = 2)]
		public ushort 权限标志;

		public override ushort 封包编号 => 612;
	}
}
