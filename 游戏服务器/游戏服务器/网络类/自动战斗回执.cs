namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 370, 长度 = 6, 注释 = "自动战斗回执")]
	public class 自动战斗回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 开关状态;

		public override ushort 封包编号 => 370;
	}
}
