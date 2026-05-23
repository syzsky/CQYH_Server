namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 280, 长度 = 4, 注释 = "g2c_activity_change")]
	public class 更新活动日程 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 日程进度;

		public override ushort 封包编号 => 280;
	}
}
