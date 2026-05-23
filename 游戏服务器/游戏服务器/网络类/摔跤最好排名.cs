namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 347, 长度 = 6, 注释 = "g2c_wrestle_best_rank")]
	public class 摔跤最好排名 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 数据;

		public override ushort 封包编号 => 347;
	}
}
