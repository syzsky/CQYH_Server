namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 28, 长度 = 0, 注释 = "同步坐骑数据")]
	public class 同步坐骑数据 : 游戏封包
	{
		[封包字段描述(下标 = 14, 长度 = 0)]
		public byte[] 坐骑编号;

		public override ushort 封包编号 => 28;
	}
}
