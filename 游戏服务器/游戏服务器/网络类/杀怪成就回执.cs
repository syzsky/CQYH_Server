namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 176, 长度 = 5, 注释 = "杀怪成就回执")]
	public class 杀怪成就回执 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 成就序号;

		[封包字段描述(下标 = 4, 长度 = 1)]
		public byte 进度编号;

		public override ushort 封包编号 => 176;
	}
}
