namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 16, 长度 = 34, 注释 = "g2c_sync_refine")]
	public class 同步精炼数据 : 游戏封包
	{
		public override ushort 封包编号 => 16;
	}
}
