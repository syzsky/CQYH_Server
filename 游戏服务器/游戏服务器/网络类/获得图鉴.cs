namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 222, 长度 = 10, 注释 = "获得图鉴")]
	public class 获得图鉴 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 图鉴类型;

		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 图鉴编号;

		public override ushort 封包编号 => 222;
	}
}
