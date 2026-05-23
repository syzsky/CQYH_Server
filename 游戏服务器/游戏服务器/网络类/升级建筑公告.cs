namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 627, 长度 = 15, 注释 = "升级建筑公告")]
	public sealed class 升级建筑公告 : 游戏封包
	{
		public override ushort 封包编号 => 627;
	}
}
