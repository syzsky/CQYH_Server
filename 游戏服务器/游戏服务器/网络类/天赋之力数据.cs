namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 360, 长度 = 82, 注释 = "天赋之力数据")]
	public class 天赋之力数据 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 0)]
		public byte[] 数据数组;

		public override ushort 封包编号 => 360;
	}
}
