namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 328, 长度 = 0, 注释 = "查看他人龙卫")]
	public class 查看他人龙卫 : 游戏封包
	{
		[封包字段描述(下标 = 4, 长度 = 0)]
		public byte[] 描述信息;

		public override ushort 封包编号 => 328;
	}
}
