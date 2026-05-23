namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 30, 长度 = 6, 注释 = "g2c_sync_purchase_aura_record")]
	public class 同步购买光环 : 游戏封包
	{
		public override ushort 封包编号 => 30;
	}
}
