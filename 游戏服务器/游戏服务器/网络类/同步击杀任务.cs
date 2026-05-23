namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 21, 长度 = 774, 注释 = "g2c_sync_kill_npc_quest")]
	public class 同步击杀任务 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 772)]
		public byte[] 字节数据;

		public override ushort 封包编号 => 21;
	}
}
