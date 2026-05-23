namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 165, 长度 = 10, 注释 = "放弃任务回执")]
	public class 放弃任务回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 任务编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 回执结果;

		public override ushort 封包编号 => 165;
	}
}
