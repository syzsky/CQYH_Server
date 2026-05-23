namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 169, 长度 = 18, 注释 = "g2c_sync_reward_quest_remain_count")]
	public class 同步悬赏剩余 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 悬赏类型;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 已经完成;

		[封包字段描述(下标 = 10, 长度 = 4)]
		public int 还能完成;

		[封包字段描述(下标 = 14, 长度 = 4)]
		public int 日程进度;

		public override ushort 封包编号 => 169;
	}
}
