namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 303, 长度 = 6, 注释 = "g2c_advanced_exercise_ack")]
	public class 开始狩猎回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 回执结果;

		public override ushort 封包编号 => 303;
	}
}
