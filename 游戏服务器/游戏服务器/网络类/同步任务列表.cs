namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 20, 长度 = 0, 注释 = "g2c_sync_quest_list")]
	public class 同步任务列表 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 0)]
		public byte[] 任务描述;

		public override ushort 封包编号 => 20;
	}
}
