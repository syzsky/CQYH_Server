namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 32, 长度 = 5, 注释 = "同步行会成就")]
	public class 同步行会成就 : 游戏封包
	{
		public override ushort 封包编号 => 32;
	}
}
