namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.客户端, 编号 = 88, 长度 = 20, 注释 = "角色合成装备")]
	public sealed class 角色合成装备 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 合成模板;

		[封包字段描述(下标 = 6, 长度 = 7)]
		public byte[] 未知参数;

		[封包字段描述(下标 = 13, 长度 = 7)]
		public byte[] 合成参数;

		public override ushort 封包编号 => 88;
	}
}
