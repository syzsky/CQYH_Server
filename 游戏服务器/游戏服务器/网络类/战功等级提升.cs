namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 378, 长度 = 6, 注释 = "战功等级提升")]
	public class 战功等级提升 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 未知参数一;

		[封包字段描述(下标 = 4, 长度 = 2)]
		public ushort 未知参数二;

		public override ushort 封包编号 => 378;
	}
}
