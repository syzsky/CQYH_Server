namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 540, 长度 = 10, 注释 = "同步亲密度")]
	public sealed class 同步好友亲密 : 游戏封包
	{
		public override ushort 封包编号 => 540;
	}
}
