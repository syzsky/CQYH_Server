namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 309, 长度 = 10, 注释 = "同步对象容貌")]
	public class 同步对象容貌 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 6, 长度 = 1)]
		public byte 对象发型;

		[封包字段描述(下标 = 7, 长度 = 1)]
		public byte 对象发色;

		[封包字段描述(下标 = 8, 长度 = 1)]
		public byte 对象脸型;

		[封包字段描述(下标 = 9, 长度 = 1)]
		public byte 未知参数;

		public override ushort 封包编号 => 309;
	}
}
