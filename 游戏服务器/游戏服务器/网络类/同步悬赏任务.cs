namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 170, 长度 = 0, 注释 = "g2c_sync_refreshed_reward_quest")]
	public class 同步悬赏任务 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 0)]
		public byte[] 任务描述;

		public override ushort 封包编号 => 170;
	}
}
