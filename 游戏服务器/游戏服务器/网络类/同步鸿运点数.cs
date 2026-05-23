namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 385, 长度 = 7, 注释 = "同步鸿运点数")]
	public class 同步鸿运点数 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 未知1;

		[封包字段描述(下标 = 2, 长度 = 4)]
		public byte 未知2;

		public override ushort 封包编号 => 385;
	}
}
