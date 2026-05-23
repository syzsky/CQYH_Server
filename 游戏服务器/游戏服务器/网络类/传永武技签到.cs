namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 366, 长度 = 4, 注释 = "传永武技签到")]
	public class 传永武技签到 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 签到次数;

		[封包字段描述(下标 = 3, 长度 = 1)]
		public bool 能否签到;

		public override ushort 封包编号 => 366;
	}
}
