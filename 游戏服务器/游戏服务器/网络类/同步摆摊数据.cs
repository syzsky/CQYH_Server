namespace 游戏服务器.网络类
{
	[封包信息描述(来源 = 封包来源.服务器, 编号 = 23, 长度 = 6, 注释 = "同步摆摊数据")]
	public class 同步摆摊数据 : 游戏封包
	{
		public override ushort 封包编号 => 23;
	}
}
