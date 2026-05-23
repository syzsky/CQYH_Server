namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 166, 长度 = 6, 注释 = "完成任务回执")]
	public class 完成任务回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 任务编号;

		public override ushort 封包编号 => 166;
	}
}
