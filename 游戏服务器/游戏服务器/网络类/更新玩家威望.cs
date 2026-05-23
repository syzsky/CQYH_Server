namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 178, 长度 = 7, 注释 = "更新玩家威望")]
	public class 更新玩家威望 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public byte 更新序号;

		[封包字段描述(下标 = 3, 长度 = 4)]
		public int 更新数值;

		public override ushort 封包编号 => 178;
	}
}
