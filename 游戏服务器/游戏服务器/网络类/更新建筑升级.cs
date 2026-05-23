namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 628, 长度 = 11, 注释 = "更新建筑升级")]
	public sealed class 更新建筑升级 : 游戏封包
	{
		public override ushort 封包编号 => 628;
	}
}
