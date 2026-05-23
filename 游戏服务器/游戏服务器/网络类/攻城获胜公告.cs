namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 666, 长度 = 34, 注释 = "攻城获胜公告")]
	public sealed class 攻城获胜公告 : 游戏封包
	{
		public override ushort 封包编号 => 666;
	}
}
