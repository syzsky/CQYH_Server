namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 377, 长度 = 6, 注释 = "更新战功任务")]
	public class 更新战功任务 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 任务编号;

		[封包字段描述(下标 = 4, 长度 = 2)]
		public ushort 任务进度;

		public override ushort 封包编号 => 377;
	}
}
