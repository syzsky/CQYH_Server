namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 304, 长度 = 18, 注释 = "g2c_get_advanced_exercise_info_ack")]
	public class 请求狩猎回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 回执结果;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 狩猎编号;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int 刷新金币;

		[封包字段描述(下标 = 14, 长度 = 4)]
		public int 未知参数;

		public override ushort 封包编号 => 304;
	}
}
