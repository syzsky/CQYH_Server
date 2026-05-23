namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 164, 长度 = 6, 注释 = "玩家接取任务")]
	public class 玩家接取任务 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 任务编号;

		public override ushort 封包编号 => 164;
	}
}
