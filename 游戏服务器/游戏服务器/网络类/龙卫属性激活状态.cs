namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 325, 长度 = 27, 注释 = "龙卫属性激活状态")]
	public class 龙卫属性激活状态 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 属性位置;

		[封包字段描述(下标 = 3, 长度 = 24)]
		public byte[] 数据;

		public override ushort 封包编号 => 325;
	}
}
