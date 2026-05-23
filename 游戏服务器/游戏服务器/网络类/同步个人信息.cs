namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 530, 长度 = 39, 注释 = "m2c_self_info")]
	public class 同步个人信息 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 37)]
		public byte[] Data = new byte[37];

		public override ushort 封包编号 => 530;
	}
}
