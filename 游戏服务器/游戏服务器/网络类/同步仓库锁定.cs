namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 273, 长度 = 3, 注释 = "g2c_lock_bank")]
	public class 同步仓库锁定 : 游戏封包
	{
		[封包字段描述(下标 = 2, 长度 = 1)]
		public bool 锁定状态;

		public override ushort 封包编号 => 273;
	}
}
