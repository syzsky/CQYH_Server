namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 145, 长度 = 6, 注释 = "g2c_char_off_mount")]
	public class 玩家骑乘下马 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 对象编号;

		public override ushort 封包编号 => 146;
	}
}
