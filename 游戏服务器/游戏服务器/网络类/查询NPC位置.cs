namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 705, 长度 = 26, 注释 = "m2c_query_npc_pos")]
	public class 查询NPC位置 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 地图编号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 对象编号;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int 状态标志;

		[封包字段描述(下标 = 14, 长度 = 4)]
		public int X;

		[封包字段描述(下标 = 18, 长度 = 4)]
		public int Y;

		[封包字段描述(下标 = 22, 长度 = 4)]
		public int Z;

		public override ushort 封包编号 => 705;
	}
}
