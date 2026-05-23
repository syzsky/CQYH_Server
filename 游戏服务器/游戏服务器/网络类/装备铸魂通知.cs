namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 275, 长度 = 4, 注释 = "装备铸魂通知")]
	public sealed class 装备铸魂通知 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 2)]
		public ushort 返回结果;

		public override ushort 封包编号 => 275;
	}
}
