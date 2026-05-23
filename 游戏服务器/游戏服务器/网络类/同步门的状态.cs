namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 182, 长度 = 10, 注释 = "g2c_sync_door_state")]
	public class 同步门的状态 : 游戏封包
	{
		public override ushort 封包编号 => 182;
	}
}
