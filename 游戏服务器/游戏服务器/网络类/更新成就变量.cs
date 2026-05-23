namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 221, 长度 = 10, 注释 = "g2c_update_achievement_variable")]
	public class 更新成就变量 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 4)]
		public int 序号;

		[封包字段描述(下标 = 6, 长度 = 4)]
		public int 序号2;

		public override ushort 封包编号 => 221;
	}
}
