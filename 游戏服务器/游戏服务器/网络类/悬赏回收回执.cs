namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 173, 长度 = 10, 注释 = "悬赏回收回执")]
	public class 悬赏回收回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 物品编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 物品位置;

		public override ushort 封包编号 => 173;
	}
}
