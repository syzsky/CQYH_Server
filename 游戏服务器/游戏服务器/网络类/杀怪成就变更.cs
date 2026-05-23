namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 175, 长度 = 6, 注释 = "成就进度改变")]
	public class 杀怪成就变更 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 成就序号;

		[封包字段描述(下标 = 4, 长度 = 2)]
		public ushort 成就进度;

		public override ushort 封包编号 => 175;
	}
}
