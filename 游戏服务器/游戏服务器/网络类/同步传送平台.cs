namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 248, 长度 = 18, 注释 = "g2c_sync_teleport_platform")]
	public class 同步传送平台 : 游戏封包
	{
		public override ushort 封包编号 => 248;
	}
}
