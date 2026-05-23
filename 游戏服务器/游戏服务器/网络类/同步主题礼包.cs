namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 379, 长度 = 142, 注释 = "同步主题礼包")]
	public sealed class 同步主题礼包 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 140)]
		public byte[] 购买数据;

		public override ushort 封包编号 => 379;
	}
}
